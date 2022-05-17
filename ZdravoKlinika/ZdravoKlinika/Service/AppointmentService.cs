using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service {
    public class AppointmentService {

        public AppointmentRepository appointmentRepository { get; set; }

        public DoctorService doctorService { get; set; }

        public AppointmentService(AppointmentRepository appointmentRepository, DoctorService doctorService) {
            this.appointmentRepository = appointmentRepository;
            this.doctorService = doctorService;
        }


        public List<Appointment> GetAllAppointments() {
            return this.appointmentRepository.GetAll();
        }

        public List<Appointment> GetAllInInterval(DateTime start, DateTime end) {
            return this.GetAllAppointments().Where(a => (a.startTime.AddMinutes(a.duration) >= start && a.startTime <= end)).ToList();
        }

        public bool SaveAppointment(Appointment appointment) {
          
            return this.appointmentRepository.Save(appointment);
            
        }

        public Appointment? GetAppointmentById(int id) {
            return this.appointmentRepository.GetById(id);
        }

        public bool DeleteAppointmentById(int id) {
            return this.appointmentRepository.DeleteById(id);
        }

        public bool MoveAppointmentById(int id, DateTime newtime) {
            var old = this.appointmentRepository.GetById(id);
            if (old is null) {
                return false;
            }

            if(DateTime.Compare(old.startTime.AddDays(-1), DateTime.Now)<0)
            {
                return false;
            }

            if (DateTime.Compare(old.startTime.AddDays(4), newtime) < 0) {
                return false;
            }

            old.startTime = newtime;
            
            
            return !this.appointmentRepository.Save(old);


        }

        public Appointment? getDoctorsNextAppointment(string doctorJMBG) {
            var appointments = this.GetAllAppointments().Where(appointment => appointment.doctorJMBG == doctorJMBG && appointment.startTime.AddMinutes(appointment.duration) >= DateTime.Now).ToList();
            appointments.Sort((a, b) => a.startTime.CompareTo(b.startTime));
            return appointments.FirstOrDefault();
        }

        public int GenerateNewId()
        {
            return this.appointmentRepository.GenerateNewId();
        }
        // Legacy
        #region AppointmentSugestion
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
        private List<DateTime[]> GetFreeAppointmentIntervals(List<Appointment> appointments, DateTime startTime, DateTime endTime, int duration) {
            // if we don't have appointments whole intervals if free
            if (appointments.Count == 0) return new List<DateTime[]>() { new DateTime[] { startTime, endTime.AddMinutes(-duration) } };
            // sort by starting time
            appointments.Sort((a, b) => a.startTime.CompareTo(b.startTime));
            List<DateTime[]> intervals = new List<DateTime[]>();
            // first create all intervals that are occupied 
            // add starting interval, that is our start time
            intervals.Add(new DateTime[] { startTime.AddMinutes(-duration), startTime });
            // go through appointments list if two appointments overlap merge them into same interval
            appointments.ForEach(a => {
                if (intervals[intervals.Count - 1][1] >= a.startTime && intervals[intervals.Count - 1][1] < a.startTime.AddMinutes(a.duration)) {
                    intervals[intervals.Count - 1][1] = a.startTime.AddMinutes(a.duration);
                } else {
                    intervals.Add(new DateTime[] { a.startTime, a.startTime.AddMinutes(a.duration) });
                }
            });
            // check if we need to add endTime
            if (intervals[intervals.Count - 1][1] < endTime) intervals.Add(new DateTime[] { endTime.AddMinutes(duration), endTime });
            // from occupied intervals create free intervals
            // [start1, end1], [start2,end2] => [end1, start2 - duration], [end2, start3 - duration] ...
            for (int i = 0; i < intervals.Count - 1; i++) {
                intervals[i][0] = intervals[i][1];
                intervals[i][1] = intervals[i + 1][0].AddMinutes(-duration);
            }
            // Remove intervals that are not valid
            intervals.RemoveAt(intervals.Count - 1);
            intervals = intervals.FindAll(dt => dt[1] >= dt[0]);
            return intervals;
        }



        private List<DateTime> GetAppointmentStartSuggestions(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration) {
            // Find all appointments with given patient, doctor and room within given time interval
            var appointments = this.GetAllAppointments().FindAll(a =>
                (a.patientJMBG == patientJMBG ||
                a.doctorJMBG == doctorJMBG ||
                a.roomId == roomId) && (a.startTime.AddMinutes(a.duration) >= startTime && a.startTime <= endTime));
            var freeIntervals = this.GetFreeAppointmentIntervals(appointments, startTime, endTime, duration);
            return ConvertFromIntervalsToDateTimes(freeIntervals);
        }
        private List<(Doctor,List<DateTime>)> GetAppointmentStartSuggestionsWithTimePriority(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration) {
            string? specialization = doctorService.GetById(doctorJMBG)?.specialization;
            if (specialization is null) throw new Exception("Doctor with JMBG not found");
            // Get all doctors with same specialization
            List<Doctor> doctors = doctorService.GetAll().FindAll(d => d.specialization == specialization && d.JMBG != doctorJMBG);
            var doctorDateTimes = new List<(Doctor, List<DateTime>)>();
            doctors.ForEach(doctor => {
                doctorDateTimes.Add((doctor, this.GetAppointmentStartSuggestions(patientJMBG,doctor.JMBG, doctor.roomId ?? roomId, startTime, endTime, duration)));
            });
            return doctorDateTimes;
        }
        private List<DateTime> GetAppointmentStartSuggestionsWithDoctorPriority(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration) {
            var startDateTimes = new List<DateTime>();
            int weeks = 0;
            const int ST_MAX = 5;
            // find suggestions for next week until you find some amount
            while (startDateTimes.Count < ST_MAX) {
                startDateTimes.AddRange(this.GetAppointmentStartSuggestions(patientJMBG, doctorJMBG, roomId, startTime.AddDays(7*weeks), endTime.AddDays(7 * (weeks+1)), duration));
                ++weeks;
            }
            return startDateTimes;
        }

        // This function can be changed base on how you want to extract start times from the interval
        // DON'T CHANGE WITHOUT PERMISSION
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
        #endregion
    }
}