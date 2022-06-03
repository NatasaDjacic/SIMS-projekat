using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service
{
    public class PatientReminderService
    {
        private IPatientReminderRepository patientReminderRepository { get; set; }

        public PatientReminderService(IPatientReminderRepository patientReminderRepository)
        {
            this.patientReminderRepository = patientReminderRepository;

        }

        public List<PatientReminder> GetAll()
        {
            return this.patientReminderRepository.GetAll();
        }

        public bool Save(PatientReminder patientReminder)
        {

            return this.patientReminderRepository.Save(patientReminder);

        }

        public int GenerateNewId()
        {
            return this.patientReminderRepository.GenerateNewId();
        }

        public List<PatientReminder> GetByPatientJMBG(string patientJMBG)
        {
            var allPatientReminders = this.GetAll();
            List<PatientReminder> remindersForCertainPatient = new List<PatientReminder>();
            foreach(PatientReminder patientReminder in allPatientReminders)
            {
                if(patientReminder.patientJMBG==patientJMBG)
                { remindersForCertainPatient.Add(patientReminder); }
            }
            return remindersForCertainPatient;
        }
    }
}
