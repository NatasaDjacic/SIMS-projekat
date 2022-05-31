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
    public partial class Rate : Page, INotifyPropertyChanged
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
                    CheckMarks();
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
                    CheckMarks();
                    OnPropertyChanged("DoctorMark");
                }
            }
        }

        Guid reportId;
        public Rate(Guid reportId)
        {
            this.reportId = reportId;
            this.doctorMark = doctorMark;
            this.hospitalMark = hospitalMark;
            this.DataContext = this;
            InitializeComponent();
            CheckMarks();
        }

        private void Click_Rate(object sender, RoutedEventArgs e)
        {
            markService.Save(markService.GenerateNewId(), hospitalMark, doctorMark, authService.user.JMBG, "1111111111111", reportId);
            
            NavigationService.Navigate(new Rated());
            


        }
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-5]+");
            e.Handled=regex.IsMatch(e.Text);
        }

        private void CheckMarks()
        {
            if (doctorTB == null) return;
            if (5<doctorMark  || doctorMark<1 )
            {
                doctorTB.Text = "Mark has to be between 1 and 5!";

                doctorTB.Foreground = Brushes.Red;
            }
            else
            {
                doctorTB.Text = " ";
                doctorTB.Foreground = Brushes.Gray;
            }


            if (hospitalTB == null) return;
            if (hospitalMark<1 || hospitalMark>5)
            {


                hospitalTB.Text = "Mark has to be between 1 and 5!";

                hospitalTB.Foreground = Brushes.Red;
            }
            else
            {
                hospitalTB.Text = " ";
                hospitalTB.Foreground = Brushes.Gray;
            }


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

