using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service {
    public class AppointmentService {

        public IAppointmentRepository appointmentRepository { get; set; }

        public DoctorService doctorService { get; set; }
        public CancellationService cancellationService { get; set; }
        public AuthService authService { get; set; }

        public AppointmentService(IAppointmentRepository appointmentRepository, DoctorService doctorService,CancellationService cancellationService,AuthService authService) {
            this.appointmentRepository = appointmentRepository;
            this.doctorService = doctorService;
            this.cancellationService = cancellationService;
            this.authService = authService;
        }

       
       
        
       

        public List<Appointment> GetAllAppointments() {
            return this.appointmentRepository.GetAll();
        }

        public List<Appointment> GetAllInInterval(DateTime start, DateTime end) {
            return this.GetAllAppointments().Where(a => (a.endTime >= start && a.startTime <= end)).ToList();
        }

        public bool SaveAppointment(Appointment appointment) {
          
            return this.appointmentRepository.Save(appointment);
            
        }

        public Appointment? GetAppointmentById(int id) {
            return this.appointmentRepository.GetById(id);
        }

        public bool DeleteAppointmentById(int id) {
            
            int number = cancellationService.GetCancellationNumber(authService.user.JMBG, DateTime.Now);

            if (number >= 4)
            {
                authService.Restrict();
                Console.WriteLine("RESTRICTED!");
                return false;
            }

            Cancellation cancellation = new Cancellation(cancellationService.GenerateNewId(), authService.user.JMBG, DateTime.Now);
            cancellationService.SaveCancellation(cancellation);

            return this.appointmentRepository.DeleteById(id);
        }

        public bool MoveAppointmentById(int id, DateTime newtime) {
            var old = this.appointmentRepository.GetById(id);
            if (old is null) {
                return false;
            }

            if(DateTime.Compare(old.startTime.AddDays(-1), DateTime.Now)<0)
            {
                return false;
            }

            if (DateTime.Compare(old.startTime.AddDays(4), newtime) < 0) {
                return false;
            }

            old.startTime = newtime;

            int number = cancellationService.GetCancellationNumber(authService.user.JMBG, DateTime.Now);

            if(number>=4) 
            {   authService.Restrict(); 
                Console.WriteLine("RESTRICTED!");
                return false; 
            }

            Cancellation cancellation = new Cancellation(cancellationService.GenerateNewId(),authService.user.JMBG, DateTime.Now);
            cancellationService.SaveCancellation(cancellation);
            
            return !this.appointmentRepository.Save(old);


        }

        public Appointment? GetDoctorsNextAppointment(string doctorJMBG) {
            var appointments = this.GetAllAppointments().Where(appointment => appointment.doctorJMBG == doctorJMBG && appointment.endTime >= DateTime.Now).ToList();
            appointments.Sort((a, b) => a.startTime.CompareTo(b.startTime));
            return appointments.FirstOrDefault();
        }

        public int GenerateNewId()
        {
            return this.appointmentRepository.GenerateNewId();
        }
    }
}