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

        public bool Save(DateTime startTime, int duration, string roomId, string firstRoomId, string firstRoomName, string firstRoomType, string firstRoomDescription, string secondRoomId, string secondRoomName, string secondRoomType, string secondRoomDescription)
        {

            RoomSeparate separationRenovation = new RoomSeparate();
            separationRenovation.startTime = startTime;
            separationRenovation.duration = duration;
            separationRenovation.roomId = roomId;
            separationRenovation.roomSeparateId = roomSeparateService.GenerateNewId();
            separationRenovation.firstRoomId = firstRoomId;
            separationRenovation.firstRoomName = firstRoomName;
            separationRenovation.firstRoomType = firstRoomType;
            separationRenovation.firstRoomDescription = firstRoomDescription;
            separationRenovation.secondRoomId = secondRoomId;
            separationRenovation.secondRoomName = secondRoomName;
            separationRenovation.secondRoomType = secondRoomType;
            separationRenovation.secondRoomDescription = secondRoomDescription;


            return roomSeparateService.Save(separationRenovation);

        }
        public void ExecuteRoomSeparating()
        {
            roomSeparateService.ExecuteRoomSeparating();
        }
    }
}
