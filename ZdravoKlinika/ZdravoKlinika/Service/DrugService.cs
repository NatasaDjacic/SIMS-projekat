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

        public Drug GetById(int drugId)
        {
            return this.drugRepository.GetById(drugId);
        }
        public List<Drug> GetAll()
        {
            return drugRepository.GetAll();
        }


        public bool Create(Drug drug)
        {
            if (this.drugRepository.GetById(drug.drugId) is null)
            {
                return this.drugRepository.Save(drug);
            }
            return false;
        }
        public int GenerateNewId()
        {
            return this.drugRepository.GenerateNewId();
        }
        public bool Update(Drug drug)
        {
            if (this.drugRepository.GetById(drug.drugId) is not null)
            {
                return !this.drugRepository.Save(drug);
            }
            return false;
        }

    }
}
