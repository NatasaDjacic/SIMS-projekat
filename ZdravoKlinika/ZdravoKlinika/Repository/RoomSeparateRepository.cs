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
    public class RoomSeparateRepository
    {
        private string fileLocation { get; set; }
        public RoomSeparateRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }


        public List<RoomSeparate> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<RoomSeparate>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<RoomSeparate>();
            }
            return values;
        }

        public bool Save(RoomSeparate advRen)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => advRen.id == value.id);
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

        public RoomSeparate? GetById(int id)
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
            catch
            {
                return 1;
            }
        }
    }
}
