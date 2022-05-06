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
    public ReportService reportService;
    
    public ReportController(ReportService reportService)
    {
        this.reportService = reportService;
    }

    public List<Report> GetAll(Patient patient)
    {
        return patient.medicalRecord.reports;
    }

    public Report? AddReport(Patient patient, string diagnostica, string description, DateTime date)
    {
        var report = new Report(Guid.NewGuid(), diagnostica, description, date);
        if(reportService.AddReport(patient, report)){
            return report;
        }
        return null;
    }



}
