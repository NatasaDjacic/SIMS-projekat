using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service
{
    public class RoomSeparateService
    {
        IRoomSeparateRepository roomSeparateRepository;

        public RoomSeparateService(IRoomSeparateRepository advancedRenovationRepository)
        {
            this.roomSeparateRepository = advancedRenovationRepository;
        }
        public List<RoomSeparate> GetAll()
        {
            return this.roomSeparateRepository.GetAll();
        }
        public int GenerateNewId()
        {
            return this.roomSeparateRepository.GenerateNewId();
        }
        public bool Save(RoomSeparate roomSeparate)
        {
            return this.roomSeparateRepository.Save(roomSeparate);
        }
        
        public RoomService roomService = GLOBALS.roomService;

        public void ExecuteRoomSeparating()
        {
            List<RoomSeparate> roomSeparates = new List<RoomSeparate>(GetAll());
            foreach (RoomSeparate roomSeparate in roomSeparates)
            {
                if (roomSeparate.startTime <= System.DateTime.Now)
                {
                    roomService.Delete(roomSeparate.roomId);
                }
                if (roomSeparate.startTime.AddHours(roomSeparate.duration) <= System.DateTime.Now)
                {
                    FinishRoomSeparation(roomSeparate);
                }
            }

        }

        public void FinishRoomSeparation(RoomSeparate roomSeparate) {
            Room room1 = new Room(roomSeparate.firstRoomId, roomSeparate.firstRoomName, roomSeparate.firstRoomDescription, roomSeparate.firstRoomType);
            Room room2 = new Room(roomSeparate.secondRoomId, roomSeparate.secondRoomName, roomSeparate.secondRoomDescription, roomSeparate.secondRoomType);

            roomService.Save(room1);
            roomService.Save(room2);
            roomSeparateRepository.DeleteById(roomSeparate.id);
        }

    }
}
