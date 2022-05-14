using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller {
    public class OrderEquipmentController {
        OrderEquipmentService orderEquipmentService;
        public OrderEquipmentController(OrderEquipmentService orderEquipmentService) { 
            this.orderEquipmentService = orderEquipmentService;
        }

        public bool CrateOrderEquipment(string name, int quantity) {
            return this.orderEquipmentService.CrateOrderEquipment(name, quantity);
        }
        public void CheckEquipmentArrival() {
            this.orderEquipmentService.CheckEquipmentArrival();
        }
    }
}
