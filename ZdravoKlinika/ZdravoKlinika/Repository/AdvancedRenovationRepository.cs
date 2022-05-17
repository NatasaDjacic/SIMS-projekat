using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class AdvancedRenovationRepository
    {
        private string fileLocation { get; set; }
        public AdvancedRenovationRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }


        public List<AdvancedRenovation> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<AdvancedRenovation>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<AdvancedRenovation>();
            }
            return values;
        }

        public bool Save(AdvancedRenovation advRen)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => advRen.advancedRenovationId == value.advancedRenovationId);
            if (found != -1)
            {
                values[found] = advRen;
            }
            else
            {
                values.Add(advRen);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public AdvancedRenovation? GetById(int id)
        {
            var values = this.GetAll();
            return values.Find(value => id == value.advancedRenovationId);
        }

        public bool DeleteById(int id)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.advancedRenovationId == id);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
        }

        public int GenerateNewId()
        {
            try
            {
                return GetAll().Max(a => a.advancedRenovationId) + 1;
            }
            catch
            {
                return 1;
            }
        }
    }
}
