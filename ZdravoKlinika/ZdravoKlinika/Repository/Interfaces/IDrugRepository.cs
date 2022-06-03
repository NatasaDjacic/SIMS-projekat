using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository.Interfaces {
    public interface IAppointmentRepository: IGenericRepository<Appointment, int> {
        public int GenerateNewId();
    }
}
