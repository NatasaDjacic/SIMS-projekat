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
    public partial class ReportNote: Page, INotifyPropertyChanged
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


        public Guid reportId;

        public ReportNote(Guid reportId)
        {
            this.note = note;
            this.reportId = reportId;
            this.DataContext = this;
            InitializeComponent();
        }

        ReportService reportService = GLOBALS.reportService;
        AuthService authService = GLOBALS.authService;
        PatientService patientService = GLOBALS.patientService;
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            reportService.SavingPatientNote(patientService.GetById(authService.user.JMBG), reportId, note);
           
            NavigationService.Navigate(new Reports());
        }



        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Reports());
        }


    }
}

