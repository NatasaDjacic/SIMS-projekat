﻿using System;
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
using System.Text.RegularExpressions;
using ZdravoKlinika.Model;
using System.ComponentModel;
using ZdravoKlinika.Model.Enums;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Page, INotifyPropertyChanged, IDataErrorInfo {
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
                        if (!Regex.IsMatch(_firstName, "^[a-zA-Z]*$")) result = "First name should contain only letters.";
                    break;
                    case "LastName":
                        if (_lastName.Length <= 2) result = "Last name should have more than 2 characters.";
                        if (!Regex.IsMatch(_lastName, "^[a-zA-Z]*$")) result = "Last name should contain only letters.";
                    break;
                    case "BirthDate":
                        if (_birthDate >= DateTime.Today.AddMinutes(-1)) result = "Date of birth should be set.";
                    break;
                    case "Gender":
                        if (_gender==Gender.None) result = "Gender should be set.";
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
                        if (_JMBG.Length != 13) result = "JMBG should have 13 digits.";
                        if (!Regex.IsMatch(_JMBG, "^[1-9]*$")) result = "JMBG should have 13 digits.";
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
