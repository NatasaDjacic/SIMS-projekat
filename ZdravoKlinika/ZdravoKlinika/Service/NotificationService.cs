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

        public void NotificationForAppointmentCreated(Appointment app) {
            var docNotif = new Notification(this.GenerateNewId(), app.doctorJMBG, "New appointment", String.Format("You have new appointment at {0} in room {1}.",app.startTime, app.roomId), DateTime.Now);
            this.notificationRepository.Save(docNotif);
            var patNotif = new Notification(this.GenerateNewId(), app.patientJMBG, "New appointment", String.Format("You have new appointment at {0} in room {1}.", app.startTime, app.roomId), DateTime.Now);
            this.notificationRepository.Save(patNotif);
        }
        public void NotificationForAppointmentMoved(Appointment app, DateTime oldTime) {
            var docNotif = new Notification(this.GenerateNewId(), app.doctorJMBG, "Moved appointment", String.Format("Your appointment at {0} in room {1} is moved to {2}.", oldTime, app.roomId, app.startTime), DateTime.Now);
            this.notificationRepository.Save(docNotif);
            var patNotif = new Notification(this.GenerateNewId(), app.patientJMBG, "Moved appointment", String.Format("Your appointment at {0} in room {1} is moved to {2}.", oldTime, app.roomId, app.startTime), DateTime.Now);
            this.notificationRepository.Save(patNotif);
        }
        public void NotificationForAppointmentCancel(Appointment app) {
            var docNotif = new Notification(this.GenerateNewId(), app.doctorJMBG, "Canceled appointment", String.Format("Your appointment at {0} in room {1} has been canceled.", app.startTime, app.roomId ), DateTime.Now);
            this.notificationRepository.Save(docNotif);
            var patNotif = new Notification(this.GenerateNewId(), app.patientJMBG, "Canceled appointment", String.Format("Your appointment at {0} in room {1} has been canceled.", app.startTime, app.roomId), DateTime.Now);
            this.notificationRepository.Save(patNotif);
        }

        public int GenerateNewId() {
            return this.notificationRepository.GenerateNewId();
        }
    }
}
