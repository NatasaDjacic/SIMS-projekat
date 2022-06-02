using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.UI.ManagerUI.View
{
    public partial class Profile : Page, INotifyPropertyChanged
    {
        string val = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
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

       


        public Profile(string value)
        {
            val = value;
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
            else { NavigationService.GoBack(); }
            this.DataContext = this;
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (value)
            {
                case "en":
                    Console.WriteLine("en");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.en.xaml", UriKind.Relative);
                    break;
                case "rus":
                    Console.WriteLine("rus");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.rus.xaml", UriKind.Relative);
                    break;
                case "srb":
                    Console.WriteLine("srb");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dictionary);
            InitializeComponent();
        }

        private void btnShow_MouseDown(object sender, EventArgs e)
        {
            Password = manager.password;
        }

        private void btnShow_MouseReleased(object sender, EventArgs e)
        {
            Password = "******";
        }
    }
}
