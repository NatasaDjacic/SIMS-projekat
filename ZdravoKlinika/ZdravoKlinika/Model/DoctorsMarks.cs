using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class DoctorsMarks
    {
        public string doctorJMBG { get; set; }
        public int Ones { get; set; }
        public int Twos { get; set; }

        public int Threes { get; set; }
        public int Fours { get; set; }
        public int Fives { get; set; }
        public double Average { get; set; }

        public DoctorsMarks(string doctorJMBG, int ones, int twos, int threes, int fours, int fives, double average)
        {
            this.doctorJMBG = doctorJMBG;
            Ones = ones;
            Twos = twos;
            Threes = threes;
            Fours = fours;
            Fives = fives;
            Average = average;
        }

        public DoctorsMarks()
        {
        }
    }
}
