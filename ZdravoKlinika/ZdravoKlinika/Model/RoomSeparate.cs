using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class RoomSeparate : Renovation
    {
        public int id;
        public string firstRoomId;
        public string firstRoomName;
        public string firstRoomType;
        public string firstRoomDescription;
        public string secondRoomId;
        public string secondRoomName;
        public string secondRoomType;
        public string secondRoomDescription;

        public RoomSeparate(int id, string roomId, DateTime startTime, int duration, string firstRoomId, string firstRoomName, string firstRoomType, string firstRoomDescription, string secondRoomId, string secondRoomName, string secondRoomType, string secondRoomDescription)
        {
            this.id = id;
            this.roomId = roomId;
            this.startTime = startTime;
            this.duration = duration;
            this.firstRoomId = firstRoomId;
            this.firstRoomName = firstRoomName;
            this.firstRoomType = firstRoomType;
            this.firstRoomDescription = firstRoomDescription;
            this.secondRoomId = secondRoomId;
            this.secondRoomName = secondRoomName;
            this.secondRoomType = secondRoomType;
            this.secondRoomDescription = secondRoomDescription;
        }

        public RoomSeparate()
        {
        }
    }
}
