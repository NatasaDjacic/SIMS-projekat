using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model {
    public class Meeting {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime startDate { get; set; }
        public int duration { get; set; }
        public string roomId { get; set; }
        public List<string> attendeesJMBG { get; set; }

        public Meeting(int id, string title, DateTime startDate, int duration, string roomId) {
            this.id = id;
            this.title = title;
            this.startDate = startDate;
            this.duration = duration;
            this.roomId = roomId;
            this.attendeesJMBG = new List<string>();
        }
    }
}
