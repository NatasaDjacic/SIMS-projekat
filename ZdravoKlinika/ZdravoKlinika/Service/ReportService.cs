using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Service
{
    public class ReportService
    {
        PatientService patientService;

        public ReportService(PatientService patientService)
        {
            this.patientService = patientService;
        }

        
        public bool AddReport(Patient patient, Report report)
        {
            report.reportId = Guid.NewGuid();
            patient.medicalRecord.reports.Add(report);
            return patientService.Update(patient);
        }

       

        

        
    }
}