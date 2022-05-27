using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Model
{
    public class HolidayRequest
    {
        public int id { get; set; }
        public DateTime startTime { get; set; }
        public int duration { get; set; } // in houres
        public DateTime endTime { get => startTime.AddHours(duration); }
        public bool urgency { get; set; }
        public string reason { get; set; }
        public RequestType type { get; set; }
        public string doctorJMBG { get; set; }

        public HolidayRequest(int id, DateTime startTime, int duration, bool urgency, string reason, RequestType type, string doctorJMBG) {
            this.id = id;
            this.startTime = startTime;
            this.duration = duration;
            this.urgency = urgency;
            this.reason = reason;
            this.type = type;
            this.doctorJMBG = doctorJMBG;
        }
    }
}
