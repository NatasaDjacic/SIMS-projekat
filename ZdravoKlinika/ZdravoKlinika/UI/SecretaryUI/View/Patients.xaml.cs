﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private ObservableCollection<Patient> patients;
        public ObservableCollection<Patient> PatientsCollection {
            get => patients;
            set {
                if (patients != value) {
                    patients = value;
                    OnPropertyChanged("PatientsCollection");
                }
            }
        }
        public PatientController patientController;
        public Patients() {
            PatientRepository patientRepository = new PatientRepository(@"..\..\..\Resource\Data\patient.json");
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            PatientsCollection = new ObservableCollection<Patient>(patientController.GetAll());
            this.DataContext = this;
            InitializeComponent();

        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e) {
            string? jmbg = ((Button)sender).Tag as string;
            Console.WriteLine(jmbg);
            if (jmbg is null) return;
            patientController.Delete(jmbg);
            PatientsCollection = new ObservableCollection<Patient>(patientController.GetAll());
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e) {
            string? jmbg = ((Button)sender).Tag as string;
            Console.WriteLine(jmbg);
            if (jmbg is null) return;
            NavigationService.Navigate(new EditPatient(jmbg));
        }

        private void Button_Click_New(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new AddPatient());
        }
        private void Button_Click_Guest(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new CreateGuest());
        }

    }
}
