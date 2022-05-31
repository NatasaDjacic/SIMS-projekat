using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class PatientReminderController
    {
        public PatientReminderService patientReminderService { get; set; }

        public PatientReminderController(PatientReminderService patientReminderService)
        { 
            this.patientReminderService = patientReminderService; 
        }

        public List<PatientReminder> GetAll()
        {
            return patientReminderService.GetAll();
        }

        public bool Save(PatientReminder patientReminder)
        {

            return this.patientReminderService.Save(patientReminder);

        }

        public int GenerateNewId()
        {
            return this.patientReminderService.GenerateNewId();
        }

        public List<PatientReminder> GetByPatientJMBG(string patientJMBG)
        {
            return this.patientReminderService.GetByPatientJMBG(patientJMBG);
        }
    }
}
