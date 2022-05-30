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
        public MeetingController(MeetingService meetingService) {
            this.meetingService = meetingService;
        }

        public List<Meeting> GetAll() {
            return this.meetingService.GetAll();
        }
        public bool SaveSuggestion(Meeting meeting) {
            return this.meetingService.SaveSuggestion(meeting);
        }
    }
}
