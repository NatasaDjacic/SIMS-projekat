using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class AdvancedRenovationService
    {
        AdvancedRenovationRepository advancedRenovationRepository;

        public AdvancedRenovationService(AdvancedRenovationRepository advancedRenovationRepository)
        {
            this.advancedRenovationRepository = advancedRenovationRepository;
        }
        public List<AdvancedRenovation> GetAll()
        {
            return this.advancedRenovationRepository.GetAll();
        }
        public int GenerateNewId()
        {
            return this.advancedRenovationRepository.GenerateNewId();
        }
        public bool Save(AdvancedRenovation advRen)
        {
            return this.advancedRenovationRepository.Save(advRen);
        }
        public List<AdvancedRenovation> GetAllInInterval(DateTime start, DateTime end)
        {
            return this.GetAll().Where(a => (a.startTime.AddMinutes(a.duration) >= start && a.startTime <= end)).ToList();
        }
        public RoomService roomService = GLOBALS.roomService;

        public void ExecuteRoomSeparating()
        {
            List<AdvancedRenovation> advancedRenovations = new List<AdvancedRenovation>(GetAll());
            foreach (AdvancedRenovation advancedRenovation in advancedRenovations)
            {
                if (advancedRenovation.startTime <= System.DateTime.Now)
                {
                    var id = advancedRenovation.roomId;
                    Console.WriteLine(id);
                    roomService.Delete(id);
                }
                if (advancedRenovation.startTime.AddHours(advancedRenovation.duration) <= System.DateTime.Now)
                {
                    Room room1 = new Room("id1", advancedRenovation.firstRoomName, advancedRenovation.firstRoomDescription, advancedRenovation.firstRoomType);
                    Room room2 = new Room("id2", advancedRenovation.secondRoomName, advancedRenovation.secondRoomDescription, advancedRenovation.secondRoomType);
                    
                    roomService.Save(room1);
                    roomService.Save(room2);
                    advancedRenovationRepository.DeleteById(advancedRenovation.advancedRenovationId);
                }
            }

        }

    }
}
