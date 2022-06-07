using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.UI.ManagerUI;

namespace ZdravoKlinika.UI.ManagerUI.ViewModel
{
    public class ProfileVM : BaseVM
    {
        private string name = "";
        private string lastName = "";
        private string jmbg = "";
        private string userName = "";
        private string password = "";
        private string phone = "";
        private string email = "";
        private string country = "";
        private string city = "";
        private string address = "";

        public string FirstName { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string LastName { get { return lastName; } set { lastName = value; OnPropertyChanged("LastName"); } }
        public string JMBG { get { return jmbg; } set { jmbg = value; OnPropertyChanged("JMBG"); } }
        public string UserName { get { return userName; } set { userName = value; OnPropertyChanged("UserName"); } }
        public string Password { get { return password; } set { password = value; OnPropertyChanged("Password"); } }
        public string Phone { get { return phone; } set { phone = value; OnPropertyChanged("Phone"); } }
        public string Email { get { return email; } set { email = value; OnPropertyChanged("Email"); } }
        public string Country { get { return country; } set { country = value; OnPropertyChanged("Country"); } }
        public string City { get { return city; } set { city = value; OnPropertyChanged("City"); } }
        public string Address { get { return address; } set { address = value; OnPropertyChanged("Address"); } }
        private Manager manager;


        public ProfileVM()
        {
            ManagerRepository managerRepository = new ManagerRepository(@"..\..\..\Resource\Data\manager.json");
            manager = managerRepository.GetManager();
            if (manager is not null)
            {
                FirstName = manager.firstName;
                LastName = manager.lastName;
                JMBG = manager.JMBG;
                UserName = manager.username;
                Password = "******";
                Phone = manager.phone;
                Email = manager.email;
                Country = manager.country;
                City = manager.city;
                Address = manager.address;
            }
        }
        public void btnShow_MouseDown()
        {
            Password = manager.password;
        }

        public void btnShow_MouseReleased()
        {
            Password = "******";
        }
        }
}
