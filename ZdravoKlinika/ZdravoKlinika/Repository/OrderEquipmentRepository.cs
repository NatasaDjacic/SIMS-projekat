using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using Newtonsoft.Json;
using System.IO;

namespace ZdravoKlinika.Repository {
    public class OrderEquipmentRepository {
        private string fileLocation { get; set; }

        public OrderEquipmentRepository(string fileLocation) {
            this.fileLocation = fileLocation;
        }
        public List<OrderEquipment> GetAll() {
            var values = JsonConvert.DeserializeObject<List<OrderEquipment>>(File.ReadAllText(fileLocation));
            if (values == null) {
                values = new List<OrderEquipment>();
            }
            return values;
        }

        public bool Save(OrderEquipment equipment) {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => equipment.id.Equals(value.id));
            if (found != -1) {
                values[found] = equipment;
            } else {
                values.Add(equipment);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public OrderEquipment? GetById(int id) {
            var values = this.GetAll();
            return values.Find(value => id.Equals(value.id));
        }

        public bool DeleteById(int id) {
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
