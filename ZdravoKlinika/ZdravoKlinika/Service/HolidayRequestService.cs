using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Service
{
    public class HolidayRequestService
    {
        HolidayRequestRepository holidayRequestRepository;

        public HolidayRequestService(HolidayRequestRepository holidayRequestRepository) {
            this.holidayRequestRepository = holidayRequestRepository;
        }

        public List<HolidayRequest> GetAll() {
            return this.holidayRequestRepository.GetAll();
        }
        public List<HolidayRequest> GetAllInInterval(DateTime startTime, DateTime endTime) {

            return this.GetAll().Where(hr => (hr.endTime >= startTime && hr.startTime <= endTime)).ToList();
        }

        public HolidayRequest? GetById(int id) { 
            return this.holidayRequestRepository.GetById(id);
        }

        public bool Save(HolidayRequest holidayRequest) {
            return this.holidayRequestRepository.Save(holidayRequest);

        }
    }
}
