using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class RenovationController
    {
        RenovationService renovationService;

        public RenovationController(RenovationService renovationService) {
            this.renovationService = renovationService;
        }

        public bool SaveRenovation(DateTime startTime, int duration, string roomId, string description)
        {
          
            Renovation renovation = new Renovation();
            renovation.startTime = startTime;
            renovation.duration = duration;
            renovation.roomId = roomId;
            var id = renovation.renovationId = renovationService.GenerateNewId();
            renovation.description = description;



            return renovationService.Save(renovation);

        }
    }
}
