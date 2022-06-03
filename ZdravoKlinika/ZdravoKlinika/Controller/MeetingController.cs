using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Controller {
    public class MeetingController {
        MeetingService meetingService;
        NotificationService notificationService;
        public MeetingController(MeetingService meetingService, NotificationService notificationService) {
            this.meetingService = meetingService;
            this.notificationService = notificationService;
        }

        public List<Meeting> GetAll() {
            return this.meetingService.GetAll();
        }
        public bool SaveSuggestion(Meeting meeting) {
            notificationService.NotificationForMeetingCreated(meeting);
            return this.meetingService.SaveSuggestion(meeting);
        }
    }
}
