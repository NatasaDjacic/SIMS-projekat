using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class DoctorsMarksRepository
    {
        private string fileLocation { get; set; }

        public DoctorsMarksRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }
        public List<DoctorsMarks> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<DoctorsMarks>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<DoctorsMarks>();
            }
            return values;
        }
        public DoctorsMarks? GetByJMBG(string jmbg)
        {
            var values = this.GetAll();
            Console.WriteLine(values);
            return values.Find(value => jmbg == value.doctorJMBG);
        }
        public void Save(DoctorsMarks doctorsMarks)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => doctorsMarks.doctorJMBG == value.doctorJMBG);
            if (found != -1)
            {
                values[found] = doctorsMarks;
            }
            else
            {
                values.Add(doctorsMarks);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
        }
       
    }
}
