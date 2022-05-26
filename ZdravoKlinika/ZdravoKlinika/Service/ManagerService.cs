using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class ManagerService
    {
        public ManagerRepository managerRepository { get; set; }

        public ManagerService(ManagerRepository managerRepository)
        {
            this.managerRepository = managerRepository;
        }

      /*  public Manager GetManager()
        {
            return this.managerRepository.
        }*/
    }
}
