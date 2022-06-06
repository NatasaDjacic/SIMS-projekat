
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
using System.Collections.ObjectModel;
using System.ComponentModel;
using ZdravoKlinika.UI;

namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Prescriptions : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public List<Prescription> prescriptions;
        public List<Prescription> PrescriptionsCollection
        {
            get => prescriptions;
            set
            {
                if (prescriptions != value)
                {
                    prescriptions = value;
                    OnPropertyChanged("PrescriptionsCollection");
                }
            }
        }

        public Prescription? selectedPrescription;
        public Prescription? SelectedPrescription
        {
            get => selectedPrescription;
            set
            {
                if (selectedPrescription != value)
                {
                    selectedPrescription = value;
                    OnPropertyChanged("SelectedPrescription");
                }
            }
        }


        PrescriptionService prescriptionService = GLOBALS.prescriptionService;
        AuthService authService = GLOBALS.authService;
        PatientController patientController = GLOBALS.patientController;
        DrugController drugController;
        public Prescriptions()
        {
          
            PrescriptionsCollection = new List<Prescription>(prescriptionService.GetAllPrescriptions(patientController.GetById(authService.user.JMBG)));
            
          
          
            this.DataContext = this;
            InitializeComponent();

        }

       


        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var patientPrescriptions = prescriptionService.GetAllPrescriptions( patientController.GetById(authService.user.JMBG) );
            var prescription = patientPrescriptions.Find(r => r.prescriptionId == selectedPrescription.prescriptionId);
            Console.WriteLine(prescription.patient_note);
            if ( prescription.patient_note != "")
            {
                NavigationService.Navigate(new Noted(selectedPrescription.patient_note));

            }
            else
            {
                NavigationService.Navigate(new Note(selectedPrescription.prescriptionId));
            }

            
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

    }
}



