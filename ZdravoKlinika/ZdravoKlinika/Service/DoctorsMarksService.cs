using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class DoctorsMarksService
    {
        public DoctorsMarksRepository doctorsMarksRepository;
        public MarkService markService = GLOBALS.markService;

        public DoctorsMarksService(DoctorsMarksRepository doctorsMarksrepository)
        {
            this.doctorsMarksRepository = doctorsMarksrepository;
        }
        public List<Mark> GetAllMarks()
        {
            return markService.GetAll();
        }
        public void calculateMarks()
        {
            int number = 0;
            int sum = 0;
            DoctorsMarks doctorsMarks = new DoctorsMarks();
            foreach (Mark mark in GetAllMarks())
            {
                doctorsMarks.doctorJMBG = mark.doctorJMBG;
                doctorsMarks.Ones = 0;
                doctorsMarks.Twos = 0;
                doctorsMarks.Threes = 0;
                doctorsMarks.Fours = 0;
                doctorsMarks.Fives = 0;
                doctorsMarks.Average = 0;

                this.doctorsMarksRepository.Save(doctorsMarks);

            }
                foreach (Mark mark in GetAllMarks())
            {
                number++;
                sum = sum + mark.doctorMark;
                doctorsMarks = this.doctorsMarksRepository.GetByJMBG(mark.doctorJMBG);
                Console.WriteLine(doctorsMarks);
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
                doctorsMarks.Average = sum / number;
                doctorsMarksRepository.Save(doctorsMarks);
            }
        }
        public List<DoctorsMarks> GetAll()
        {
            return this.doctorsMarksRepository.GetAll();
        }
    }
}
