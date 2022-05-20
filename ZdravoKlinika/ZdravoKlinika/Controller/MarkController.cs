using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class MarkController
    {
        
        public MarkService markService { get; set; }

        public MarkController(MarkService markService) { this.markService = markService; }

        public List<Mark> GetAll()
        {
            return markService.GetAll();
        }


        public bool Save(int id, int hospitalMark, int doctorMark, string patientJMBG, string doctorJMBG, Guid reportId)
        {
            return this.markService.Save(id, hospitalMark, doctorMark, patientJMBG, doctorJMBG, reportId);
        }

        public int GenerateNewId()
        {
            return this.markService.GenerateNewId();
        }

        public Mark? GetById(int id)
        {
            return this.markService.GetById(id);
        }


        public Mark? GetByReportAndPatient(Guid reportId, string patientJMBG)
        {

            return markService.GetByReportAndPatient(reportId,patientJMBG);
        }


        public bool DeleteById(int id)
        {
            return this.markService.DeleteById(id);
        }


    }
}
