
using System;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Model;
using System.Text.Json.Serialization;

namespace ZdravoKlinika.Model
{
    public class Doctor : User
    {
        public string specialization { get; set; }
        public string? roomId { get; set; }
        public Gender? gender { get; set; }

        public Doctor(string firstName, string lastName, string jmbg, string? username, string? password, string? phone,
              string? email, string? country, string? city, string? address,
              Gender? gender, string specialization, string roomId) : base(firstName, lastName, jmbg, username, password, phone, email, country, city, address)
        {
            this.gender = gender ?? Gender.None;
            this.specialization = specialization;
            this.roomId = roomId;

        }

    }
}