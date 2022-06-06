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
    public partial class Note : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string note;
        public string Notes
        {
            get => note;
            set
            {
                if (note != value)
                {
                    note = value;
                    
                    OnPropertyChanged("Notes");
                }
            }
        }
        public Guid prescriptionId;

        public Note(Guid prescriptionId )
        {
            this.note = note;
            this.prescriptionId = prescriptionId;
            this.DataContext = this;
            InitializeComponent();
        }

        PrescriptionService prescriptionService = GLOBALS.prescriptionService;
        AuthService authService = GLOBALS.authService;
        PatientService patientService = GLOBALS.patientService;
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            prescriptionService.SavingPatientNote(patientService.GetById(authService.user.JMBG), prescriptionId, note);
           // Console.WriteLine(note);
            NavigationService.Navigate(new Prescriptions());
        }



        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Prescriptions());
        }




    }

}
