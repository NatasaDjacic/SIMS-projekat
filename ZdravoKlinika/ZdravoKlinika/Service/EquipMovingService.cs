﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class EquipMovingService
    {
        public EquipMovingRepository equipMovingRepository { get; set; }
       
        public RoomService roomService { get; set; }

        public EquipmentService equipmentService { get; set; }
        public void CheckEquipMoving()
        {
            List<EquipMoving> equipMovings = new List<EquipMoving>(GetAllEquipMovings());
            foreach(EquipMoving equipMoving in equipMovings)
            {
                if (equipMoving.date <= System.DateTime.Now) {
                    foreach (int equipmentId in equipMoving.equipments)
                    {
                        var equip = equipmentService.GetById(equipmentId);
                        if(equip != null)
                        {
                            equip.roomId = equipMoving.roomTo;
                            equipmentService.Update(equip);
                        }
                    }
                    equipMovingRepository.DeleteById(equipMoving.id);
                }
            }
           
        }
        public EquipMovingService(EquipMovingRepository equipMovingRepository, RoomService roomService, EquipmentService equipmentService)
        {
            this.equipMovingRepository = equipMovingRepository;
            this.roomService = roomService;
            this.equipmentService = equipmentService;
        }

        public List<EquipMoving> GetAllEquipMovings()
        {
            return this.equipMovingRepository.GetAll();
        }

        public bool SaveEquipMoving(EquipMoving equipMoving)
        {
            if (this.equipMovingRepository.GetById(equipMoving.id) is null)
            {
                return this.equipMovingRepository.Save(equipMoving);
            }
            return false;
        }

        public EquipMoving? GetEquipMovingById(int id)
        {
            return this.equipMovingRepository.GetById(id);
        }

        public List<int> GetEquipIdsForMove(List<int> ids, int amount)
        {
            List<int> result = new List<int>();
            var moveEquips = GetAllEquipMovings();
            HashSet<int> equipIds = new HashSet<int>();
            foreach (var equip in moveEquips)
            {
                foreach(var e in equip.equipments)
                {
                    equipIds.Add(e);

                }
            }
            ids.RemoveAll(i => equipIds.Contains(i));
            if(ids.Count < amount)
            {
                throw new Exception("No equipments for moving.");
            }
            return ids.Take(amount).ToList();

            return result;

        }

        public bool DeleteEquipMovingById(int id)
        {
            return this.equipMovingRepository.DeleteById(id);
        }

       
        public int GenerateNewId()
        {
            return this.equipMovingRepository.GenerateNewId();
        }
    }
}