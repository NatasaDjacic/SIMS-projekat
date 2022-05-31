using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class RoomController
    {
        public Service.RoomService roomService;
        public RoomController(RoomService roomService)
        {
            this.roomService = roomService;
        }
        public bool Create(string roomId, string name, string description, string type)
        {
            var room = new Room(roomId,name, description, type);
            room.Validate();
            return this.roomService.Create(room);
        }

        public bool Delete(string roomId)
        {
            return this.roomService.Delete(roomId);
        }

        public bool Update(Room room)
        {
            room.Validate();
            return this.roomService.Update(room);
        }

        public Room GetById(string roomId)
        {
            return roomService.GetById(roomId);
        }

        public List<Room> GetAll()
        {
           return roomService.GetAll();
        }

        

    }
}
