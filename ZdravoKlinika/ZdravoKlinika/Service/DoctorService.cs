using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
   public class DoctorService
   {

      public DoctorRepository doctorRepository { get; set; }

        public PatientService(DoctorRepository doctorRepository)
        {
            this.doctorRepository = doctorRepository;
        }
      public List<Doctor> GetAll()
      {
         return this.doctorRepository.GetAll();
      }
      
      public bool Save(Doctor doctor)
      {
         throw new NotImplementedException();
      }
      
      public Doctor GetById(string id)
      {
         return this.doctorRepository.GetById(id);
      }
      
      public bool DeleteById(string id)
      {
         throw new NotImplementedException();
      }
      
     
   
   }
}