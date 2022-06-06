using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;
using ZdravoKlinika.Controller;


namespace ZdravoKlinika.Controller
{
    public class PrescriptionController
    {
        PrescriptionService prescriptionService;
        DrugController drugController;
        public PrescriptionController(PrescriptionService prescriptionService)
        {
            this.prescriptionService = prescriptionService;
        }

        public Prescription? AddPrescription(Patient patient, Guid reportId, int drugId, string description, int useDuration, int useFrequency, double useAmount)
        {
            Prescription prescription = new Prescription(Guid.NewGuid(), drugId, description, useDuration, useFrequency, useAmount , drugController.GetById(drugId).name);
            if(this.prescriptionService.AddPrescription(patient, reportId, prescription)) { 
                // Todo create notifications
                return prescription;
            }

            return null;
        }
        public List<Prescription>? GetPrescriptions(Patient patient, Guid reportId)
        {
            return patient.medicalRecord.reports.Find(r => r.reportId == reportId)?.prescriptions;
        }

      


    }
}
