using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.DTO;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Controller {
    public class AppointmentController {

        AppointmentService appointmentService;
        DoctorService doctorService;
        AuthService authService;
        SuggestionService suggestionService;
        NotificationService notificationService;
        EmergencyAppointmentService emergencyAppointmentService;

        public AppointmentController(
            AppointmentService appointmentService,
            DoctorService doctorService,
            AuthService authService,
            SuggestionService suggestionService,
            NotificationService notificationService,
            EmergencyAppointmentService emergencyAppointmentService
            ) {
            this.appointmentService = appointmentService;
            this.authService = authService;
            this.doctorService = doctorService;
            this.suggestionService = suggestionService;
            this.notificationService = notificationService;
            this.emergencyAppointmentService = emergencyAppointmentService;
        }


        public List<Appointment> GetAllAppointments() {
            return appointmentService.GetAllAppointments();
        }

        public List<AppointmentDTO> GetDTOAppointmentsByPatient(string patientJMBG)
        {
            return appointmentService.GetDTOAppointmentsByPatient(patientJMBG);
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


        public Appointment CreateInstanceOfAppointment(DateTime startTime, Doctor doctor , int duration)
        {
            Appointment appointment = new Appointment();
            appointment.startTime = startTime;
            appointment.patientJMBG = authService.user.JMBG;
            appointment.doctorJMBG = doctor.JMBG;
            appointment.roomId = doctor.roomId;
            appointment.urgency = false;
            appointment.appointmentType = AppointmentType.regular;
            appointment.duration = duration;
            appointment.id = appointmentService.GenerateNewId();

            return appointment;
        }
        public bool CreateAppointmentPatient(DateTime startTime, int duration, string doctorJMBG) {

            var doctor = doctorService.GetById(doctorJMBG);

            if (doctor == null) throw new Exception("Doctor not found");
            if (authService.user == null) throw new Exception("Not logged in");
            if (authService.user_role != ROLE.PATIENT) throw new Exception("Not patient role");

            var appointment = this.CreateInstanceOfAppointment(startTime, doctor, duration);
            appointment.Validate();
            return appointmentService.SaveAppointment(appointment);

        }


        public List<Appointment> GetDoctorAppointments() {
            if (authService.user == null) throw new Exception("Not logged in");
            if (authService.user_role != ROLE.DOCTOR) throw new Exception("Not doctore role");
            return this.appointmentService.GetAllAppointments().FindAll(a => a.doctorJMBG.Equals(authService.user.JMBG));

        }

        public bool CreateAppointmentDoctor(DateTime startTime, int duration, string patientJMBG, string doctorJMBG, string roomId, bool urgency, AppointmentType appointmentType) {
            Appointment app = new Appointment(appointmentService.GenerateNewId(), startTime, duration, urgency, appointmentType, patientJMBG, doctorJMBG, roomId);
            return this.appointmentService.SaveAppointment(app);
        }

        public bool IsDoctorAppointment(int id) {
            if (authService.user == null) throw new Exception("Not logged in");
            if (authService.user_role != ROLE.DOCTOR) throw new Exception("Not doctore role");
            var app = this.appointmentService.GetAppointmentById(id);
            return app is not null && app.doctorJMBG.Equals(authService.user.JMBG);
        }

        public List<Appointment> getSuggestions(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration, string priority, AppointmentType appointmentType) {
            return this.suggestionService.GetAppointmentSuggestions(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration, priority, appointmentType);
        }


        public void CreateAppointmentFromSuggestion(Appointment app) {
            app.id = this.appointmentService.GenerateNewId();
            this.appointmentService.SaveAppointment(app);
            this.notificationService.NotificationForAppointmentCreated(app);
        }
        public void MoveAppointmentSecretary(Appointment app, DateTime newTime) {
            var old = new DateTime(app.startTime.Ticks);
            app.startTime = newTime;
            Console.WriteLine(this.appointmentService.SaveAppointment(app));
            this.notificationService.NotificationForAppointmentMoved(app, old);
        }
        public void RemoveAppointmentSecretary(Appointment app) {
            this.appointmentService.DeleteAppointmentById(app.id);
            this.notificationService.NotificationForAppointmentCancel(app);
        }
        public EmergencyAppointmentSuggestionDTO createOrSuggestEmergencyAppointment(Patient patient, string specialization) {
            return this.emergencyAppointmentService.createOrSuggestEmergencyAppointment(patient.JMBG, specialization);
        }
    }
}