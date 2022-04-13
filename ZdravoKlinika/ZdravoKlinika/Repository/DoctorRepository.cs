

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
   public class DoctorRepository
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
         throw new NotImplementedException();
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