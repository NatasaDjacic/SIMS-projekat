
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

        DoctorController doctorController = GLOBALS.doctorController;
        RoomController roomController = GLOBALS.roomController;
        AppointmentController appointmentController = GLOBALS.appointmentController;
        PatientController patientController = GLOBALS.patientController;
        SuggestionController suggestionController = GLOBALS.suggestionController;

        

        public List<Appointment> appointments;
        public List<Appointment> AppointmentsList
        {
            get => appointments;
            set
            {
                appointments = value;
                OnPropertyChanged("AppointmentsList");
            }
        }
        public Appointment? selectedAppointment;
        public Appointment? SelectedAppointment
        {
            get => selectedAppointment;
            set
            {
                if (selectedAppointment != value)
                {
                   
                    selectedAppointment = value;
                    Console.WriteLine(selectedAppointment.doctorJMBG);

                    OnPropertyChanged("SelectedAppointment");

                }
            }
        }

        

        public SuggestedAppointments(string patientJMBG, string doctorJMBG, string roomId, DateTime startTime, DateTime endTime, int duration, string priority, Model.Enums.AppointmentType appointmentType)
        {


         

            this.DataContext = this;


            if(priority=="doctor")
            {
               startTime = DateTime.Now;
                AppointmentsList = suggestionController.GetAppointmentSuggestion(patientJMBG, doctorJMBG, roomId, startTime, startTime.AddDays(7), duration, priority, appointmentType);

            }
            else
            {       
                AppointmentsList = suggestionController.GetAppointmentSuggestion(patientJMBG, doctorJMBG, roomId, startTime, endTime, duration, priority, appointmentType);


            }
            InitializeComponent();

        }

       


        //!!!!!!!!!!!!!!
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            
            
            if (selectedAppointment.doctorJMBG is null) return;

            
            Console.WriteLine(selectedAppointment.doctorJMBG);
            
            try
            {


                appointmentController.CreateAppointmentPatient(selectedAppointment.startTime, selectedAppointment.duration, selectedAppointment.doctorJMBG);


                NavigationService.Navigate(new Appointments());


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
            NavigationService.Navigate(new Home());
        }

       

    }
}
