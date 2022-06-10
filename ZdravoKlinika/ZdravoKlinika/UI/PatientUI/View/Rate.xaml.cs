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
using System.Text.RegularExpressions;
using ZdravoKlinika.Model;
using System.ComponentModel;


namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Rate : Page, INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "DoctorMark":
                        if (doctorMark.Trim() == "") result = "Doctor mark should be set.";
                        else if (doctorMark.Length != 1) result = "Doctor mark should have 1 digit.";
                        else if (!Regex.IsMatch(doctorMark, "^[1-5]*$")) result = "Doctor mark should be between 1 and 5.";
                        break;
                    case "HospitalMark":
                        if (hospitalMark.Trim() == "") result = "Hospital mark should be set.";
                        else if (hospitalMark.Length != 1) result = "Hospital mark should have 1 digit.";
                        else if (!Regex.IsMatch(hospitalMark, "^[1-5]*$")) result = "Hospital mark should be between 1 and 5.";
                        break;
                  
                    default: break;
                }
                return result;
            }
        }



        MarkService markService = GLOBALS.markService;
        AuthService authService = GLOBALS.authService;

        private string hospitalMark ="";
       
        public string HospitalMark
        {
            get { return hospitalMark; }
            set { hospitalMark = value; OnPropertyChanged("HospitalMark"); }
        }

        private string doctorMark="";

        public string DoctorMark
        { get { return doctorMark; } 
            set { doctorMark = value; OnPropertyChanged("DoctorMark"); } 
        }


        Guid reportId;
        public Rate(Guid reportId)
        {
            this.reportId = reportId;
            this.DataContext = this;
            InitializeComponent();
            
        }

    
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

       private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            markService.Save(markService.GenerateNewId(), Convert.ToInt32(hospitalMark) , Convert.ToInt32(doctorMark) , authService.user.JMBG, "1111111111111", reportId);

            NavigationService.Navigate(new Rated());
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

