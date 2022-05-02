using System.Collections.Generic;
using System.Linq;

namespace ZdravoKlinika.Model.DTO
{
    public class EquipRoomGroupDTO
    {
        public string roomId { get; set; }
        public string name { get; set; }
        public List<int> equipIds { get; set; }

        public EquipRoomGroupDTO(string roomId, string name, List<int> equipIds)
        {
            this.roomId = roomId;
            this.name = name;
            this.equipIds = equipIds;
        }
        public static List<EquipRoomGroupDTO> groupEquip(List<Equipment> equipments)
        {
            return equipments.GroupBy(e => new { e.roomId, e.name }).Select(g =>
            new EquipRoomGroupDTO(g.Key.roomId, g.Key.name, g.Select(h => h.id).ToList())).ToList();
         
        }
    }
}
