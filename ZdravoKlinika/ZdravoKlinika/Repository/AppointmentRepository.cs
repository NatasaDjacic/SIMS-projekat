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
   public class AppointmentRepository
   {
      private string fileLocation{ get; set; }

      public AppointmentRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

      
      public List<Appointment> GetAll()
      {
         var values = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Appointment>();
            }
            return values;
      }
      
      public bool Save(Appointment appointment)
      {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => appointment.id=value.id);
            if (found != -1)
            {
                values[found] = appointment;
            }
            else
            {
                values.Add(appointment);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
      }
      
      public Appointment GetById(int id)
      {
            var values = this.GetAll();
            return values.Find(value => id=value.id);
      }
      
      public bool DeleteById(int id)
      {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.id=id);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
      }
      
      public int GenerateNewId()
      {
         return GetAll().Max(a=> a.id)+1;

      }
   
   }
}