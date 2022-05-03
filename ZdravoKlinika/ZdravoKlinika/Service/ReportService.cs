using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class ReportService
    {
        public ReportRepository reportRepository { get; set; }

        public ReportService(ReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
        }

        public bool Create(Report report)
        {
            if (this.reportRepository.GetById(report.reportId) is null)
            {
                return this.reportRepository.Save(report);
            }
            return false;
        }

        public List<Report> GetAll()
        {
            return this.reportRepository.GetAll();
        }

        public Report? GetById(int id)
        {
            return this.reportRepository.GetById(id);
        }

        public bool Save(Report report)
        {
            if (this.reportRepository.GetById(report.reportId) is null)
            {
                return this.reportRepository.Save(report);
            }
            return false;
        }

        public bool Update(Model.Report report)
        {
            if (this.reportRepository.GetById(report.reportId) is not null)
            {
                return !this.reportRepository.Save(report);
            }
            return false;
        }

        public bool Delete(int id)
        {
            return this.reportRepository.DeleteById(id);
        }
    }
}