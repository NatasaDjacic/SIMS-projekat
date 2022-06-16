using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.DTO;
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

            if( cancellationService.CheckNumOfCancelledAppointments()==false) { return false; }
            cancellationService.SaveNewCancellation();

            return this.appointmentRepository.DeleteById(id);
        }

        public bool CheckConditionsForMovingAppointment(Appointment appointment,DateTime newtime)
        {
            if (appointment is null)
            {
                return false;
            }

            if (DateTime.Compare(appointment.startTime.AddDays(-1), DateTime.Now) < 0)
            {
                return false;
            }

            if (DateTime.Compare(appointment.startTime.AddDays(4), newtime) < 0)
            {
                return false;
            }

            return true;
        }

        public bool MoveAppointmentById(int id, DateTime newtime) {
            var appointment = this.appointmentRepository.GetById(id);
            
            if(this.CheckConditionsForMovingAppointment(appointment,newtime) == false) { return false; }

            appointment.startTime = newtime;

            cancellationService.SaveNewCancellation();
            return !this.appointmentRepository.Save(appointment);
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


        // metoda kreirana preko DTO zbog lakseg prikaza na WPF-u za HCI-ja
        public List<AppointmentDTO> GetDTOAppointmentsByPatient(string patientJMBG)
        {
            var allAppointmentsByPatient = this.GetAllAppointments().Where(a => a.patientJMBG == patientJMBG).ToList();
            List<AppointmentDTO> DTOappointments = new List<AppointmentDTO>();
            foreach (var appointment in allAppointmentsByPatient)
            {
                var appointmentDTO = new AppointmentDTO(appointment.id, appointment.startTime, appointment.doctorJMBG, appointment.roomId, this.doctorService.GetById(appointment.doctorJMBG).firstName, this.doctorService.GetById(appointment.doctorJMBG).lastName);
                DTOappointments.Add(appointmentDTO);

            }

            return DTOappointments;

        }

    }
}