using System;
using System.Text.RegularExpressions;

namespace ZdravoKlinika.Model {
    public class DoctorsMarkDTO {
        public string roomId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        public DoctorsMarkDTO(string roomId, string name, string description, string type) {
            this.roomId = roomId;
            this.name = name;
            this.description = description;
            this.type = type;
        }

        public DoctorsMarkDTO() {
        }

        public void Validate() {
          

            if (name.Trim().Length == 0) {
                throw new Exception("Morate uneti ime.");
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
