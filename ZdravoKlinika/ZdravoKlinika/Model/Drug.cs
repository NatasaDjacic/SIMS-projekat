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
        public List<string> ingredients { get; set; }
        public bool approved { get; set; }
        public string checkedBy { get; set; }
        public string comment { get; set; }

        public Drug(string name) {
            this.name = name;
            this.comment = "";
            this.ingredients = new List<string>();
        }
        public Drug(string name, List<string> ingredients)
        {
            this.name = name;
            this.comment = "";
            this.ingredients = ingredients;
        }
    }
}
