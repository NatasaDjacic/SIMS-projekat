using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model {
    public class OrderEquipment {
        public int id { get;set;}
        public DateTime orderArrival { get;set;}
        public Equipment equipment {get;set;}

        public OrderEquipment(int id, DateTime orderArrival, string name, int quantitiy) {
            this.id = id;
            this.orderArrival = orderArrival;
            this.equipment = new Equipment(-1, name, "", "Dinamicka", quantitiy);
        }
    }
}
