using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Service
{
    public class AuthService
    {
        
        public User? user { get; set; }
        public string user_role { get; set; }
        public bool restricted;
        IPatientRepository patientRepository;
        IDoctorRepository doctorRepository;
        IManagerRepository managerRepository;
        ISecretaryRepository secretaryRepository;

        public AuthService(IPatientRepository patientRepository, IDoctorRepository doctorRepository, IManagerRepository managerRepository, ISecretaryRepository secretaryRepository)
        {
            user = null;
            user_role = ROLE.NONE;
            restricted = false;
            this.patientRepository = patientRepository;
            this.doctorRepository = doctorRepository;
            this.managerRepository = managerRepository;
            this.secretaryRepository = secretaryRepository;
        }

        public bool Login(string username, string password)
        {
            Predicate<User> loginFN = x => (x.username == username || x.JMBG == username) && x.password == password;
            user = secretaryRepository.GetAll().Find(loginFN);

            if(restricted==true)
            {
                Console.WriteLine("Account restricted");
                return false;
            }

            if(user != null)
            {
                user_role = ROLE.SECRETARY;
                return true;
            }
            user = managerRepository.GetAll().Find(loginFN);
            if (user != null)
            {
                user_role = ROLE.MANAGER;
                return true;
            }
            user = doctorRepository.GetAll().Find(loginFN);
            if (user != null)
            {
                user_role = ROLE.DOCTOR;
                return true;
            }
            user = patientRepository.GetAll().Find(loginFN);
            if (user != null)
            {
                user_role = ROLE.PATIENT;
                return true;
            }
            return false;
        }
        public void Logout()
        {
            user = null;
            user_role = ROLE.NONE;
            
        }

        public void Restrict()
        {
            user = null;
            user_role = ROLE.NONE;
            restricted = true;
        }
    }
}
