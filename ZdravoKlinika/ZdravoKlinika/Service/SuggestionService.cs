using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Service;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service {
    public class SuggestionService {

        AppointmentService appointmentService;
        DoctorService doctorService;
        public SuggestionService(AppointmentService appointmentService, DoctorService doctorService) { 
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
        }


        #region appointment_sugestion
        public List<Appointment> GetAppointmentSuggestions(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration, string priority, AppointmentType appointmentType) {
            List<Appointment> result = new List<Appointment>();
            List<(Doctor, List<DateTime>)> startDateTimes = new List<(Doctor, List<DateTime>)>();
            Doctor? doctor = this.doctorService.GetById(doctorJMBG);
            if (doctor == null) throw new Exception("Doctor with JMBG not found");
            startDateTimes.Add((doctor, this.GetAppointmentStartSuggestions(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration)));
            if (startDateTimes[0].Item2.Count == 0) {
                startDateTimes.Clear();
                if (priority == "time") {
                    startDateTimes = this.GetAppointmentStartSuggestionsWithTimePriority(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration);
                } else if (priority == "doctor") {
                    startDateTimes.Add((doctor, this.GetAppointmentStartSuggestionsWithDoctorPriority(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration)));
                }
            }
            foreach (var pair in startDateTimes) {
                foreach (var start in pair.Item2) {
                    result.Add(new Appointment(-1, start, duration, false, appointmentType, patientJMBG, pair.Item1.JMBG, pair.Item1.roomId ?? roomId));
                }
            }
            return result;
        }

        private List<DateTime> GetAppointmentStartSuggestions(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration) {
            // Find all appointments with given patient, doctor and room within given time interval
            List<DateTime[]> busyIntervals = this.getRoomBusyInterval(roomId, startTime, endTime);
            busyIntervals = this.mergeListOfIntervals(busyIntervals, this.getPatientBusyInterval(patientJMBG, startTime, endTime));
            busyIntervals = this.mergeListOfIntervals(busyIntervals, this.getDoctorBusyInterval(doctorJMBG, startTime, endTime));
            
            return this.ConvertFromIntervalsToDateTimes(this.ConvertFromBusyToFreeIntervals(busyIntervals, startTime, endTime, duration));
        }

        private List<(Doctor, List<DateTime>)> GetAppointmentStartSuggestionsWithTimePriority(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration) {
            string? specialization = doctorService.GetById(doctorJMBG)?.specialization;
            if (specialization is null) throw new Exception("Doctor with JMBG not found");
            // Get all doctors with same specialization
            List<Doctor> doctors = doctorService.GetAll().FindAll(d => d.specialization == specialization && d.JMBG != doctorJMBG);
            var doctorDateTimes = new List<(Doctor, List<DateTime>)>();
            doctors.ForEach(doctor => {
                doctorDateTimes.Add((doctor, this.GetAppointmentStartSuggestions(patientJMBG, doctor.JMBG, doctor.roomId ?? roomId, startTime, endTime, duration)));
            });
            return doctorDateTimes;
        }
        private List<DateTime> GetAppointmentStartSuggestionsWithDoctorPriority(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration) {
            var startDateTimes = new List<DateTime>();
            int weeks = 0;
            const int ST_MAX = 5;
            // find suggestions for next week until you find some amount
            while (startDateTimes.Count < ST_MAX) {
                startDateTimes.AddRange(this.GetAppointmentStartSuggestions(patientJMBG, doctorJMBG, roomId, startTime.AddDays(7 * weeks), endTime.AddDays(7 * (weeks + 1)), duration));
                ++weeks;
            }
            return startDateTimes;
        }
        #endregion


        #region busy_intervals
        private List<DateTime[]> getRoomBusyInterval(string roomId, DateTime startTime, DateTime endTime) {
            List<DateTime[]> intervals = new List<DateTime[]>();
            List<Appointment> appointments = appointmentService.GetAllAppointments().FindAll(a =>
                a.roomId == roomId && (a.startTime.AddMinutes(a.duration) >= startTime && a.startTime <= endTime)
            );
            foreach (Appointment a in appointments) {
                intervals.Add(new DateTime[] { a.startTime, a.startTime.AddMinutes(a.duration) });
            }
            intervals.Sort((a, b) => a[0].CompareTo(b[0]));

            return reduceListOfIntervals(intervals);

        }
        private List<DateTime[]> getPatientBusyInterval(string patientJMBG, DateTime startTime, DateTime endTime) {
            List<DateTime[]> intervals = new List<DateTime[]>();
            List<Appointment> appointments = appointmentService.GetAllAppointments().FindAll(a =>
                a.patientJMBG == patientJMBG && (a.startTime.AddMinutes(a.duration) >= startTime && a.startTime <= endTime)
            );

            foreach (Appointment a in appointments) {
                intervals.Add(new DateTime[] { a.startTime, a.startTime.AddMinutes(a.duration) });
            }
            intervals.Sort((a, b) => a[0].CompareTo(b[0]));

            return reduceListOfIntervals(intervals);


        }
        private List<DateTime[]> getDoctorBusyInterval(string doctorJMBG, DateTime startTime, DateTime endTime) {
            List<DateTime[]> intervals = new List<DateTime[]>();
            List<Appointment> appointments = appointmentService.GetAllAppointments().FindAll(a =>
                a.doctorJMBG == doctorJMBG && (a.startTime.AddMinutes(a.duration) >= startTime && a.startTime <= endTime)
            );

            foreach (Appointment a in appointments) {
                intervals.Add(new DateTime[] { a.startTime, a.startTime.AddMinutes(a.duration) });
            }
            intervals.Sort((a, b) => a[0].CompareTo(b[0]));

            return reduceListOfIntervals(intervals);


        }
        #endregion
        #region helpers
        private List<DateTime[]> mergeListOfIntervals(List<DateTime[]> intervals1, List<DateTime[]> intervals2) {
            List<DateTime[]> result = new List<DateTime[]>();
            int i = 0; int j = 0;
            while (i != intervals1.Count || j != intervals2.Count) {
                
                if (i == intervals1.Count) {
                    result.Add(intervals2[j++]);
                    continue;
                }
                if (j == intervals2.Count) {
                    result.Add(intervals1[i++]);
                    continue;
                }
                if (intervals1[i][0] == intervals2[j][0] && intervals1[i][1] == intervals2[j][1]) {
                    result.Add(intervals1[i++]); j++;
                }else if (intervals1[i][0] <= intervals2[j][0]) {
                    result.Add(intervals1[i++]);
                } else{
                    result.Add(intervals2[j++]);
                } 
            }
            return reduceListOfIntervals(result);
        }
        private List<DateTime[]> reduceListOfIntervals(List<DateTime[]> intervals) {
            List<DateTime[]> result = new List<DateTime[]>();
            if (intervals.Count == 0) return result;

            result.Add(intervals[0]);
            intervals.ForEach(interval => {
                if (result.Last()[1] >= interval[0]) {
                    result[result.Count - 1][1] = result.Last()[1] <= interval[1] ? interval[1] : result.Last()[1];
                } else {
                    result.Add(interval);
                }
            });
            return result;
        }
        private List<DateTime> ConvertFromIntervalsToDateTimes(List<DateTime[]> intervals) {

            const int ST_SHIFT = 30;
            const int ST_MAX_PER_INTERVAL = 5;

            var startDateTimes = new List<DateTime>();
            intervals.ForEach(interval => {
                DateTime current = interval[0];
                int tdAdded = 0;
                do {
                    startDateTimes.Add(current);
                    current = current.AddMinutes(ST_SHIFT);
                } while (current <= interval[1] && ++tdAdded < ST_MAX_PER_INTERVAL);
            });
            return startDateTimes;
        }
        private List<DateTime[]> ConvertFromBusyToFreeIntervals(List<DateTime[]> intervals, DateTime startTime, DateTime endTime, int duration) {
            if(intervals.Count == 0) new List<DateTime[]>() { new DateTime[] { startTime, endTime.AddMinutes(-duration) } };
            if (intervals[0][0] > startTime) intervals.Insert(0, new DateTime[] { startTime.AddMinutes(-duration), startTime });
            if (intervals[intervals.Count - 1][1] < endTime) intervals.Add(new DateTime[] { endTime.AddMinutes(duration), endTime });
            for (int i = 0; i < intervals.Count - 1; i++) {
                intervals[i][0] = intervals[i][1];
                intervals[i][1] = intervals[i + 1][0].AddMinutes(-duration);
            }
            // Remove intervals that are not valid
            intervals.RemoveAt(intervals.Count - 1);
            return intervals.FindAll(dt => dt[1] >= dt[0]);
        }
        #endregion

    }
}
