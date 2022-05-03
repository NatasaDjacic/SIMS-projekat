using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using Newtonsoft.Json;
using System.IO;

namespace ZdravoKlinika.Repository
{
    public class PrescriptionRepository
    {
        private string fileLocation { get; set; }

        public PrescriptionRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public List<Prescription> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Prescription>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Prescription>();
            }
            return values;
        }

        public bool Save(Prescription prescription)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => prescription.drugId.Equals(value.drugId));
            if (found != -1)
            {
                values[found] = prescription;
            }
            else
            {
                values.Add(prescription);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Prescription GetById(string id)
        {
            var values = this.GetAll();
            return values.Find(value => id.Equals(value.drugId));
        }

        public bool DeleteById(string id)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.drugId.Equals(id));
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0; ;
        }

    }
}
