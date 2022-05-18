using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model.DTO {
    public class EmergencyAppointmentSuggestionDTO {

        public bool found { get; set; }
        public List<(Appointment, Appointment?)> pairsOfAppointmentAndMovedAppointment { get; set; }

        public EmergencyAppointmentSuggestionDTO() { 
            this.pairsOfAppointmentAndMovedAppointment = new List<(Appointment, Appointment?)> ();
        }
        public EmergencyAppointmentSuggestionDTO(bool found, (Appointment,Appointment?) pairOfAppointmentAndMovedAppointment) {
            this.found = found;
            this.pairsOfAppointmentAndMovedAppointment = new List<(Appointment, Appointment?)>();
            this.pairsOfAppointmentAndMovedAppointment.Add(pairOfAppointmentAndMovedAppointment);
        }
    }
}
