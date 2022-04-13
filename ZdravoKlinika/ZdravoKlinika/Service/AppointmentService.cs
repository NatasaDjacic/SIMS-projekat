using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
   public class AppointmentService
   {

        public AppointmentRepository appointmentRepository { get; set; }

        public AppointmentService(AppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }     


      public List<Appoinment> GetAllAppointments()
      {
         return this.appointmentRepository.GetAll();
      }
      
      public bool SaveAppointment(Appointment appointment)
      {
            if (this.appointmentRepository.GetById(appointment.id) is null)
            {
                return this.appointmentRepository.Save(appointment); 
            }
            return false;
      }
      
      public Appointment GetAppointmentById(int id)
      {
         return this.appointmentRepository.GetById(id);
      }
      
      public bool DeleteAppointmentById(int id)
      {
         retur this.appointmentRepository.DeleteById(id);
      }
      
      public bool MoveAppointmentById(int id, DateTime newtime)
      {
             var old=this.appointmentRepository.GetById(id);
             if (old is null)
            {
                return false;
            }


            if(DateTime.Compare(old.StartTime.AddDays(4),newtime) <0 )
            {
                return false;
            }

            old.StartTime=newtime;
            return this.appointmentRepository.Save(old);

            
      }
      
   
   }
}