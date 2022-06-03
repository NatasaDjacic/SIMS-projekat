using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using Newtonsoft.Json;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Repository
{
    public class PatientReminderRepository: IPatientReminderRepository
    {
        private string fileLocation { get; set; }

        public PatientReminderRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public List<PatientReminder> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<PatientReminder>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<PatientReminder>();
            }
            return values;
        }

        public bool Save(PatientReminder patientReminder)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => patientReminder.id == value.id);
            if (found != -1)
            {
                values[found] = patientReminder;
            }
            else
            {
                values.Add(patientReminder);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public int GenerateNewId()
        {
            return GetAll().Max(a => a.id) + 1;
        }

        public PatientReminder? GetById(int id) {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id) {
            throw new NotImplementedException();
        }
    }
}
