using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Controller {
    public class AppointmentController {

        private AppointmentService appointmentService;
        private DoctorService doctorService;
        private AuthService authService;
        private SuggestionService suggestionService;
        public AppointmentController(AppointmentService appointmentService, DoctorService doctorService, AuthService authService, SuggestionService suggestionService) {
            this.appointmentService = appointmentService;
            this.authService = authService;
            this.doctorService = doctorService;
            this.suggestionService = suggestionService;
        }


        public List<Appointment> GetAllAppointments() {
            return appointmentService.GetAllAppointments();
        }


        public Appointment? GetAppointmentById(int id) {
            return appointmentService.GetAppointmentById(id);
        }

        public bool DeleteAppointmentById(int id) {
            return appointmentService.DeleteAppointmentById(id);
        }

        public bool MoveAppointmentById(int id, DateTime time) {
            return appointmentService.MoveAppointmentById(id, time);
        }

        public bool CreateAppointmentPatient(DateTime startTime, int duration, string doctorJMBG)
        {

            var doctor = doctorService.GetById(doctorJMBG);

            if (doctor == null) throw new Exception("Doctor not found");
            if (authService.user == null) throw new Exception("Not logged in");
            if (authService.user_role != AuthService.ROLE.PATIENT) throw new Exception("Not patient role");
            

            Appointment appointment = new Appointment();
            appointment.startTime = startTime;
            // Get from authService
            appointment.patientJMBG = authService.user.JMBG;
            appointment.doctorJMBG = doctorJMBG;
            appointment.roomId = doctor.roomId;
            appointment.urgency = false;
            appointment.appointmentType = AppointmentType.regular;
            appointment.duration = duration;
            appointment.id = appointmentService.GenerateNewId();

            appointment.Validate();


            return appointmentService.SaveAppointment(appointment);

        }
        public List<Appointment> GetDoctorAppointments() {
            if (authService.user == null) throw new Exception("Not logged in");
            if (authService.user_role != AuthService.ROLE.DOCTOR) throw new Exception("Not doctore role");
            return this.appointmentService.GetAllAppointments().FindAll(a => a.doctorJMBG.Equals(authService.user.JMBG));

        }

        public bool CreateAppointmentDoctor(DateTime startTime, int duration, string patientJMBG, string doctorJMBG, string roomId, bool urgency, AppointmentType appointmentType) {
            Appointment app = new Appointment(appointmentService.GenerateNewId(), startTime, duration, urgency, appointmentType, patientJMBG, doctorJMBG, roomId);
            return this.appointmentService.SaveAppointment(app);
        }

        public bool IsDoctorAppointment(int id) {
            if (authService.user == null) throw new Exception("Not logged in");
            if (authService.user_role != AuthService.ROLE.DOCTOR) throw new Exception("Not doctore role");
            var app = this.appointmentService.GetAppointmentById(id);
            return app is not null && app.doctorJMBG.Equals(authService.user.JMBG);
        }

        public List<Appointment> getSuggestions(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration, string priority, AppointmentType appointmentType) {
            return this.suggestionService.GetAppointmentSuggestions(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration, priority, appointmentType);
        }
        


    }
}