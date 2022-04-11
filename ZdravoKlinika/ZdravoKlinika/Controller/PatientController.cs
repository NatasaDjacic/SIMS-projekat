using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class PatientController
    {
        public PatientService patientService;
        public PatientController(PatientService patientSevice)
        {
            this.patientService = patientSevice;
        }

        public bool Create(string fName, string lName, string jmbg, string phone, string email, string country, string city, string address, Gender gender, BloodType bloodType, List<string> allergens)
        {
            var patient = new Patient(fName, lName, jmbg, null, phone, email, country, city, address, gender, bloodType, allergens);
            return this.patientService.Create(patient);
        }

        public bool Delete(string jmbg)
        {
            return this.patientService.Delete(jmbg);
        }

        public bool Update(Patient patient)
        {
            return this.patientService.Update(patient);
        }

        public Patient? GetById(string jmbg)
        {
            return this.patientService.GetById(jmbg);
        }

        public List<Patient> GetAll()
        {
            return this.patientService.GetAll();
        }

    }
}
