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
using System.Collections.ObjectModel;
using System.ComponentModel;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;


namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Reports: Page, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        
        ReportController reportController = GLOBALS.reportController;
        PatientController patientController = GLOBALS.patientController;
        AuthService authService = GLOBALS.authService;
        MarkService markService = GLOBALS.markService;

        


        public List<Report> reports;
        public List<Report> ReportsList
        {
            get => reports;
            set
            {
                reports = value;
               
                OnPropertyChanged("ReportsList");
            }
        }

        public Report? selectedreport;
        public Report? SelectedReport
        {
            get => selectedreport;
            set
            {
                if (selectedreport != value)
                {

                    selectedreport = value;
                
                    OnPropertyChanged("SelectedReport");

                }
            }
        }

       

        public Reports()
        {
            
            ReportsList = reportController.GetAll(patientController.GetById(authService.user.JMBG));
            


            this.DataContext = this;
            
            InitializeComponent();
           
            
        }



      

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (markService.GetByReportAndPatient(selectedreport.reportId, authService.user.JMBG) == null)
            {
                NavigationService.Navigate(new Rate(selectedreport.reportId));
            }
            else
            {

                Console.WriteLine("Already rated");
                NavigationService.Navigate(new Marks(selectedreport.reportId));
            }
        }


        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(selectedreport.patient_note!= "") { NavigationService.Navigate(new Noted(selectedreport.patient_note)); }
            else { NavigationService.Navigate(new ReportNote(selectedreport.reportId));}

            
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


