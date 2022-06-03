using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository.Interfaces {
    public interface IEquipmentRepository : IGenericRepository<Equipment, int> {
        public int GenerateNewId();
        public List<Equipment> FindAll(string name);
        public List<Equipment> GetAllDynamic();
    }
}
