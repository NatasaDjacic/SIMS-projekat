using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string JMBG { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string address { get; set; }

        public User(string firstName, string lastName, string jmbg, string? password, string? phone, string? email, string? country, string? city, string? address)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.JMBG = jmbg;
            this.password = password ?? "zdravo";
            this.phone = phone ?? "";
            this.email = email ?? "";
            this.country = country ?? "";
            this.city = city ?? "";
            this.address = address ?? "";
        }
        public User()
        {
            this.firstName = "";
            this.lastName = "";
            this.JMBG = "";
            this.password = "zdravo";
            this.phone = "";
            this.email = "";
            this.country = "";
            this.city = "";
            this.address = "";
        }
    }
}
