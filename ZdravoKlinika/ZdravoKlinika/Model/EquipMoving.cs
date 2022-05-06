using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class EquipMoving
    {
        public int id { get; set; }
        public string roomFrom { get; set; }
        public string roomTo { get; set; }
        public List<int> equipments { get; set; }
        public DateTime date { get; set; }


        public EquipMoving() { }

        public EquipMoving(int id, string roomFrom, string roomTo, List<int> equipments, DateTime date)
        {
            this.id = id;
            this.roomFrom = roomFrom;
            this.roomTo = roomTo;
            this.equipments = equipments;
            this.date = date;
        }
        public void Validate()
        {


            if (roomFrom.Trim().Length == 0)
            {
                throw new Exception("Please choose room from.");
            }
            if (roomTo.Trim().Length == 0)
            {
                throw new Exception("Please choose room to.");
            }
            if (equipments.ToString().Trim().Length == 0)
            {
                throw new Exception("Please select equipments.");
            }
            /*if (date < System.DateTime.Now)
            {
                throw new Exception("Date cannot be in past.");
            }*/

        }
    }
}
