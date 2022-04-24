using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Controller {
    public class AppointmentController {

        public AppointmentService appointmentService;
        public DoctorService doctorService;
        private string DOCTORJMBG = "1111111111111";

        public AppointmentController(AppointmentService appointmentService, DoctorService doctorService) {
            this.appointmentService = appointmentService;
            this.doctorService = doctorService;
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

            Appointment appointment = new Appointment();
            appointment.startTime = startTime;
            // Get from authService
            appointment.patientJMBG = "1231231231231";
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
            return this.appointmentService.GetAllAppointments().FindAll(a => a.doctorJMBG.Equals(DOCTORJMBG));

        }

        public bool CreateAppointmentDoctor(DateTime startTime, int duration, string patientJMBG, string doctorJMBG, string roomId, bool urgency, AppointmentType appointmentType) {
            Appointment app = new Appointment(appointmentService.GenerateNewId(), startTime, duration, urgency, appointmentType, patientJMBG, doctorJMBG, roomId);

            return this.appointmentService.SaveAppointment(app);
        }

        public bool IsDoctorAppointment(int id) {
            var app = this.appointmentService.GetAppointmentById(id);
            return app is not null && app.doctorJMBG.Equals(DOCTORJMBG);
        }

        public List<Appointment> getSuggestions(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration, string priority, AppointmentType appointmentType) {
            return this.appointmentService.GetAppointmentSuggestions(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration, priority, appointmentType);
        }
        


    }
}