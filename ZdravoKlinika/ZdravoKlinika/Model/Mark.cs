using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Mark
    {
        public int id { get; set; }
        public int hospitalMark { get; set; }

        public int doctorMark { get; set; }
        public string patientJMBG { get; set; }
        public string doctorJMBG { get; set; }

        public Guid reportId { get; set; }


        public Mark() { }

        public Mark(int id,int hospitalMark,int doctorMark,string patientJMBG,string doctorJMBG,Guid reportId)
        {
            this.id = id;
            this.hospitalMark = hospitalMark;
            this.doctorMark = doctorMark;
            this.patientJMBG = patientJMBG;
            this.doctorJMBG = doctorJMBG;
            this.reportId = reportId;

            
        }

    }
}
