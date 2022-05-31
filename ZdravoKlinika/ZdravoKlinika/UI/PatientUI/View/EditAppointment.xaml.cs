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
    public partial class EditAppointment: Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private int _duration = 0;
        private string _doctorJMBG = "";
        private DateTime _startTime = DateTime.Now;


       public DateTime startTime { get { return _startTime; } set { _startTime = value; OnPropertyChanged("startTime"); } }


        public AppointmentController appointmentController = GLOBALS.appointmentController;
        public AuthService authService=GLOBALS.authService;

        Appointment? a;

        public EditAppointment(int id) {
            this.DataContext = this;

            a = appointmentController.GetAppointmentById(id);


            if (a is not null)
            {

                
                startTime = a.startTime;
               
            }
            else { NavigationService.GoBack(); }
            InitializeComponent();

        }


       


        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {


            try
            {
                if (a is not null)
                {
                   
                    a.startTime = startTime;

                    appointmentController.MoveAppointmentById(a.id, a.startTime);
                }


                if (authService.user == null)
                {

                    var window = new MainWindow();
                    Application.Current.MainWindow.Hide();
                    Application.Current.MainWindow = window;
                    window.Show();

                }
                else
                {
                    NavigationService.Navigate(new Appointments());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Appointments());
        }



    }
}



