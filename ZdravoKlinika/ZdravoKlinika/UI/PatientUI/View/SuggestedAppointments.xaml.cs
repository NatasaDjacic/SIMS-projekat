
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

namespace ZdravoKlinika.UI.PatientUI.View
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class SuggestedAppointments : Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private ObservableCollection<Appointment> appointments;
        public ObservableCollection<Appointment> AppointmentCollection
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

        public int duration = 30;
        public DateTime startTime=DateTime.Now;
        public string doctorJMBG = "3213213213213";
       

        public SuggestionController suggestionController = GLOBALS.suggestionController;
        public SuggestedAppointments(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration, string priority, Model.Enums.AppointmentType appointmentType)
        {

            this.DataContext = this;


            AppointmentCollection = new ObservableCollection<Appointment>(suggestionController.getAppointmentSuggestion(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration, priority, appointmentType));
            InitializeComponent();

        }

        AppointmentController appointmentController = GLOBALS.appointmentController;

       
        //URADITI CUVANJE !!!!!!!!!!!!!!!!
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            
            Console.WriteLine(doctorJMBG);
            if (doctorJMBG is null) return;

            if (doctorJMBG != "") return;

            try
            {
                appointmentController.CreateAppointmentPatient( startTime,  duration,  doctorJMBG);
           
            
               

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            NavigationService.Navigate(new Appointments());
          

        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());

        }


    }
}
