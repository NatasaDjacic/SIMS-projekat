using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class PrescriptionController
    {
        private PrescriptionService prescriptionService = new PrescriptionService();
        public NotificationService notificationService;
        public PatientService patientService;

        public PrescriptionController(PrescriptionService prescriptionService)
        {
            this.prescriptionService = prescriptionService;
        }


        /*public bool Create(int drugId, string description, int useDuration, int useFrequency, double useAmount)
        {
            var prescription = new Prescription(drugId, description, useDuration, useFrequency, useAmount);
            return this.prescriptionService.Create(prescription);
        }*/

        /*public bool Add(Prescription prescription)
        {

        }*/
    }
}
