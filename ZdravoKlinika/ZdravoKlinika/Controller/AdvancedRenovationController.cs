using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class AdvancedRenovationController
    {
        AdvancedRenovationService advancedRenovationService;

        public AdvancedRenovationController(AdvancedRenovationService advancedRenovationService)
        {
            this.advancedRenovationService = advancedRenovationService;
        }

        public bool SaveAdvancedRenovation(DateTime startTime, int duration, string roomId, string firstRoomName, string firstRoomType, string firstRoomDescription, string secondRoomName, string secondRoomType, string secondRoomDescription)
        {

            AdvancedRenovation advancedRenovation = new AdvancedRenovation();
            advancedRenovation.startTime = startTime;
            advancedRenovation.duration = duration;
            advancedRenovation.roomId = roomId;
            var id = advancedRenovation.advancedRenovationId = advancedRenovationService.GenerateNewId();
            advancedRenovation.firstRoomName = firstRoomName;
            advancedRenovation.firstRoomType = firstRoomType;
            advancedRenovation.firstRoomDescription = firstRoomDescription;
            advancedRenovation.secondRoomName = secondRoomName;
            advancedRenovation.secondRoomType = secondRoomType;
            advancedRenovation.secondRoomDescription = secondRoomDescription;


            return advancedRenovationService.Save(advancedRenovation);

        }
        public void ExecuteRoomSeparating()
        {
            advancedRenovationService.ExecuteRoomSeparating();
        }
    }
}
