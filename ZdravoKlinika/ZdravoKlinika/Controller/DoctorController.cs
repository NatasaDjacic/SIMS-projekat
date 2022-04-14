using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Model;


namespace ZdravoKlinika.Controller
{
    internal class DoctorController
    {
        public DoctorService doctorService;
        public DoctorController(DoctorService doctorService)
        {
            this.doctorService = doctorService;
        }

        public bool Create(string fName, string lName, string jmbg, string username, string phone, string email, string country, string city, string address, Gender gender, string specialization, string roomId)
        {
            var doctor = new Doctor(fName, lName, jmbg, username, null, phone, email, country, city, address, gender, specialization, roomId);
            return this.doctorService.Save(doctor);
        }
    }
}
