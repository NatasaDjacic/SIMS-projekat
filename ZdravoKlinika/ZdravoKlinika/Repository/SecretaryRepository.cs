using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.Repository
{
    public class SecretaryRepository
    {
        private string fileLocation { get; set; }

        public SecretaryRepository(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public List<Secretary> GetAll()
        {
            var values = JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText(fileLocation));
            if (values == null)
            {
                values = new List<Secretary>();
            }
            return values;
        }

        public bool Save(Doctor doctor)
        {

            throw new NotImplementedException();
        }

        public Secretary? GetById(string id)
        {
            var values = this.GetAll();
            return values.Find(value => id.Equals(value.JMBG));
        }

        public bool DeleteById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
