using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using Newtonsoft.Json;
/*
namespace ZdravoKlinika.Repository
{
    public class ReportRepository
    {


        private string fileLocation { get; set; }

        public ReportRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public List<Report> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Report>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Report>();
            }
            return values;
        }


        public bool Save(Report r)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => r.reportId == value.reportId);
            if (found != -1)
            {
                values[found] = r;
            }
            else
            {
                values.Add(r);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }




        public Report? GetById(int reportId)
        {
            var values = this.GetAll();
            return values.Find(value => reportId == value.reportId);
        }



        public bool DeleteById(int reportId)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.reportId == reportId);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
        }


        public int GenerateNewId()
        {
            return GetAll().Max(a => a.reportId) + 1;
        }



    }
}
*/