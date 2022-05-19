using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Service
{
    public class AuthService
    {
        public static class ROLE { 
            public static readonly string NONE = "none";
            public static readonly string PATIENT = "patient";
            public static readonly string DOCTOR = "doctor";
            public static readonly string MANAGER = "manager";
            public static readonly string SECRETARY = "secretary";
        }
        public User? user { get; set; }
        public string user_role { get; set; }
        public bool restricted;
        PatientRepository patientRepository;
        DoctorRepository doctorRepository;
        ManagerRepository managerRepository;
        SecretaryRepository secretaryRepository;

        public AuthService(PatientRepository pr, DoctorRepository dr, ManagerRepository mr, SecretaryRepository sr)
        {
            user = null;
            user_role = ROLE.NONE;
            restricted = false;
            patientRepository = pr;
            doctorRepository = dr;
            managerRepository = mr;
            secretaryRepository = sr;
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
