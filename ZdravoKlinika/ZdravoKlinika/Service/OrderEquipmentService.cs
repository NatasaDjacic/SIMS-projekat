using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service {
    public class OrderEquipmentService {
        IOrderEquipmentRepository orderEquipmentRepository;
        IEquipmentRepository equipmentRepository;

        public OrderEquipmentService(IOrderEquipmentRepository orderEquipmentRepository, IEquipmentRepository equipmentRepository) { 
            this.orderEquipmentRepository = orderEquipmentRepository;
            this.equipmentRepository = equipmentRepository;
        }

        public bool CrateOrderEquipment(string name, int quantity) {
            var orderEquipment = new OrderEquipment(this.GenerateNewId(), DateTime.Now.AddDays(3), name, quantity);
            return this.orderEquipmentRepository.Save(orderEquipment);
        }
        public void CheckEquipmentArrival() {
            var orderEquipments = this.orderEquipmentRepository.GetAll();

            orderEquipments.ForEach(orderEquipment => {
                if(DateTime.Now >= orderEquipment.orderArrival) {
                    EquipmentArrive(orderEquipment);
                    this.orderEquipmentRepository.DeleteById(orderEquipment.id);
                }
            });
        }
        private bool EquipmentArrive(OrderEquipment orderEquipment) {
            var equipment = this.equipmentRepository.GetAllDynamic().Find(equipment => equipment.name == orderEquipment.equipment.name);
            if (equipment != null) {
                equipment.quantity += orderEquipment.equipment.quantity;
            } else {
                equipment = orderEquipment.equipment;
                equipment.id = equipmentRepository.GenerateNewId();
            }
            return this.equipmentRepository.Save(equipment);

        }
        public int GenerateNewId() {
            return this.orderEquipmentRepository.GenerateNewId();
        }
    }
}
