using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class DrugRepository
    {
        private string fileLocation { get; set; }

        public DrugRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public List<Drug> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Drug>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Drug>();
            }
            return values;
        }

        public bool Save(Drug report)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => report.drugId == value.drugId);
            if (found != -1)
            {
                values[found] = report;
            }
            else
            {
                values.Add(report);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Drug? GetById(int id)
        {
            var values = this.GetAll();
            return values.Find(value => id == value.drugId);
        }

        public bool DeleteById(int id)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.drugId == id);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0; ;
        }
        public int GenerateNewId() {
            return GetAll().Max(a => a.drugId) + 1;
        }
    }
}
