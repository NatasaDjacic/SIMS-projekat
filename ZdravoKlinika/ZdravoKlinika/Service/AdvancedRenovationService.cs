using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class AdvancedRenovationService
    {
        AdvancedRenovationRepository advancedRenovationRepository;

        public AdvancedRenovationService(AdvancedRenovationRepository advancedRenovationRepository)
        {
            this.advancedRenovationRepository = advancedRenovationRepository;
        }
        public List<AdvancedRenovation> GetAll()
        {
            return this.advancedRenovationRepository.GetAll();
        }
        public int GenerateNewId()
        {
            return this.advancedRenovationRepository.GenerateNewId();
        }
        public bool Save(AdvancedRenovation advRen)
        {
            return this.advancedRenovationRepository.Save(advRen);
        }
        public List<AdvancedRenovation> GetAllInInterval(DateTime start, DateTime end)
        {
            return this.GetAll().Where(a => (a.startTime.AddMinutes(a.duration) >= start && a.startTime <= end)).ToList();
        }
    }
}
