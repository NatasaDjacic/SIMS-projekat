using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;


namespace ZdravoKlinika.Service
{
    public class DrugService
    {
        public DrugRepository drugRepository { get; set; }


        public DrugService(DrugRepository drugRepository)
        {
            this.drugRepository = drugRepository; 
        }

        public List<Drug> GetAll()
        {
            return drugRepository.GetAll();
        }


        public bool Save(Drug drug)
        {
            throw new NotImplementedException();
        }


    }
}
