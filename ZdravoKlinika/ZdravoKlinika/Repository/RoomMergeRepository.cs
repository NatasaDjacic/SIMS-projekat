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
    public class RoomMergeRepository: IRoomMergeRepository
    {
        private string fileLocation { get; set; }
        public RoomMergeRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }


        public List<RoomMerge> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<RoomMerge>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<RoomMerge>();
            }
            return values;
        }

        public bool Save(RoomMerge roomMerge)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => roomMerge.id == value.id);
            if (found != -1)
            {
                values[found] = roomMerge;
            }
            else
            {
                values.Add(roomMerge);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public RoomMerge? GetById(int id)
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
