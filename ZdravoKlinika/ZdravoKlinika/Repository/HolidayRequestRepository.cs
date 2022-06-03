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
    public class HolidayRequestRepository: IHolidayRequestRepository  {
        private string fileLocation { get; set; }
        public HolidayRequestRepository(string fileLocation) {
            this.fileLocation = fileLocation;
        }


        public List<HolidayRequest> GetAll() {
            var values = JsonConvert.DeserializeObject<List<HolidayRequest>>(File.ReadAllText(fileLocation));
            if (values == null) {
                values = new List<HolidayRequest>();
            }
            return values;
        }

        public bool Save(HolidayRequest holidayRequest) {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => holidayRequest.id == value.id);
            if (found != -1) {
                values[found] = holidayRequest;
            } else {
                values.Add(holidayRequest);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public HolidayRequest? GetById(int id) {
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
