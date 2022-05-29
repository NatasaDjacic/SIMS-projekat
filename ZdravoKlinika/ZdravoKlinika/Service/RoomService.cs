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
        public bool Create(DoctorsMarkDTO room)
        {
            if (this.roomRepository.GetById(room.roomId) is null)
            {
                return this.roomRepository.Save(room);
            }
            return false;
        }
        public List<DoctorsMarkDTO> GetAll()
        {
            return this.roomRepository.GetAll();
        }

        public DoctorsMarkDTO GetById(string roomId)
        {
            return this.roomRepository.GetById(roomId);
        }

        public bool Save(DoctorsMarkDTO room)
        {
            return this.roomRepository.Save(room);
        }

        public bool Update(DoctorsMarkDTO room)
        {
           
            
            return !this.roomRepository.Save(room);
            
          
        }

        public bool Delete(string roomId)
        {
            return this.roomRepository.DeleteById(roomId);
        }

    }
}
