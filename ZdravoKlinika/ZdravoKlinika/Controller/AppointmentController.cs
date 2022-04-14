using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Controller {
    public class AppointmentController {

        public AppointmentService appointmentService;
        public DoctorService doctorService = new DoctorService();
        

        public AppointmentController(AppointmentService appointmentService) {
            this.appointmentService = appointmentService;
        }


        public List<Appointment> GetAllAppointments() {
            return appointmentService.GetAllAppointments();
        }


        public Appointment GetAppointmentById(int id) {
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

            //var doctor = doctorService.GetById(doctorJMBG);
            //var roomId = doctor.roomId;

            Appointment appointment = new Appointment();
            appointment.startTime = startTime;
            appointment.patientJMBG = "1231231231231";
            appointment.doctorJMBG = doctorJMBG;
            appointment.roomId = "5";
            appointment.urgency = false;
            appointment.appointmentType = AppointmentType.regular;
            appointment.duration = duration;
            appointment.id = appointmentService.GenerateNewId();



            return appointmentService.SaveAppointment(appointment);

        }

    }
}