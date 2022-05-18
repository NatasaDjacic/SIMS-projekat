using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class RoomSeparateController
    {
        RoomSeparateService roomSeparateService;

        public RoomSeparateController(RoomSeparateService roomSeparateService)
        {
            this.roomSeparateService = roomSeparateService;
        }

        public bool SaveAdvancedRenovation(DateTime startTime, int duration, string roomId, string firstRoomId, string firstRoomName, string firstRoomType, string firstRoomDescription, string secondRoomId, string secondRoomName, string secondRoomType, string secondRoomDescription)
        {

            RoomSeparate advancedRenovation = new RoomSeparate();
            advancedRenovation.startTime = startTime;
            advancedRenovation.duration = duration;
            advancedRenovation.roomId = roomId;
            var id = advancedRenovation.roomSeparateId = roomSeparateService.GenerateNewId();
            advancedRenovation.firstRoomId = firstRoomId;
            advancedRenovation.firstRoomName = firstRoomName;
            advancedRenovation.firstRoomType = firstRoomType;
            advancedRenovation.firstRoomDescription = firstRoomDescription;
            advancedRenovation.secondRoomId = secondRoomId;
            advancedRenovation.secondRoomName = secondRoomName;
            advancedRenovation.secondRoomType = secondRoomType;
            advancedRenovation.secondRoomDescription = secondRoomDescription;


            return roomSeparateService.Save(advancedRenovation);

        }
        public void ExecuteRoomSeparating()
        {
            roomSeparateService.ExecuteRoomSeparating();
        }
    }
}
