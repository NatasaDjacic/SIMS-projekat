using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository {
    public class NotificationRepository {
        private string fileLocation { get; set; }

        public NotificationRepository(string fileLocation) {
            this.fileLocation = fileLocation;
        }

        public List<Notification> GetAll() {
            var values = JsonConvert.DeserializeObject<List<Notification>>(File.ReadAllText(fileLocation));
            if (values == null) {
                values = new List<Notification>();
            }
            return values;
        }

        public bool Save(Notification notification) {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => notification.id == (value.id));
            if (found != -1) {
                values[found] = notification;
            } else {
                values.Add(notification);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Notification? GetById(int id) {
            var values = this.GetAll();
            return values.Find(value => id == (value.id));
        }

        public bool DeleteById(int id) {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.id == id);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
        }
        public int GenerateNewId() {
            try { 
                return GetAll().Max(a => a.id) + 1;
            }catch(Exception ex) {
                return 1;
            }
        }
    }
}
