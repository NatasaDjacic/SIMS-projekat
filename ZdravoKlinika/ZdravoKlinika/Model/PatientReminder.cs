using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class PatientReminder
    {
        public int id { get; set; }
        public string patientJMBG { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }

        public PatientReminder(int id, string patientJMBG, DateTime date,string name)
        {
            this.id = id;
            this.patientJMBG = patientJMBG;
            this.date = date;
            this.name = name;
        }

    }
}
