using System;

namespace ZdravoKlinika.Model {
    public class Room {
        public string roomId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        public Room(string roomId, string name, string description, string type) {
            this.roomId = roomId;
            this.name = name;
            this.description = description;
            this.type = type;
        }

        public Room() {
        }

        public void Validate() {

            if (name.Trim().Length == 0) {
                throw new Exception("Morate uneti ime.");
            }
            if (description.Trim().Length == 0) {
                throw new Exception("Morate uneti opis.");
            }
            if (type.Trim().Length == 0) {
                throw new Exception("Morate uneti tip prostorije.");
            }
            if (roomId.Trim().Length == 0) {
                throw new Exception("Morate uneti id.");
            }
        }

    }
}
