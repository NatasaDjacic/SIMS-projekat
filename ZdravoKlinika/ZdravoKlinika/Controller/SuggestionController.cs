using ZdravoKlinika.Service;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using System;

namespace ZdravoKlinika.Controller {
    public class SuggestionController {
        SuggestionService suggestionService;
        public SuggestionController(SuggestionService suggestionService) {
            this.suggestionService = suggestionService;

        }
        public List<Appointment> getAppointmentSuggestion(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration, string priority, AppointmentType appointmentType) {
            return this.suggestionService.GetAppointmentSuggestions(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration, priority, appointmentType);
        }

        public List<Renovation> getRenovationSuggestion(string roomId, DateTime startTime, DateTime endTime, int duration) {
            return this.suggestionService.GetRenovationSuggestions(roomId, startTime, endTime, duration);
        }
    }
}
