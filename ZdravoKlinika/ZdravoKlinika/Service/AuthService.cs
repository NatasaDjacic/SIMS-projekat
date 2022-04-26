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
            public static string NONE = "none";
            public static string PATIENT = "patient";
            public static string DOCTOR = "doctor";
            public static string MANAGER = "manager";
            public static string SECRETARY = "secretary";
        }
        public User? user { get; set; }
        public string user_role { get; set; }
        PatientRepository patientRepository;
        DoctorRepository doctorRepository;
        ManagerRepository managerRepository;
        SecretaryRepository secretaryRepository;

        public AuthService(PatientRepository pr, DoctorRepository dr, ManagerRepository mr, SecretaryRepository sr)
        {
            user = null;
            user_role = ROLE.NONE;
            patientRepository = pr;
            doctorRepository = dr;
            managerRepository = mr;
            secretaryRepository = sr;
        }

        public bool login(string username, string password)
        {
            Predicate<User> loginFN = x => (x.username == username || x.JMBG == username) && x.password == password;
            user = secretaryRepository.GetAll().Find(loginFN);
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
        public void logout()
        {
            user = null;
            user_role = ROLE.NONE;
        }
    }
}
