using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class PatientService
    {
        public PatientRepository patientRepository { get; set; }

        public PatientService(PatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }
        public bool Create(Patient patient)
        {
            if (this.patientRepository.GetById(patient.JMBG) is null)
            {
                return this.patientRepository.Save(patient); ;
            }
            return false;
        }
        public bool Update(Patient patient)
        {
            if (this.patientRepository.GetById(patient.JMBG) is not null)
            {
                return !this.patientRepository.Save(patient);
            }
            return false;
        }

        public bool Delete(string jmbg)
        {
            return this.patientRepository.DeleteById(jmbg);
        }

        public Patient? GetById(string jmbg)
        {
            return this.patientRepository.GetById(jmbg);
        }

        public List<Patient> GetAll()
        {
            return this.patientRepository.GetAll();
        }

    }
}
