using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller {
    public class AppointmentController {

        public AppointmentService appointmentService;

        public AppointmentController(AppointmentService appointmentService) {
            this.appointmentService = appointmentService;
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

    }
}