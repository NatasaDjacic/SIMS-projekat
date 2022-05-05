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
   public class RenovationRepository
   {
      private string fileLocation{ get; set; }

      public RenovationRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

      
      public List<Renovation> GetAll()
      {
         var values = JsonConvert.DeserializeObject<List<Renovation>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Renovation>();
            }
            return values;
      }
      
      public bool Save(Renovation ren)
      {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => ren.renovationId==value.renovationId);
            if (found != -1)
            {
                values[found] = ren;
            }
            else
            {
                values.Add(ren);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
      }
      
      public Renovation? GetById(int id)
      {
            var values = this.GetAll();
            return values.Find(value => id==value.renovationId);
      }
      
      public bool DeleteById(int id)
      {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.renovationId==id);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
      }
      
      public int GenerateNewId()
      {
            try
            {
                return GetAll().Max(a => a.renovationId) + 1;
            }
            catch
            {
                return 1;
            }
      }
      /*public string DescriptionAdd(string description, int id)
        {
            var value = this.GetById(id);
            value.description = description;
            return null;
        }*/


   }
}