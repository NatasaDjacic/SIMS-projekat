using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class EquipMovingController
    {
        private RoomService roomService;
        private EquipMovingService equipMovingService;
        public EquipMovingController(EquipMovingService equipMovingService, RoomService roomService)
        {
            this.equipMovingService = equipMovingService;
            this.roomService = roomService;
        }

        public List<EquipMoving> GetAllEquipMoving()
        {
            return equipMovingService.GetAllEquipMovings();
        }

        public EquipMoving? GetEquipMovingById(int id)
        {
            return equipMovingService.GetEquipMovingById(id);
        }

        public bool DeleteEquipMovingById(int id)
        {
            return equipMovingService.DeleteEquipMovingById(id);
        }

      

        public bool CreateEquipMovingRoomFrom(DateTime date, string id1, string id2, List<int> equips)
        {
            var room1 = roomService.GetById(id1);
            var room2 = roomService.GetById(id2);

            if (room1 == null) throw new Exception("Room not found");
            if (room2 == null) throw new Exception("Room not found");


            EquipMoving equipMoving = new EquipMoving();
            
            equipMoving.date = date;
            equipMoving.roomFrom = id1;
            equipMoving.roomTo = id2;
            equipMoving.id = equipMovingService.GenerateNewId();
            equipMoving.equipments = equips;

            return equipMovingService.SaveEquipMoving(equipMoving);
        }
        public void CheckEquipMoving()
        {
            equipMovingService.CheckEquipMoving();
        }
        public List<EquipMoving> GetRoomEquipMovings()
        {
            throw new NotImplementedException();
        }

        public bool CreateEquipMovingRoomTo(DateTime date)
        {
            throw new NotImplementedException();
        }
        public List<int> GetEquipIdsForMove(List<int> ids, int amount)
        {
            return equipMovingService.GetEquipIdsForMove(ids, amount);
        }

    }
}
