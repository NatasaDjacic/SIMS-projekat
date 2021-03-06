using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Repository
{
    public class EquipmentRepository: IEquipmentRepository
    {
        private string fileLocation { get; set; }

        public EquipmentRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }
        public List<Equipment> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Equipment>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Equipment>();
            }
            return values;
        }
        public List<Equipment> FindAll(string name)
        {
            var values = JsonConvert.DeserializeObject<List<Equipment>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Equipment>();
            }
            return values.FindAll(room => room.name.Contains(name));
        }
        public List<Equipment> GetAllDynamic()
        {
            var values = JsonConvert.DeserializeObject<List<Equipment>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Equipment>();
            }
            return GetAll().FindAll(e=>e.type == "Dinamicka");
        }

        public bool Save(Equipment equipment)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => equipment.id.Equals(value.id));
            if (found != -1)
            {
                values[found] = equipment;
            }
            else
            {
                values.Add(equipment);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Equipment? GetById(int id)
        {
            var values = this.GetAll();
            return values.Find(value => id.Equals(value.id));
        }

        public bool DeleteById(int id)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.id.Equals(id));
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0; ;
        }

        public int GenerateNewId() {
            try {
                return GetAll().Max(a => a.id) + 1;
            } catch (Exception ex) {
                return 1;
            };
        }
    }
}
