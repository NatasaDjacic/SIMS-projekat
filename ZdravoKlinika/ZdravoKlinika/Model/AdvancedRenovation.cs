using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class AdvancedRenovation
    {
        public int advancedRenovationId;
        public string roomId;
        public DateTime startTime;
        public int duration;
        public string firstRoomName;
        public string firstRoomType;
        public string firstRoomDescription;
        public string secondRoomName;
        public string secondRoomType;
        public string secondRoomDescription;


        public AdvancedRenovation(int advancedRenovationId, string roomId, DateTime startTime, int duration, string firstRoomName, string firstRoomType, string firstRoomDescription, string secondRoomName, string secondRoomType, string secondRoomDescription)
        {
            this.advancedRenovationId = advancedRenovationId;
            this.roomId = roomId;
            this.startTime = startTime;
            this.duration = duration;
            this.firstRoomName = firstRoomName;
            this.firstRoomType = firstRoomType;
            this.firstRoomDescription = firstRoomDescription;
            this.secondRoomName = secondRoomName;
            this.secondRoomType = secondRoomType;
            this.secondRoomDescription = secondRoomDescription;
        }

        public AdvancedRenovation()
        {
        }
    }
}
