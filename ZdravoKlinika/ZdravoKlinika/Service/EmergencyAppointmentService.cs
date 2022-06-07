using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.DTO;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service {
    public class EmergencyAppointmentService {
        const int EMERGENCY_APPOINTMENT_DURATION = 60;
        AppointmentService appointmentService;
        SuggestionService suggestionService;
        RoomService roomService;
        DoctorService doctorService;

        public EmergencyAppointmentService(SuggestionService suggestionService, AppointmentService appointmentService, RoomService roomService, DoctorService doctorService) {
            this.suggestionService = suggestionService;
            this.appointmentService = appointmentService;
            this.roomService = roomService;
            this.doctorService = doctorService;
        }

        public EmergencyAppointmentSuggestionDTO createOrSuggestEmergencyAppointment(string patientJMBG, string specialization) {

            EmergencyAppointmentSuggestionDTO emergencyAppointmentReturn = new EmergencyAppointmentSuggestionDTO();

            List<Doctor> doctors = this.doctorService.GetAll().FindAll(doctor => doctor.specialization == specialization);
            string? roomId = this.getFreeRoomIdsForEmergency().FirstOrDefault();

            foreach(var doctor in doctors) {
                var appointment = this.appointmentService.GetDoctorsNextAppointment(doctor.JMBG);
                if(appointment is null || appointment.startTime >= DateTime.Now.AddMinutes(EMERGENCY_APPOINTMENT_DURATION)) {
                    if (roomId is null) continue;
                    return this.FoundEmergencyAppointment(roomId, patientJMBG, doctor.JMBG);
                } else {
                    emergencyAppointmentReturn.pairsOfAppointmentAndMovedAppointment.Add(MoveAppointmentSuggestionPair(appointment, patientJMBG));
                }
            }

            if (emergencyAppointmentReturn.pairsOfAppointmentAndMovedAppointment.Count == 0) throw new Exception("Emergency appointment can not be created or suggested");
            emergencyAppointmentReturn.pairsOfAppointmentAndMovedAppointment.Sort((a, b) => a.Item2.startTime.CompareTo(b.Item2.startTime));

            return emergencyAppointmentReturn;

        }
        private (Appointment, Appointment) MoveAppointmentSuggestionPair(Appointment appointment, string patientJMBG) {
            var appointmentMove = this.suggestionService.GetAppointmentMoveSuggestions(appointment);
            appointment.patientJMBG = patientJMBG;
            appointment.urgency = true;
            return (appointment, appointmentMove.First());
        }
        private EmergencyAppointmentSuggestionDTO FoundEmergencyAppointment(string roomId, string patientJMBG, string doctorJMBG) {

            var appointment = this.suggestionService.GetFirstNextAppointment(patientJMBG, doctorJMBG, roomId, EMERGENCY_APPOINTMENT_DURATION);
            appointment.id = this.appointmentService.GenerateNewId();
            appointment.urgency = true;
            this.appointmentService.SaveAppointment(appointment);
            return new EmergencyAppointmentSuggestionDTO(true, (appointment, null));
        }
        
        private List<string> getFreeRoomIdsForEmergency() {
            List<string> roomIds = this.roomService.GetAll()
                .Where(room => room.type != "Sala za sastanke")
                .Select(room => room.roomId)
                .ToList();

            return this.suggestionService.getFreeRoomsInInterval(roomIds, DateTime.Now, DateTime.Now.AddMinutes(EMERGENCY_APPOINTMENT_DURATION));
        }

    }
}
