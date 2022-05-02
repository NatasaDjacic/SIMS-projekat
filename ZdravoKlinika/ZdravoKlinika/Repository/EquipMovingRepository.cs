using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using Newtonsoft.Json;

namespace ZdravoKlinika.Repository
{
    public class EquipMovingRepository
    {
        private string fileLocation { get; set; }

        public EquipMovingRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public List<EquipMoving> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<EquipMoving>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<EquipMoving>();
            }
            return values;
        }

        public bool Save(EquipMoving equipMoving)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => equipMoving.id == value.id);
            if (found != -1)
            {
                values[found] = equipMoving;
            }
            else
            {
                values.Add(equipMoving);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public EquipMoving? GetById(int id)
        {
            var values = this.GetAll();
            return values.Find(value => id == value.id);
        }

        public bool DeleteById(int id)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.id == id);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
        }

        public int GenerateNewId()
        {
            try
            {
                return GetAll().Max(a => a.id) + 1;
            }
            catch (Exception ex)
            {
                return 1;
            };
        }

    }
}
