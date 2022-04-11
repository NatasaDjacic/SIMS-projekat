using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Room
    {
        public string roomId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        public Room(string roomId, string name, string description, string type)
        {
            this.roomId = roomId;
            this.name = name;
            this.description = description;
            this.type = type;
        }

        public Room()
        {
        }

    }
}
