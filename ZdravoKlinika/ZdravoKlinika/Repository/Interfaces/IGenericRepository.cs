using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Repository.Interfaces {
    public interface IGenericRepository<T, U> {

        public List<T> GetAll();
        public bool Save(T model);
        public T? GetById(U id);
        public bool DeleteById(U id);
    }
}
