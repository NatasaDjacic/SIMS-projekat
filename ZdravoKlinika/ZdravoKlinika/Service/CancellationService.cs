using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service
{
    public class CancellationService
    {

        private ICancellationRepository cancellationRepository { get; set; }
        
        public CancellationService(ICancellationRepository cancellationRepository)
        {
            this.cancellationRepository = cancellationRepository;
            
        }

        public List<Cancellation> GetAllCancellations()
        {
            return this.cancellationRepository.GetAll();
        }

        public bool SaveCancellation(Cancellation cancellation)
        {

            return this.cancellationRepository.Save(cancellation);

        }

        public int GetCancellationNumber(string patientJMBG,DateTime dateTime)
        {
            List<Cancellation> allCancellations = this.cancellationRepository.GetAll();
            int numberOfCancelledAppointments=0;            
            foreach (Cancellation c in allCancellations)
            {
                if(c.patientJMBG==patientJMBG && c.date.CompareTo(dateTime.AddDays(-30))>=0 )
                {
                    numberOfCancelledAppointments = numberOfCancelledAppointments + 1;

                }
            }

            return numberOfCancelledAppointments;

        }
       
        public int GenerateNewId()
        {
            return this.cancellationRepository.GenerateNewId();
        }

    }
}
