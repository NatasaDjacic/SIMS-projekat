﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Model {
    public class Renovation {
        public int renovationId;
        public string roomId;
        public DateTime startTime;
        public int duration;
        public string description;
        public Renovation(int id, string roomId, DateTime startTime, int duration, string description) { 
            this.renovationId = id;
            this.roomId = roomId;
            this.startTime = startTime;
            this.duration = duration;
            this.description = description;
        }
        public Renovation() { }

    }
}
