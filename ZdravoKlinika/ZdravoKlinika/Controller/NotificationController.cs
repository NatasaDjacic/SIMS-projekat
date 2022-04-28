using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Controller {
    public class NotificationController {
        private NotificationService notificationService;
        public NotificationController(NotificationService notificationService) {
            this.notificationService = notificationService;
        }
        public List<Notification> GetMyNotifications() {
            return this.notificationService.getMyNotifications();
        }
    }
}
