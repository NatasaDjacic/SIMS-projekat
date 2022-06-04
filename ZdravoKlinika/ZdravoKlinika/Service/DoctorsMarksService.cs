using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service
{
    public class DoctorsMarksService
    {
        public IDoctorsMarksRepository doctorsMarksRepository;
        public IDoctorRepository doctorRepository;
        public MarkService markService = GLOBALS.markService;

        public DoctorsMarksService(IDoctorsMarksRepository doctorsMarksrepository, IDoctorRepository doctorRepository)
        {
            this.doctorsMarksRepository = doctorsMarksrepository;
            this.doctorRepository = doctorRepository;
        }
        public List<Mark> GetAllMarks()
        {
            return markService.GetAll();
        }
        public void calculateMarks()
        {
            Dictionary<string, DoctorsMarks> doctorsMarksDictionary = new Dictionary<string, DoctorsMarks>();
            foreach (Mark mark in GetAllMarks())
            {

                if (!doctorsMarksDictionary.ContainsKey(mark.doctorJMBG))
                {
                    var doctor = doctorRepository.GetById(mark.doctorJMBG);
                    if (doctor == null) continue;
                    var newDoctorsMarks = new DoctorsMarks(doctor);
                    doctorsMarksDictionary.Add(newDoctorsMarks.DoctorJMBG, newDoctorsMarks);

                }
                var doctorsMarks = doctorsMarksDictionary[mark.doctorJMBG];
                switch (mark.doctorMark)
                {
                    case 1:
                        doctorsMarks.Ones++;
                        break;
                    case 2:
                        doctorsMarks.Twos++;
                        break;
                    case 3:
                        doctorsMarks.Threes++;
                        break;
                    case 4:
                        doctorsMarks.Fours++;
                        break;
                    case 5:
                        doctorsMarks.Fives++;
                        break;
                    default: throw new Exception("Not valid marks.");
                }
                
            }
            foreach(DoctorsMarks doctorsMarks in doctorsMarksDictionary.Values)
            {
                doctorsMarks.CalculateAverage();
                doctorsMarksRepository.Save(doctorsMarks);

            }
        }
        public List<DoctorsMarks> GetAll()
        {
            return this.doctorsMarksRepository.GetAll();
        }
    }
}
