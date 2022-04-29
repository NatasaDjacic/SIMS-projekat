using System;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Service;
using System.Collections.Generic;


namespace ZdravoKlinika.Controller {
    public class DoctorController {
        public DoctorService doctorService;
        // Information from authService
        public readonly string DOCTORJMBG = "1111111111111";
        public DoctorController(DoctorService doctorService) {
            this.doctorService = doctorService;
        }
        public bool Create(string fName, string lName, string jmbg, string username, string phone, string email, string country, string city, string address, Gender gender, string specialization, string roomId) {
            var doctor = new Doctor(fName, lName, jmbg, username, null, phone, email, country, city, address, gender, specialization, roomId);
            return this.doctorService.Save(doctor);
        }
        public List<Doctor> GetAll() { 
            return this.doctorService.GetAll();
        }
    }
}
