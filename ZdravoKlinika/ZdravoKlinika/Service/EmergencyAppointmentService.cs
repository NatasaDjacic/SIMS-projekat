using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.DTO;

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

        public EmergencyAppointmentSuggestionDTO createEmergencyAppointment(string patientJMBG, string specialization) {

            EmergencyAppointmentSuggestionDTO emergencyAppointmentReturn = new EmergencyAppointmentSuggestionDTO();
            List<Doctor> doctors = this.doctorService.GetAll().FindAll(doctor => doctor.specialization == specialization);
            string roomId = this.getFreeRoomIdsInNextHoure().First();
            foreach(var doctor in doctors) {
                var appointment = this.appointmentService.getDoctorsNextAppointment(doctor.JMBG);
                if(appointment is null || appointment.startTime >= DateTime.Now.AddMinutes(EMERGENCY_APPOINTMENT_DURATION)) {
                    emergencyAppointmentReturn.found = true;
                    appointment = this.suggestionService.getFirstNextAppointment(patientJMBG, doctor.JMBG, roomId, EMERGENCY_APPOINTMENT_DURATION);
                    emergencyAppointmentReturn.pairsOfAppointmentAndMovedAppointment.Add((appointment, null));
                    return emergencyAppointmentReturn;
                } else {
                    var appointmentMove = this.suggestionService.GetAppointmentMoveSuggestions(appointment);
                    appointment.id = -1;
                    appointment.patientJMBG = patientJMBG;
                    emergencyAppointmentReturn.pairsOfAppointmentAndMovedAppointment.Add((appointment, appointmentMove.First()));
                }
            }

            return emergencyAppointmentReturn;

        }
        
        private List<string> getFreeRoomIdsInNextHoure() {
            List<string> roomIds = this.roomService.GetAll()
                .Where(room => room.type != "Sala za sastanke")
                .Select(room => room.roomId)
                .ToList();

            return this.suggestionService.getFreeRoomsInInterval(roomIds, DateTime.Now, DateTime.Now.AddHours(1));
        }

    }
}
