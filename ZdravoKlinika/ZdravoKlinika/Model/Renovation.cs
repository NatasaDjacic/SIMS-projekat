using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Model {
    public class Renovation {
        public int id { get; set; }
        public string roomId { get; set; }
        public DateTime startTime { get; set; }
        public int duration { get; set; }
        public string description { get; set; }
        public Renovation(int id, string roomId, DateTime startTime, int duration, string description) { 
            this.id = id;
            this.roomId = roomId;
            this.startTime = startTime;
            this.duration = duration;
            this.description = description;
        }
        public Renovation() { }
        public void Validate()
        {


            if (roomId.Trim().Length == 0)
            {
                throw new Exception("Please choose room.");
            }
            if (roomId.Trim().Length == 0)
            {
                throw new Exception("Please choose room.");
            }
            if (startTime.ToString().Trim().Length == 0)
            {
                throw new Exception("Please set date.");
            }
            /*if (startTime < DateTime.Now)
            {
                throw new Exception("Date cannot be in past.");
            }*/

        }
    }
}
