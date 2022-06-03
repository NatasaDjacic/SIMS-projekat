

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Repository
{
   public class DoctorRepository: IDoctorRepository
   {
      private string fileLocation{ get; set; }

      public DoctorRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }
      
      public List<Doctor> GetAll()
      {
         var values = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Doctor>();
            }
            return values;
      }
      
      public bool Save(Doctor doctor)
      {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => doctor.JMBG.Equals(value.JMBG));
            if (found != -1)
            {
                values[found] = doctor;
            }
            else
            {
                values.Add(doctor);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }
      
      public Doctor? GetById(string id)
      {
         var values = this.GetAll();
         return values.Find(value => id.Equals(value.JMBG));
      }
      
      public bool DeleteById(string id)
      {
         throw new NotImplementedException();
      }
   
   }
}