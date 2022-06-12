using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service {
    public class EmployeService {
        
        IDoctorRepository doctorRepository;
        IManagerRepository managerRepository;
        ISecretaryRepository secretaryRepository;

        public EmployeService(IDoctorRepository doctorRepository, IManagerRepository managerRepository, ISecretaryRepository secretaryRepository) {
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
