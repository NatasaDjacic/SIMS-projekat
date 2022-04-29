using System;
using System.Collections.Generic;
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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;
using System.ComponentModel;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private string _firstName = "";
        private string _lastName = "";
        private string _email = "";
        private DateTime _birthDate = DateTime.Now;
        private string _country = "";
        private string _city = "";
        private string _address = "";
        private string _JMBG = "";
        private BloodType _bloodType = BloodType.None;
        private Gender _gender = Gender.None;
        private string _phone = "";
        private string _allergy = "";

        public string FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged("FirstaName"); } }
        public string LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged("LastName"); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged("Email"); } }
        public DateTime BirthDate { get { return _birthDate; } set { _birthDate = value; OnPropertyChanged("BirthDate"); } }
        public string Country { get { return _country; } set { _country = value; OnPropertyChanged("Country"); } }
        public string City { get { return _city; } set { _city = value; OnPropertyChanged("City"); } }
        public string Address { get { return _address; } set { _address = value; OnPropertyChanged("Address"); } }
        public string JMBG { get { return _JMBG; } set { _JMBG = value; OnPropertyChanged("JMBG"); } }
        public BloodType BloodType { get { return _bloodType; } set { _bloodType = value; OnPropertyChanged("BloodType"); } }
        public Gender Gender { get { return _gender; } set { _gender = value; OnPropertyChanged("Gender"); } }
        public string Phone { get { return _phone; } set { _phone = value; OnPropertyChanged("Phone"); } }
        public string Allergy { get { return _allergy; } set { _allergy = value; OnPropertyChanged("Allergy"); } }

        public PatientController patientController = GLOBALS.patientController;
        public AddPatient() {
            this.DataContext = this;
            InitializeComponent();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e) {
            try {
                patientController.Create(FirstName, LastName, JMBG, JMBG,
                    BirthDate, Phone, Email, Country, City, Address, Gender, BloodType, new List<string>(Allergy.Split("\r\n",StringSplitOptions.RemoveEmptyEntries)));
                NavigationService.Navigate(new Patients());
                

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
