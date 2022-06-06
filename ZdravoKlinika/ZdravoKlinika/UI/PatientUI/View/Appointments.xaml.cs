
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
using ZdravoKlinika.Model.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ZdravoKlinika.UI;

namespace ZdravoKlinika.UI.PatientUI.View
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Appointments : Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private List<AppointmentDTO> appointments;
        public List<AppointmentDTO> AppointmentCollection
        {
            get => appointments;
            set
            {
                if (appointments != value)
                {
                    appointments = value;
                    OnPropertyChanged("AppointmentCollection");
                }
            }
        }
        private AppointmentDTO? selectedAppointments;
        public AppointmentDTO? SelectedAppointment
        {
            get => selectedAppointments;
            set
            {
                if (selectedAppointments != value)
                {
                    selectedAppointments = value;
                    OnPropertyChanged("SelectedAppointment");
                }
            }
        }



        public AppointmentController appointmentController = GLOBALS.appointmentController ;
        public AuthService authService=GLOBALS.authService ;
        public Appointments()
        {
            AppointmentCollection = new List<AppointmentDTO>(appointmentController.GetAppointmentsByPatient(authService.user.JMBG));
            this.DataContext = this;
            InitializeComponent();

        }


        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int id = selectedAppointments.id;
            //Console.WriteLine(id);
            appointmentController.DeleteAppointmentById(id);

            if (authService.user == null)
            {

                var window = new MainWindow();
                Application.Current.MainWindow.Hide();

                Application.Current.MainWindow = window;
                window.Show();

            }
            else
            {
                AppointmentCollection = new List<AppointmentDTO>(appointmentController.GetAppointmentsByPatient(authService.user.JMBG));
            }
        }


        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int id = selectedAppointments.id;
           // Console.WriteLine(((Button)sender).Tag);
            NavigationService.Navigate(new EditAppointment(id));
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
