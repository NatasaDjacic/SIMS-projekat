using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class CancellationService
    {

        private CancellationRepository cancellationRepository { get; set; }
        
        public CancellationService(CancellationRepository cancellationRepository)
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


            List<Cancellation> all = this.cancellationRepository.GetAll();
            List<Cancellation> cancellationList = new List<Cancellation>(); 
            foreach (Cancellation c in all)
            {
                if(c.patientJMBG==patientJMBG && c.date.CompareTo(dateTime.AddDays(-30))>=0 )
                {
                    cancellationList.Add(c);

                }
            }

            

            return cancellationList.Count;

        }

        public int GenerateNewId()
        {
            return this.cancellationRepository.GenerateNewId();
        }

    }
}
