using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service
{
    public class CancellationService
    {

        private ICancellationRepository cancellationRepository { get; set; }
        private AuthService authService { get; set; }
        
        public CancellationService(ICancellationRepository cancellationRepository, AuthService authService)
        {
            this.cancellationRepository = cancellationRepository;
            this.authService = authService;
            
        }

        public List<Cancellation> GetAllCancellations()
        {
            return this.cancellationRepository.GetAll();
        }

        public bool SaveCancellation(Cancellation cancellation)
        {

            return this.cancellationRepository.Save(cancellation);

        }

        public int CountingNumOfCancelledAppointments(List<Cancellation> allCancellations, string patientJMBG, DateTime dateTime)
        {
            int numberOfCancelledAppointments = 0;
            foreach (Cancellation c in allCancellations)
            {
                if (c.patientJMBG == patientJMBG && c.date.CompareTo(dateTime.AddDays(-30)) >= 0)
                {
                    numberOfCancelledAppointments = numberOfCancelledAppointments + 1;

                }
            }
            return numberOfCancelledAppointments;
        }

        public int GetCancellationNumber(string patientJMBG,DateTime dateTime)
        {
            List<Cancellation> allCancellations = this.cancellationRepository.GetAll();
            return this.CountingNumOfCancelledAppointments(allCancellations,patientJMBG,dateTime);

        }
       
        public int GenerateNewId()
        {
            return this.cancellationRepository.GenerateNewId();
        }


        public bool CheckNumOfCancelledAppointments()
        {
            int numberOfCancelledAppointments = this.GetCancellationNumber(authService.user.JMBG, DateTime.Now);

            if (numberOfCancelledAppointments >= 4)
            {
                authService.Restrict();
                Console.WriteLine("RESTRICTED!");
                return false;
            }
            return true;

        }

        public void SaveNewCancellation()
        {
            Cancellation cancellation = new Cancellation(this.GenerateNewId(), authService.user.JMBG, DateTime.Now);
            this.SaveCancellation(cancellation);


        }

    }
}
