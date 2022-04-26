using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Model {
    public class Patient : User {

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public BloodType bloodType { get; set; }
        public  DateTime dateOfBirth { get; set; }
        public List<string> allergens { get; set; }

        public List<Report> reports { get; set; }

        public Patient(string firstName, string lastName, string jmbg, string? username,DateTime? dateOfBirth, string? password, string? phone,
            string? email, string? country, string? city, string? address,
            Gender? gender, BloodType? bloodType, List<string>? allergens) : base(firstName, lastName, jmbg, username, password, phone, email, country, city, address) {
            this.gender = gender ?? Gender.None;
            this.dateOfBirth = dateOfBirth ?? DateTime.Now;
            this.bloodType = bloodType ?? BloodType.None;
            this.allergens = allergens ?? new List<string>();
            this.reports = new List<Report>();
        }
        public Patient(string firstName, string lastName, string jmbg) : base(firstName, lastName, jmbg, null, null, null, null, null, null, null) {
            this.allergens = new List<string>();
            this.dateOfBirth = DateTime.Now;
            this.bloodType = BloodType.None;
            this.gender = Gender.None;
            this.reports = new List<Report>();
        }

        public Patient() : base() {
            this.allergens = new List<string>();
            this.reports = new List<Report>();
        }

        public void ValidateGuest() {
            var nameReg = new Regex(@"^[a-zA-Z]{3,}$");
            var jmbgReg = new Regex(@"^\d{13}$");

            if (!nameReg.IsMatch(this.firstName)) {
                throw new Exception("Not valid first name.");
            }
            if (!nameReg.IsMatch(this.lastName)) {
                throw new Exception("Not valid last name.");
            }
            if (!jmbgReg.IsMatch(this.JMBG)) {
                throw new Exception("Not valid JMBG.");
            }
        }
        public void Validate() {
            this.ValidateGuest();
            var usernameReg = new Regex(@"^[a-zA-Z0-9]{3,}$");
            var emailReg = new Regex(@"^\S+@\S+\.\S+$");

            if (!usernameReg.IsMatch(this.username)) {
                throw new Exception("Not valid username.");
            }
            if (!emailReg.IsMatch(this.email) && this.email.Trim() != "") {
                throw new Exception("Not valid email.");
            }
            if(gender == Gender.None) {
                throw new Exception("Gender must be set.");
            }
            if(city.Trim().Length == 0) {
                throw new Exception("City must be set.");
            }
            if (country.Trim().Length == 0) {
                throw new Exception("Country must be set.");
            }
            if (address.Trim().Length == 0) {
                throw new Exception("Address must be set.");
            }
        }
    }
}
