using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using Newtonsoft.Json;
using System.IO;


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

        public bool Save(Report report)
        {
            bool added = false;
            var values = this.GetAll();
            var found = values.FindIndex(value => report.reportId.Equals(value.reportId));
            if (found != -1)
            {
                values[found] = report;
            }
            else
            {
                values.Add(report);
                added = true;
            }
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return added;
        }

        public Report? GetById(int id)
        {
            var values = this.GetAll();
            return values.Find(value => id == value.reportId);
        }

        public bool DeleteById(int reportId)
        {
            var values = this.GetAll();
            var deleted = values.RemoveAll(value => value.reportId == reportId);
            File.WriteAllText(fileLocation, JsonConvert.SerializeObject(values, Formatting.Indented));
            return deleted > 0;
        }

    }
}
