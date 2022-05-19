using System;
using System.Collections.Generic;
using ZdravoKlinika.Model;
using System.Linq;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.Service
{
    public class MarkService
    {

        public MarkRepository markRepository { get; set; }

        public MarkService(MarkRepository markRepository)
        {
            this.markRepository = markRepository;
        }

        public List<Mark> GetAll()
        {
            return this.markRepository.GetAll();
        }

        public bool Save(int id, int hospitalMark, int doctorMark, string patientJMBG, string doctorJMBG, Guid reportId)
        {
            Mark mark=new Mark(id,hospitalMark,doctorMark,patientJMBG,doctorJMBG,reportId);
            return this.markRepository.Save(mark);
        }

        public int GenerateNewId()
        {
            return this.markRepository.GenerateNewId();
        }

        public Mark? GetById(int id)
        {
            return this.markRepository.GetById(id);
        }

        public Mark? GetOne(Guid reportId,string patientJMBG)
        {

            List<Mark> all = this.GetAll();

            foreach(Mark mark in all)
            {
                if(mark.reportId==reportId && mark.patientJMBG==patientJMBG)
                {
                    return mark;
                }
            }

            return null;
        }

        



    }

}
