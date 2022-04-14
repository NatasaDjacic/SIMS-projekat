using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service {
    public class DoctorService {

        public DoctorRepository doctorRepository { get; set; }

        public DoctorService(DoctorRepository doctorRepository) {
            this.doctorRepository = doctorRepository;
        }

        public DoctorService() { }
        public List<Doctor> GetAll() {
            return this.doctorRepository.GetAll();
        }

        public bool Save(Doctor doctor) {
            if (this.doctorRepository.GetById(doctor.JMBG) is null)
            {
                return this.doctorRepository.Save(doctor); ;
            }
            return false;
        }

        public Doctor? GetById(string id) {
            return this.doctorRepository.GetById(id);
        }

        public bool DeleteById(string id) {
            throw new NotImplementedException();
        }



    }
}