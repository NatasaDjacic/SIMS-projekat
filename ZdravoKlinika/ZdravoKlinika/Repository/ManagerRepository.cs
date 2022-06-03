using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using Newtonsoft.Json;
using System.IO;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Repository
{
    public class ManagerRepository: IManagerRepository
    {
        private string fileLocation { get; set; }

        public ManagerRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public List<Manager> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Manager>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Manager>();
            }
            return values;
        }

        public bool Save(Manager manager)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => manager.JMBG.Equals(value.JMBG));
            if (found != -1)
            {
                values[found] = manager;
            }
            else
            {
                values.Add(manager);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Manager? GetById(string id)
        {
            var values = this.GetAll();
            return values.Find(value => id.Equals(value.JMBG));
        }
        public Manager? GetManager()
        {
            var values = this.GetAll();
            return values.Find(value => value.JMBG.Equals(value.JMBG));
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
