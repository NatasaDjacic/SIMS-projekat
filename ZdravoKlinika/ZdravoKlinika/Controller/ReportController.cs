using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.Controller;

public class ReportController
{
    public Service.ReportService reportService;

    public ReportController(ReportService reportService)
    {
        this.reportService = reportService;
    }

    private PatientService patientService;
    private AuthService authService;

    public bool Create(int reportId, string diagnostica, DateTime date)
    {
        var report = new Report(reportId, diagnostica, date);
        return this.reportService.Create(report);
    }

    public bool Delete(int id)
    {
        return this.reportService.Delete(id);
    }

    public bool Update(Report report)
    {
        return this.reportService.Update(report);
    }

    public Report GetById(int id)
    {
        return reportService.GetById(id);
    }

    public List<Report> GetAll()
    {
        return reportService.GetAll();
    }

}
