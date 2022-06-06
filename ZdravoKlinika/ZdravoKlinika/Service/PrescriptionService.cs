using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;

namespace ZdravoKlinika.Service
{
    public class PrescriptionService
    {
        PatientService patientService;

        public PrescriptionService(PatientService patientService)
        {
            this.patientService = patientService;
        }

        public bool AddPrescription(Patient patient,Guid reportId ,Prescription prescription)
        {
            Report? report = patient.medicalRecord.reports.Find(r => r.reportId == reportId);
            if (report == null) return false;
            report.prescriptions.Add(prescription);
            return patientService.Update(patient);
        }

        public List<Prescription>? GetAllPrescriptions(Patient patient)
        { 
            if(patient != null) {
            
                var patientPrescriptions = patient.medicalRecord.reports.SelectMany(report => report.prescriptions).ToList();
                return patientPrescriptions;
            }

            return null;
        }

        public void SavingPatientNote(Patient patient, Guid prescriptionId,string note)
        {
            var patientPrescriptions = patient.medicalRecord.reports.SelectMany(report => report.prescriptions).ToList();
            var prescription=patientPrescriptions.Find(r => r.prescriptionId == prescriptionId);
            prescription.patient_note=note;
           
           patientService.Update(patient);
            Console.WriteLine(prescription.patient_note);
        }


    }
}
