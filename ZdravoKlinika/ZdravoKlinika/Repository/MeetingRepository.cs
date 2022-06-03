using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using System.IO;
using Newtonsoft.Json;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Repository {
    public class MeetingRepository: IMeetingRepository {
        private string fileLocation { get; set; }
        public MeetingRepository(string fileLocation) {
            this.fileLocation = fileLocation;
        }


        public List<Meeting> GetAll() {
            var values = JsonConvert.DeserializeObject<List<Meeting>>(File.ReadAllText(fileLocation));
            if (values == null) {
                values = new List<Meeting>();
            }
            return values;
        }

        public bool Save(Meeting meeting) {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => meeting.id == value.id);
            if (found != -1) {
                values[found] = meeting;
            } else {
                values.Add(meeting);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Meeting? GetById(int id) {
            var values = this.GetAll();
            return values.Find(value => id == value.id);
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
            } catch {
                return 1;
            }
        }
    }
}
