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
        public DateTime endTime { get; set; }
        public bool urgency { get; set; }
        public string reason { get; set; }
        public RequestType appointmentType { get; set; }
        public string doctorJMBG { get; set; }

        public string specialization { get; set; }
    }
}
