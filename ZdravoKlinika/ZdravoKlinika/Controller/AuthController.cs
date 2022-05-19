using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller {
    public class AuthController {
        private AuthService authService { get; set; }
        public AuthController( AuthService authService) { 
            this.authService = authService;
        }

        public bool Login(string username, string password) { 
            return this.authService.Login(username, password);
        }
        public void Logout() {
            this.authService.Logout();
        }

        public void Restrict()
        {
            this.authService.Restrict();
        }
    }
}
