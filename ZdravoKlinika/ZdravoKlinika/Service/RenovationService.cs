using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service {
    public class RenovationService {
        RenovationRepository renovationRepository;

        public RenovationService(RenovationRepository renovationRepository) { 
            this.renovationRepository = renovationRepository;
        }
        public List<Renovation> GetAll() {
            return this.renovationRepository.GetAll();
        }

    }
}
