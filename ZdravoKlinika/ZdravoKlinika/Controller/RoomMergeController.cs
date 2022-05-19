using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class RoomMergeController
    {
        RoomMergeService roomMergeService;

        public RoomMergeController(RoomMergeService roomMergeService)
        {
            this.roomMergeService = roomMergeService;
        }

        public bool Save(DateTime startTime, int duration, string roomFirstId, string roomSecondId, string newRoomId, string newRoomName, string newRoomType, string newRoomDescription)
        {

            RoomMerge roomMerge = new RoomMerge();
            roomMerge.startTime = startTime;
            roomMerge.duration = duration;
            roomMerge.roomFirstId = roomFirstId;
            roomMerge.roomSecondId = roomSecondId;
            roomMerge.roomMergeId = roomMergeService.GenerateNewId();
            roomMerge.newRoomId = newRoomId;
            roomMerge.newRoomName = newRoomName;
            roomMerge.newRoomType = newRoomType;
            roomMerge.newRoomDescription = newRoomDescription;



            return roomMergeService.Save(roomMerge);

        }
        public void ExecuteRoomMerging()
        {
            roomMergeService.ExecuteRoomMerging();
        }

    }
}
