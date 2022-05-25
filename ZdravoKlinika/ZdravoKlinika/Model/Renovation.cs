using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Model {
    public class Renovation {
        public int id;
        public string roomId;
        public DateTime startTime;
        public int duration;
        public string description;
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
