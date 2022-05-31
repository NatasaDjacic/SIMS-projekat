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
using System.Text.RegularExpressions;

namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Marks : Page, INotifyPropertyChanged
    {

        
            public event PropertyChangedEventHandler? PropertyChanged;
            protected virtual void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }

            MarkService markService = GLOBALS.markService;
            AuthService authService = GLOBALS.authService;

            private int hospitalMark;
            public int HospitalMark
            {
                get => hospitalMark;
                set
                {
                    if (hospitalMark != value)
                    {
                        hospitalMark = value;
                    
                        OnPropertyChanged("HospitalMark");
                    }
                }
            }

            private int doctorMark;
            public int DoctorMark
            {
                get => doctorMark;
                set
                {
                    if (doctorMark != value)
                    {
                        doctorMark = value;
                       
                        OnPropertyChanged("DoctorMark");
                    }
                }
            }


            Guid reportId;
            public Marks(Guid reportId)
            {
                this.reportId = reportId;
                var mark=markService.GetByReportAndPatient(reportId, authService.user.JMBG);

                this.hospitalMark = mark.hospitalMark;
                this.doctorMark = mark.doctorMark;
                
                this.DataContext = this;
                InitializeComponent();
              
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
