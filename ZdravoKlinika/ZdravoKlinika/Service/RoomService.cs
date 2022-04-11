using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class RoomService
    {
        public RoomRepository roomRepository { get; set; }
        public RoomService(RoomRepository roomRepository) {
            this.roomRepository = roomRepository;
        }
        public bool Create(Room room)
        {
            if (this.roomRepository.GetById(room.roomId) is null)
            {
                return this.roomRepository.Save(room); ;
            }
            return false;
        }
        public List<Room> GetAll()
        {
            return this.roomRepository.GetAll();
        }

        public Room GetById(string roomId)
        {
            return this.roomRepository.GetById(roomId);
        }

        public bool Save(Room room)
        {
            throw new NotImplementedException();
        }

        public bool Update(Model.Room room)
        {
            if (this.roomRepository.GetById(room.roomId) is not null)
            {
                return !this.roomRepository.Save(room);
            }
            return false;
        }

        public bool Delete(string roomId)
        {
            return this.roomRepository.DeleteById(roomId);
        }

    }
}
