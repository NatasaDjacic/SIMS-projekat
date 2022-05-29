using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model.Enums;
using Newtonsoft.Json;

namespace ZdravoKlinika.Model
{
    public class HolidayRequest
    {
        public int id { get; set; }
        public DateTime startTime { get; set; }
        public int duration { get; set; } // in days
        [JsonIgnore]
        public DateTime endTime { get => startTime.AddDays(duration); }
        [JsonIgnore]
        public string startDate { get => startTime.ToShortDateString(); }
        public bool urgent { get; set; }
        public string reason { get; set; }
        public string declineReason { get; set; }
        public RequestType status { get; set; }
        public string doctorJMBG { get; set; }

        public HolidayRequest(int id, DateTime startTime, int duration, bool urgent, string reason, RequestType type, string doctorJMBG) {
            this.id = id;
            this.startTime = startTime;
            this.duration = duration;
            this.urgent = urgent;
            this.reason = reason;
            this.status = type;
            this.doctorJMBG = doctorJMBG;
            this.declineReason = "";
        }
    }
}
