using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ZdravoKlinika.Model {
    public class Meeting {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime startTime { get; set; }
        public int duration { get; set; }

        [JsonIgnore]
        public DateTime endTime { get => startTime.AddMinutes(duration); }
        public string roomId { get; set; }
        public List<string> attendeesJMBG { get; set; }

        public Meeting(int id, string title, DateTime startTime, int duration, string roomId, List<string> attendeesJMBG) {
            this.id = id;
            this.title = title;
            this.startTime = startTime;
            this.duration = duration;
            this.roomId = roomId;
            this.attendeesJMBG = attendeesJMBG;
        }


        public bool HasAttende(string JMBG) {
            return this.attendeesJMBG.Contains(JMBG);
        }
    }
}
