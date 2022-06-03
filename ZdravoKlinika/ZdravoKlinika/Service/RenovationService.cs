using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service {
    public class RenovationService {
        IRenovationRepository renovationRepository;

        public RenovationService(IRenovationRepository renovationRepository) { 
            this.renovationRepository = renovationRepository;
        }
        public List<Renovation> GetAll() {
            return this.renovationRepository.GetAll();
        }
        public int GenerateNewId()
        {
            return this.renovationRepository.GenerateNewId();
        }
        public bool Save(Renovation ren)
        {
            return this.renovationRepository.Save(ren);
        }
        public List<Renovation> GetAllInInterval(DateTime start, DateTime end) {
            return this.GetAll().Where(a => (a.startTime.AddMinutes(a.duration) >= start && a.startTime <= end)).ToList();
        }
    }
}
