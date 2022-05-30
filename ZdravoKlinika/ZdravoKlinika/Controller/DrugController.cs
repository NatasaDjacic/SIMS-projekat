using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.Controller
{
    public class DrugController
    {
        public Service.DrugService drugService;
        public DrugController(DrugService drugService)
        {
            this.drugService = drugService;
        }
        public List<Drug> GetAll()
        {
            return drugService.GetAll();
        }
        public bool Create(string name, string ingredients, string alternative)
        {
            Drug drug = new Drug();
            drug.drugId = drugService.GenerateNewId();
            drug.name = name;
            drug.ingredients = ingredients;
            drug.alternative = alternative;
            drug.comment = "";
            drug.approved = Model.Enums.DrugStatus.OnHold;
            return this.drugService.Create(drug);
        }
        public Drug GetById(int drugId)
        {
            return drugService.GetById(drugId);
        }
        public bool Update(Drug drug)
        {
            return this.drugService.Update(drug);
        }


    }
}
