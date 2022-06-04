using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class DoctorsMarks
    {
        public string DoctorJMBG { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Ones { get; set; }
        public int Twos { get; set; }

        public int Threes { get; set; }
        public int Fours { get; set; }
        public int Fives { get; set; }
        public double Average { get; set; }

        public DoctorsMarks(string DoctorJMBG, string Username, string FirstName, string LastName, int ones, int twos, int threes, int fours, int fives, double average)
        {
            this.DoctorJMBG = DoctorJMBG;
            this.Username = Username;
            this.FirstName = FirstName;
            this.LastName = LastName;
            Ones = ones;
            Twos = twos;
            Threes = threes;
            Fours = fours;
            Fives = fives;
            Average = average;
        }

        public DoctorsMarks(Doctor doctor)
        {
            this.DoctorJMBG = doctor.JMBG;
            this.Username = doctor.username;
            this.FirstName = doctor.firstName;
            this.LastName = doctor.lastName;
            Ones = 0;
            Twos = 0;
            Threes = 0;
            Fours = 0;
            Fives = 0;
            Average = 0;
        }

        public DoctorsMarks()
        {
        }
        public void CalculateAverage() {
            var sum = Ones * 1 + Twos * 2 + Threes * 3 + Fours * 4 + Fives * 5 ;
            var N = Ones + Twos + Threes + Fours + Fives;
            Average = (double)sum / N;
        }
    }
}
