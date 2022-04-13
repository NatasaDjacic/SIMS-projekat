

using System;


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
      
      public Doctor GetById(string id)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteById(string id)
      {
         throw new NotImplementedException();
      }
   
   }
}