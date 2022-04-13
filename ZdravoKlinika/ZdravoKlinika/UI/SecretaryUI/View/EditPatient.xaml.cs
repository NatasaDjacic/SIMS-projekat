﻿using System;
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

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class EditPatient : Page, INotifyPropertyChanged {
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
                Gender = p.gender ?? Gender.None;
                Email = p.email ?? "";
                BirthDate = p.dateOfBirth;
                Country = p.country;
                City = p.city;
                Address = p.address;
                JMBG = p.JMBG;
                Allergy = String.Join("\r\n", p.allergens);
                Phone = p.phone;
                BloodType = p.bloodType ?? BloodType.None;
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
