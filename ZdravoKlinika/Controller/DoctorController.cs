using System;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Service;
using System.Collections.Generic;


namespace ZdravoKlinika.Controller {
    public class DoctorController {
        public DoctorService doctorService;
        public AppointmentService appointmentService;
        // Information from authService
        public readonly string DOCTORJMBG = "1111111111111";
        public DoctorController(DoctorService doctorService, AppointmentService appointmentService) {
            this.doctorService = doctorService;
            this.appointmentService = appointmentService;
        }
        public bool Create(string fName, string lName, string jmbg, string username, string phone, string email, string country, string city, string address, Gender gender, string specialization, string roomId) {
            var doctor = new Doctor(fName, lName, jmbg, username, null, phone, email, country, city, address, gender, specialization, roomId);
            return this.doctorService.Save(doctor);
        }
        public List<Appointment> GetMyAppointments() {
            return this.appointmentService.GetAllAppointments().FindAll(a => a.doctorJMBG.Equals(DOCTORJMBG));
            
        }

        public bool CreateAppointment(DateTime startTime, int duration, string patientJMBG, string doctorJMBG, string roomId, bool urgency, AppointmentType appointmentType) {
            Appointment app = new Appointment(appointmentService.GenerateNewId(), startTime, duration, urgency, appointmentType, patientJMBG, doctorJMBG, roomId);

            return this.appointmentService.SaveAppointment(app);
        }

        public bool IsMyAppointment(int id) {
            var app = this.appointmentService.GetAppointmentById(id);
            return app is not null && app.doctorJMBG.Equals(DOCTORJMBG);
        }
    }
}
