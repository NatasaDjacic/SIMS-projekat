using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model {
    public class Notification {

        public string JMBG { get; set; }
        public string title { get; set; }
        public string message { get; set; }
        public bool seen { get; set; }
        public DateTime showDate { get; set; }

        public Notification(string JMBG, string title, string message, DateTime showDate) {
            this.JMBG = JMBG;
            this.title = title;
            this.message = message;
            this.showDate = showDate;
            this.seen = false;
        }
        public Notification() {
            this.JMBG = "";
            this.title = "";
            this.message = "";
            this.seen = false;
            this.showDate = DateTime.Now;
        }

    }
}
