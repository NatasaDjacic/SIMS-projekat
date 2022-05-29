using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Controller
{
    public class HolidayRequestController
    {
        HolidayRequestService holidayRequestService;
        public HolidayRequestController(HolidayRequestService holidayRequestService) {
            this.holidayRequestService = holidayRequestService;
        }

        public List<HolidayRequest> GetAll() {
            return this.holidayRequestService.GetAll();
        }
        public HolidayRequest GetById(int id) {
            var holidayRequest = this.holidayRequestService.GetById(id);
            if(holidayRequest is null) throw new Exception("Not Found");
            return holidayRequest;
        }
        public void Accept(int id) {
            var holidayRequest = this.GetById(id);

            holidayRequest.status = RequestType.ACCEPTED;

            this.holidayRequestService.Save(holidayRequest);
        }

        public void Decline(int id, string declineReason) {
            var holidayRequest = this.GetById(id);

            holidayRequest.status = RequestType.DECLINED;
            holidayRequest.declineReason = declineReason;

            this.holidayRequestService.Save(holidayRequest);
        }
    }
}
