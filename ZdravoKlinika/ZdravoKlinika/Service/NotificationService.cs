using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Service {
    public class NotificationService {

        private NotificationRepository notificationRepository { get; set; }
        private AuthService authService;
        public NotificationService(NotificationRepository notificationRepository, AuthService authService) {
            this.notificationRepository = notificationRepository;
            this.authService = authService;
        }
        public List<Notification> getMyNotifications() {
            if (authService.user == null) throw new Exception("User not loggedin");
            return this.notificationRepository.GetAll().FindAll(n => n.JMBG == authService.user.JMBG && !n.seen);
        }
    }
}
