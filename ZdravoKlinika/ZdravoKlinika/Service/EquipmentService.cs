﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class EquipmentService
    {
        public EquipmentRepository equipmentRepository { get; set; }

        public EquipmentService(EquipmentRepository equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }

        public bool Create(Equipment equipment)
        {
            if (this.equipmentRepository.GetById(equipment.id) is null)
            {
                return this.equipmentRepository.Save(equipment);
            }
            return false;
        }

        public List<Equipment> GetAll()
        {
            return this.equipmentRepository.GetAll();
        }
        public List<Equipment> GetAllDynamic()
        {
            return this.equipmentRepository.GetAllDynamic();
        }

        public Equipment? GetById(int id)
        {
            return this.equipmentRepository.GetById(id);
        }

        public bool Save(Equipment equipment)
        {
           
            return this.equipmentRepository.Save(equipment);
           
        }

        public bool Update(Model.Equipment equipment)
        {
            if(this.equipmentRepository.GetById(equipment.id) is not null)
            {
                return !this.equipmentRepository.Save(equipment);
            }
            return false;
        }

        public bool Delete(int id)
        {
            return this.equipmentRepository.DeleteById(id);
        }

        public int GenerateNewId() {
            return this.equipmentRepository.GenerateNewId();
        }
       

    }
}
