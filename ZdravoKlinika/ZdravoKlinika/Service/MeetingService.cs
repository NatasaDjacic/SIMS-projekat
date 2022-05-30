﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;


namespace ZdravoKlinika.Service {
    public class MeetingService {

        MeetingRepository meetingRepository;
        public MeetingService(MeetingRepository meetingRepository) {
            this.meetingRepository = meetingRepository;
        
        }

        public List<Meeting> GetAll() { 
            return this.meetingRepository.GetAll();
        }

        public List<Meeting> GetAllInInterval(DateTime startTime, DateTime endTime) {
            return this.GetAll().Where(meeting => (meeting.endTime >= startTime && meeting.startTime <= endTime)).ToList(); ;
        }
        public bool SaveSuggestion(Meeting meeting) {
            meeting.id = this.meetingRepository.GenerateNewId();

            return this.meetingRepository.Save(meeting);
        }


    }
}
