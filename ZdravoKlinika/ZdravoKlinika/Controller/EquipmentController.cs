using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class EquipmentController
    {
        public Service.EquipmentService equipmentService;
        public EquipmentController(EquipmentService equipmentService)
        {
            this.equipmentService = equipmentService;
        }
        public bool Create(int id, string name, string roomId, string type, int quantity)
        {
            var equipment = new Equipment(id, name, roomId, type, quantity);
            return this.equipmentService.Create(equipment);
        }

        public bool Delete(int id)
        {
           return this.equipmentService.Delete(id);
        }

        public bool Update(Equipment equipment)
        {
            return this.equipmentService.Update(equipment);
        }

        public Equipment GetById(int id)
        {
            return equipmentService.GetById(id);
        }

        public List<Equipment> GetAll()
        {
            return equipmentService.GetAll();
        }
        public List<Equipment> GetAllDynamic()
        {
            return equipmentService.GetAllDynamic();
        }
        

    }
}
