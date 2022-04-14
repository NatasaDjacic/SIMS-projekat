using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Controller {
    public class AppointmentController {

        public AppointmentService appointmentService;
        public DoctorService doctorService;
        

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



            return appointmentService.SaveAppointment(appointment);

        }

    }
}