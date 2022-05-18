using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class RoomSeparateService
    {
        RoomSeparateRepository roomSeparateRepository;

        public RoomSeparateService(RoomSeparateRepository advancedRenovationRepository)
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
        public List<RoomSeparate> GetAllInInterval(DateTime start, DateTime end)
        {
            return this.GetAll().Where(a => (a.startTime.AddMinutes(a.duration) >= start && a.startTime <= end)).ToList();
        }
        public RoomService roomService = GLOBALS.roomService;

        public void ExecuteRoomSeparating()
        {
            List<RoomSeparate> advancedRenovations = new List<RoomSeparate>(GetAll());
            foreach (RoomSeparate advancedRenovation in advancedRenovations)
            {
                if (advancedRenovation.startTime <= System.DateTime.Now)
                {
                    var id = advancedRenovation.roomId;
                    Console.WriteLine(id);
                    roomService.Delete(id);
                }
                if (advancedRenovation.startTime.AddHours(advancedRenovation.duration) <= System.DateTime.Now)
                {
                    Room room1 = new Room(advancedRenovation.firstRoomId, advancedRenovation.firstRoomName, advancedRenovation.firstRoomDescription, advancedRenovation.firstRoomType);
                    Room room2 = new Room(advancedRenovation.secondRoomId, advancedRenovation.secondRoomName, advancedRenovation.secondRoomDescription, advancedRenovation.secondRoomType);
                    
                    roomService.Save(room1);
                    roomService.Save(room2);
                    roomSeparateRepository.DeleteById(advancedRenovation.roomSeparateId);
                }
            }

        }

    }
}
