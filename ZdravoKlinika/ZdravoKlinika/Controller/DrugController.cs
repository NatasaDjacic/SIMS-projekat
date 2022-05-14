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
        public bool Create(string name, string ingredients)
        {
            Drug drug = new Drug();
            drug.drugId = drugService.GenerateNewId();
            drug.name = name;
            drug.ingredients = ingredients;
            drug.comment = "";
            drug.approved = false;
            return this.drugService.Create(drug);
        }

    }
}
