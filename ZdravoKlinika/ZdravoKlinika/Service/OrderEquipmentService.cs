﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service {
    public class OrderEquipmentService {
        OrderEquipmentRepository orderEquipmentRepository;
        EquipmentRepository equipmentRepository;

        public OrderEquipmentService(OrderEquipmentRepository orderEquipmentRepository, EquipmentRepository equipmentRepository) { 
            this.orderEquipmentRepository = orderEquipmentRepository;
            this.equipmentRepository = equipmentRepository;
        }

        public bool CrateOrderEquipment(string name, int quantity) {
            var orderEquipment = new OrderEquipment(GenerateNewId(), DateTime.Now.AddDays(3), name, quantity);
            return this.orderEquipmentRepository.Save(orderEquipment);
        }
        public void CheckEquipmentArrival() {
            var orderEquipments = this.orderEquipmentRepository.GetAll();

            orderEquipments.ForEach(orderEquipment => {
                if(DateTime.Now <= orderEquipment.orderArrival) {
                    EquipmentArrive(orderEquipment);   
                }
            });
        }
        private bool EquipmentArrive(OrderEquipment orderEquipment) {
            var equipment = this.equipmentRepository.GetAllDynamic().Find(equipment=>equipment.name == orderEquipment.equipment.name);
            if (equipment != null) {
                equipment.quantity += orderEquipment.equipment.quantity;
            } else {
                equipment = orderEquipment.equipment;
                equipment.id = equipmentRepository.GenerateNewId();
            }
            this.orderEquipmentRepository.DeleteById(orderEquipment.id);
            return this.equipmentRepository.Save(equipment);

        }
        public int GenerateNewId() {
            return this.orderEquipmentRepository.GenerateNewId();
        }
    }
}