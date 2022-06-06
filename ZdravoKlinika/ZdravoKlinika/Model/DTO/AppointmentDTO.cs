using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Model.DTO
{
    public class AppointmentDTO
    {

        public int id { get; set; }
        public DateTime startTime { get; set; }

        public string doctorJMBG { get; set; }

        public string roomId { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }

        public AppointmentDTO(int id, DateTime startTime,  string doctorJMBG, string roomId, string firstName,string lastName)
        {
            this.id = id;
            this.startTime = startTime;
            this.firstName = firstName;
            this.lastName = lastName;
            this.doctorJMBG = doctorJMBG;
            this.roomId = roomId;
        }

    }
}


