using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;
using System.Text.RegularExpressions;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class EditPatient : Page, INotifyPropertyChanged, IDataErrorInfo {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string Error {
            get {
                return null;
            }
        }

        public string this[string name] {
            get {
                string result = null;
                switch (name) {
                    case "FirstName":
                        if (_firstName.Length <= 2) result = "First name should have more than 2 characters.";
                        else if (!Regex.IsMatch(_firstName, "^[a-zA-Z]*$")) result = "First name should contain only letters.";
                        break;
                    case "LastName":
                        if (_lastName.Length <= 2) result = "Last name should have more than 2 characters.";
                        else if (!Regex.IsMatch(_lastName, "^[a-zA-Z]*$")) result = "Last name should contain only letters.";
                        break;
                    case "BirthDate":
                        if (_birthDate >= DateTime.Today.AddMinutes(-1)) result = "Date of birth should be set.";
                        break;
                    case "Gender":
                        if (_gender == Gender.None) result = "Gender should be set.";
                        break;
                    case "Country":
                        if (_country.Trim().Length < 1) result = "Country should be set.";
                        break;
                    case "City":
                        if (_city.Trim().Length < 1) result = "City should be set.";
                        break;
                    case "Address":
                        if (_address.Trim().Length < 1) result = "Address should be set.";
                        break;
                    case "Email":
                        if (_email != "" && !Regex.IsMatch(_email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")) result = "Email fromat is not correct.";
                        break;
                    case "JMBG":
                        if (_JMBG.Trim() == "") result = "JMBG should be set.";
                        else if (_JMBG.Length != 13) result = "JMBG should have 13 digits.";
                        else if (!Regex.IsMatch(_JMBG, "^[1-9]*$")) result = "JMBG should have 13 digits.";
                        break;
                    default: break;
                }
                return result;
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

        public PatientController patientController;
        Patient? p;
        public EditPatient(string jmbg) {
            this.DataContext = this;
            PatientRepository patientRepository = new PatientRepository(@"..\..\..\Resource\Data\patient.json");
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            p = patientController.GetById(jmbg);
            if (p is not null) {
                FirstName = p.firstName;
                LastName = p.lastName;
                Gender = p.gender;
                Email = p.email ?? "";
                BirthDate = p.dateOfBirth;
                Country = p.country;
                City = p.city;
                Address = p.address;
                JMBG = p.JMBG;
                Allergy = String.Join("\r\n", p.allergens);
                Phone = p.phone;
                BloodType = p.bloodType;
            } else { NavigationService.GoBack(); }
            InitializeComponent();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e) {
            try {
                if (p is not null) {
                    p.firstName = FirstName;
                    p.lastName = LastName;
                    p.gender = Gender;
                    p.email = Email;
                    p.dateOfBirth = BirthDate;
                    p.country = Country;
                    p.city = City;
                    p.address = Address;
                    p.allergens = new List<string>(Allergy.Split("\r\n", StringSplitOptions.RemoveEmptyEntries));
                    p.phone = Phone;
                    p.bloodType = BloodType;
                    patientController.Update(p);
                }
                NavigationService.Navigate(new Patients());


            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
