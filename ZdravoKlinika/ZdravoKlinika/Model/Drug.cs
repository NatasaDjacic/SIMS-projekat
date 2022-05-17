using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Drug
    {
        public int drugId { get; set; }
        public string name { get; set; }
        public string ingredients { get; set; }
        public bool approved { get; set; }
        public string checkedBy { get; set; }
        public string comment { get; set; }
        public string alternative { get; set; }


        public Drug(string name) {
            this.name = name;
            this.comment = "";
            this.alternative = "";
            this.ingredients = "";
        }
        public Drug(string name, string ingredients, string alternative)
        {
            this.name = name;
            this.comment = "";
            this.alternative = alternative;
            this.ingredients = ingredients;
        }

        public Drug()
        {
        }
    }
}
