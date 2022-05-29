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
    public class RoomRepository
    {
        private string fileLocation { get; set; }

        public RoomRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public List<DoctorsMarkDTO> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<DoctorsMarkDTO>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<DoctorsMarkDTO>();
            }
            return values;
        }

        public bool Save(DoctorsMarkDTO room)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => room.roomId.Equals(value.roomId));
            if (found != -1)
            {
                values[found] = room;
            }
            else
            {
                values.Add(room);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public DoctorsMarkDTO? GetById(string id)
        {
            var values = this.GetAll();
            return values.Find(value => id.Equals(value.roomId));
        }

        public bool DeleteById(string id)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.roomId.Equals(id));
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0; ;
        }
    }
}
