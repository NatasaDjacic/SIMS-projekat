using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service
{
    public class ManagerService
    {
        public IManagerRepository managerRepository { get; set; }

        public ManagerService(IManagerRepository managerRepository)
        {
            this.managerRepository = managerRepository;
        }

      /*  public Manager GetManager()
        {
            return this.managerRepository.
        }*/
    }
}
