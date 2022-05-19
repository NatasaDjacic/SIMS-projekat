using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Cancellation
    {
        public int id { get; set; }
        public string patientJMBG { get; set; }
        public DateTime date { get; set; }

        public Cancellation(int id,string patientJMBG,DateTime date)
        {
            this.id = id;
            this.patientJMBG = patientJMBG;
            this.date = date;
        }

    }
}
