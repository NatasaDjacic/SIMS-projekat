using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model {

    public class MedicalRecord {

        public List<Report> reports { get; set; }

        public MedicalRecord() {
            this.reports = new List<Report>();
        }
    }
}
