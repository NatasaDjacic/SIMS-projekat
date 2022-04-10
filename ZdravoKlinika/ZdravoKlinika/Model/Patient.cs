using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace ZdravoKlinika.Model
{
    public class Patient : User
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender? gender { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BloodType? bloodType { get; set; }
        public List<string> allergens { get; set; } 

        public Patient(string firstName, string lastName, string jmbg, string? password, string? phone,
            string? email, string? country, string? city, string? address,
            Gender? gender, BloodType? bloodType, List<string>? allergens): base(firstName, lastName, jmbg, password, phone, email, country,city, address)
        {
            this.gender = gender;
            this.bloodType = bloodType;
            this.allergens = allergens ?? new List<string>();
        }
        public Patient(string firstName, string lastName, string jmbg) : base(firstName, lastName, jmbg, null,null,null,null,null,null)
        {
            this.allergens = new List<string>();
        }

        public Patient() : base()
        {
            this.allergens = new List<string>();
        }
    }
}
