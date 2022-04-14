
using System;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Model {
    public class Appointment {
        public int id { get; set; }
        public DateTime startTime { get; set; }
        public int duration { get; set; }
        public bool urgency { get; set; }
        public AppointmentType appointmentType { get; set; }
        public string patientJMBG { get; set; }
        public string doctorJMBG { get; set; }
        public string roomId { get; set; }


        public Appointment() { }

        public Appointment(int id, DateTime startTime, int duration, bool urgency, AppointmentType appointmentType, string patientJMBG, string doctorJMBG, string roomId) {
            this.id = id;
            this.startTime = startTime;
            this.duration = duration;
            this.urgency = urgency;
            this.appointmentType = appointmentType;
            this.patientJMBG = patientJMBG;
            this.doctorJMBG = doctorJMBG;
            this.roomId = roomId;
        }
    }
}