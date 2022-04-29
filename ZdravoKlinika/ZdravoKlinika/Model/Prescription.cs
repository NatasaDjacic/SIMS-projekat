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

        // 7 days 3 times a day 1 tablet 
        // 7 - useDurations, 3 - useFrequenc, 1 - useAmmount
        public int useDuration { get; set; }
        public int useFrequency { get; set; }
        public int useAmount { get; set; }

        public Prescription(int drugId, string description, int useDuration, int useFrequency, int useAmount)
        {
            this.drugId = drugId;
            this.description = description;
            this.useDuration = useDuration;
            this.useFrequency = useFrequency;
            this.useAmount = useAmount;
        }
    }
}
