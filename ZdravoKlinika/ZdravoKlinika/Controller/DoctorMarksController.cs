using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class DoctorMarksController
    {
        public DoctorsMarksService doctorsMarksService { get; set;}

        public DoctorMarksController(DoctorsMarksService doctorsMarksService)
        {
            this.doctorsMarksService = doctorsMarksService;
        }
        public void calculateMarks()
        {
            doctorsMarksService.calculateMarks();
        }
        public List<DoctorsMarks> GetAll()
        {
            return doctorsMarksService.GetAll();
        }
    }
}
