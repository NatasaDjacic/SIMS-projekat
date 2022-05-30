using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Service {
    public class EmployeService {

        DoctorRepository doctorRepository;
        ManagerRepository managerRepository;
        SecretaryRepository secretaryRepository;

        public EmployeService(DoctorRepository doctorRepository, ManagerRepository managerRepository, SecretaryRepository secretaryRepository) {
            this.doctorRepository = doctorRepository;
            this.managerRepository = managerRepository;
            this.secretaryRepository = secretaryRepository;
        }

        public List<User> GetAll() {
            List<User> employees = new List<User>();
            employees.AddRange(doctorRepository.GetAll());
            employees.AddRange(managerRepository.GetAll());
            employees.AddRange(secretaryRepository.GetAll());
            return employees;
        }

        public string GetEmployeRole(string employeJMBG) {
            if(doctorRepository.GetById(employeJMBG) != null) {
                return ROLE.DOCTOR;
            }
            if (managerRepository.GetById(employeJMBG) != null) {
                return ROLE.MANAGER;
            }
            if (secretaryRepository.GetById(employeJMBG) != null) {
                return ROLE.SECRETARY;
            }
            return ROLE.NONE;
        }


        
    }
}
