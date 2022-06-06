using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Prescription
    {
        public Guid prescriptionId { get; set; }
        public int drugId { get; set; }
        public string patient_note { get; set; }
        public string drugName { get; set; }
        public string description { get; set; }

        // 7 days 3 times a day 1 tablet 
        // 7 - useDurations, 3 - useFrequenc, 1 - useAmmount
        public int useDuration { get; set; }
        public int useFrequency { get; set; }
        public double useAmount { get; set; }

        public Prescription(Guid pId, int drugId, string description, int useDuration, int useFrequency, double useAmount,string drugName)
        {
            this.prescriptionId = pId;
            this.drugId = drugId;
            this.drugName = drugName;
            this.description = description;
            this.useDuration = useDuration;
            this.useFrequency = useFrequency;
            this.useAmount = useAmount;
            this.patient_note="";
        }
    }
}
