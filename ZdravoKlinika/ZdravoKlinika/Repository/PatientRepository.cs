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
    public class PatientRepository: IPatientRepository
    {
        private string fileLocation { get; set; }

        public PatientRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;   
        }

        public List<Patient> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Patient>();
            }
            return values;
        }

        public bool Save(Patient patient)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => patient.JMBG.Equals(value.JMBG));
            if (found != -1)
            {
                values[found] = patient;
            }
            else
            {
                values.Add(patient);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Patient? GetById(string id)
        {
            var values = this.GetAll();
            return values.Find(value => id.Equals(value.JMBG));
        }

        public bool DeleteById(string id)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.JMBG.Equals(id));
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
        }
    }
}
