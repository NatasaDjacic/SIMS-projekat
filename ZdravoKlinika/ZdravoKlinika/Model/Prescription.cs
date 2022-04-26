using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Prescription
    {
        public int drugId { get; set; }
        public string description { get; set; }

        public Prescription(int drugId, string description)
        {
            this.drugId = drugId;
            this.description = description;
        }
    }
}
