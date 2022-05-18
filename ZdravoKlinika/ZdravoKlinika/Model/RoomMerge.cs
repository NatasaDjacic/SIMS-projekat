using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class RoomMerge
    {
        public int roomMergeId;
        public string roomFirstId;
        public string roomSecondId;
        public DateTime startTime;
        public int duration;
        public string newRoomId;
        public string newRoomName;
        public string newRoomType;
        public string newRoomDescription;

        public RoomMerge()
        {
        }

        public RoomMerge(int roomMergeId, string roomFirstId, string roomSecondId, DateTime startTime, int duration, string newRoomId, string newRoomName, string newRoomType, string newRoomDescription)
        {
            this.roomMergeId = roomMergeId;
            this.roomFirstId = roomFirstId;
            this.roomSecondId = roomSecondId;
            this.startTime = startTime;
            this.duration = duration;
            this.newRoomId = newRoomId;
            this.newRoomName = newRoomName;
            this.newRoomType = newRoomType;
            this.newRoomDescription = newRoomDescription;
        }
    }
}
