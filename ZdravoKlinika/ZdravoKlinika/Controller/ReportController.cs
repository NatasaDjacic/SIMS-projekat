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
    public PatientService patientService;

    public ReportController(ReportService reportService)
    {
        this.reportService = reportService;
    }


   /* public bool Create(Patient patient, int reportId, string diagnostica, string description, DateTime date, List<Prescription> prescriptions)
    {
        var report = new Report(patient, reportId, diagnostica, description, date, prescriptions);
        return this.reportService.Create(report);
    }*/

    

}
