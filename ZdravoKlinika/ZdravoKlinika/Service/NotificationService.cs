using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service {
    public class NotificationService {

        private INotificationRepository notificationRepository { get; set; }
        private AuthService authService;
        public NotificationService(INotificationRepository notificationRepository, AuthService authService) {
            this.notificationRepository = notificationRepository;
            this.authService = authService;
        }
        public List<Notification> getMyNotifications() {
            if (authService.user == null) throw new Exception("User not logged in");
            return this.notificationRepository.GetAll().FindAll(n => n.JMBG == authService.user.JMBG && !n.seen);
        }

        public List<Notification> getPatientNotifications()
        {
            if (authService.user == null) throw new Exception("User not logged in");
            return this.notificationRepository.GetAll().FindAll(n => n.JMBG == authService.user.JMBG && n.showDate<=DateTime.Now.AddHours(2) && n.showDate>=DateTime.Now);
        }

        public List<Notification> getPatientPrescriptionNotifications()
        {
            List<Notification> notifications = new List<Notification>();
            Patient? patient = authService.user as Patient;
            if (patient == null) throw new Exception("Patient not looged in");
            patient.medicalRecord.reports.ForEach(report => {
                notifications.AddRange(ConvertReportPrescriptionToNotification(patient, report));
            });
            return notifications;

        }
        private List<Notification> ConvertReportPrescriptionToNotification(Patient patient,Report report) {

            List<Notification> notifications = new List<Notification>();
            report.prescriptions.ForEach(prescription =>
            {
                var interval = 24 / prescription.useFrequency;

                if (report.date.AddDays(prescription.useDuration) >= DateTime.Today && DateTime.Now.Hour % interval >= interval - 1) {
                    notifications.Add(new Notification(-1, patient.JMBG, "Take medicine", String.Format("Take drug:{0} this amount {1}.", prescription.drugId, prescription.useAmount), DateTime.Now));
                }

            });
            return notifications;
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
        public void NotificationForMeetingCreated(Meeting meeting) {
            meeting.attendeesJMBG.ForEach(attendeJMBG => {
                var attendeNotification = new Notification(
                    this.GenerateNewId(), 
                    attendeJMBG, 
                    String.Format("New Meeting - {0}", meeting.title),
                    String.Format("You have new meeting at {0} in room {1}.", meeting.startTime, meeting.roomId), 
                    DateTime.Now);
                this.notificationRepository.Save(attendeNotification);
            });
        }
        public int GenerateNewId() {
            return this.notificationRepository.GenerateNewId();
        }
    }
}
