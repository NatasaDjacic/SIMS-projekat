using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class PrescriptionService
    {
        public PatientRepository patientRepository;
        public PrescriptionRepository prescriptionRepository { get; set; }

        public bool Create(Prescription prescription)
        {
            if(patientRepository == null)
            {
                return this.prescriptionRepository.Save(prescription);
            }
            return false;
        }
    }
}
