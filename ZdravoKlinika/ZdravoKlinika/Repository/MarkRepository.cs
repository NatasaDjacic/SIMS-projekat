using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using Newtonsoft.Json;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Repository
{
    public class MarkRepository: IMarkRepository
    {
        private string fileLocation { get; set; }

        public MarkRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }


        public List<Mark> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Mark>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Mark>();
            }
            return values;
        }

        public List<Mark> GetByPatient(string patientJMBG)
        {
            var values = this.GetAll();
            List<Mark> result = new List<Mark>();

            foreach(Mark m in values)
            {
                if(m.patientJMBG==patientJMBG)
                {
                    result.Add(m);
                }
            }

            return result;
        }

        public bool Save(Mark mark)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => mark.id == value.id);
            if (found != -1)
            {
                values[found] = mark;
            }
            else
            {
                values.Add(mark);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Mark? GetById(int id)
        {
            var values = this.GetAll();
            return values.Find(value => id == value.id);
        }


        public int GenerateNewId()
        {
            return GetAll().Max(a => a.id) + 1;
        }

        public bool DeleteById(int id)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.id == id);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
        }

    }
}
