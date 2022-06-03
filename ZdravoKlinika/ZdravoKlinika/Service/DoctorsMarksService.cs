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
            int number = 0;
            int sum = 0;
            DoctorsMarks doctorsMarks = new DoctorsMarks();
            foreach (Mark mark in GetAllMarks())
            {
                doctorsMarks.DoctorJMBG = mark.doctorJMBG;
                doctorsMarks.Username = doctorRepository.GetById(mark.doctorJMBG).username;
                doctorsMarks.FirstName = doctorRepository.GetById(mark.doctorJMBG).firstName;
                doctorsMarks.LastName = doctorRepository.GetById(mark.doctorJMBG).lastName;
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
