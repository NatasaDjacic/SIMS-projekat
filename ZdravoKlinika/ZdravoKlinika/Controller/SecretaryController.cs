using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class SecretaryController
    {
        public PatientService patientService;
        public SecretaryController(PatientService patientSevice)
        {
            this.patientService = patientSevice;
        }
        public bool CreateGuestAccount(string fName, string lName, string jmbg)
        {
            var patient = new Patient(fName, lName, jmbg);
            patient.ValidateGuest();
            return patientService.Create(patient);
        }

    }
}
