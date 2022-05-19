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
    public class CancellationRepository
    {

        private string fileLocation { get; set; }

        public CancellationRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }




        public List<Cancellation> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Cancellation>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Cancellation>();
            }
            return values;
        }

        public bool Save(Cancellation cancellation)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => cancellation.id == value.id);
            if (found != -1)
            {
                values[found] = cancellation;
            }
            else
            {
                values.Add(cancellation);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

       


        public int GenerateNewId()
        {
            return GetAll().Max(a => a.id) + 1;
        }

    }
}
