using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class RoomMergeService
    {
        RoomMergeRepository roomMergeRepository;

        public RoomMergeService(RoomMergeRepository roomMergeRepository)
        {
            this.roomMergeRepository = roomMergeRepository;
        }
        public List<RoomMerge> GetAll()
        {
            return this.roomMergeRepository.GetAll();
        }
        public int GenerateNewId()
        {
            return this.roomMergeRepository.GenerateNewId();
        }
        public bool Save(RoomMerge roomMerge)
        {
            return this.roomMergeRepository.Save(roomMerge);
        }
        
        public RoomService roomService = GLOBALS.roomService;

        public void ExecuteRoomMerging()
        {
            List<RoomMerge> roomMerges = new List<RoomMerge>(GetAll());
            foreach (RoomMerge roomMerge in roomMerges)
            {
                if (roomMerge.startTime <= System.DateTime.Now)
                {
                    this.StartMergingRooms(roomMerge);
                }
                if (roomMerge.startTime.AddHours(roomMerge.duration) <= System.DateTime.Now)
                {
                    this.FinishMergingRooms(roomMerge);
                }
            }

        }
        public void StartMergingRooms(RoomMerge roomMerge) {
            roomService.Delete(roomMerge.roomFirstId);
            roomService.Delete(roomMerge.roomSecondId);
        }

        public void FinishMergingRooms(RoomMerge roomMerge) {
            Room room = new Room(roomMerge.newRoomId, roomMerge.newRoomName, roomMerge.newRoomDescription, roomMerge.newRoomType);

            roomService.Save(room);
            roomMergeRepository.DeleteById(roomMerge.roomMergeId);
        }
    }
}
