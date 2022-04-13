using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
   public class AppointmentController   {
      
      public AppointmentService appointmentService=new AppointmentService();
      

      public List<Appoinment> GetAllAppointments()
      {
         return appointmentService.GetAllAppointments();
      }
      
  
      public Appointment GetAppointmentById(int id)
      {
         return appointmentService.GetAppointmentById(id);
      }
      
      public bool DeleteAppointmentById(int id)
      {
         return appointmentService.DeleteAppointmentById(id);
      }
      
      public int MoveAppointmentById(int id, DateTime time)
      {
         return appointmentService.MoveAppointmentById(id, time);
      }
   
   }
}