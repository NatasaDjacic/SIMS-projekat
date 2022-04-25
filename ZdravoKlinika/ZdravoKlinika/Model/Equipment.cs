using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Equipment
    {
        public int id { get; set; }
        public string name { get; set; }
        public string roomId { get; set; }
        public string type { get; set; }
        public int quantity { get; set; }

        public Equipment() { }
        public Equipment(int id, string name, string roomId, string type, int quantity)
        {
            this.id = id;
            this.name = name;
            this.roomId = roomId;
            this.type = type;
            this.quantity = quantity;
        }
    }
}
