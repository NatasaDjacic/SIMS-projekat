using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Controller {
    public class EmployeController {

        EmployeService employeService;
        public EmployeController(EmployeService employeService) { 
            this.employeService = employeService;
        }
        public List<User> GetAll() {
            return this.employeService.GetAll();
        }
    }
}
