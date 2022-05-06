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
    public partial class NewAppointment : Page, INotifyPropertyChanged
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
        

        public ListCollectionView doctors { get; set; }
        private Doctor? selectedDoctor;
        public Doctor? SelectedDoctor
        {
            get => selectedDoctor;
            set
            {
                if (selectedDoctor != value)
                {
                    selectedDoctor = value;
                    CheckDoctorRoom();
                    OnPropertyChanged("SelectedDoctor");
                }
            }
        }

        private string roomId;
        private void CheckDoctorRoom()
        {
            if (selectedDoctor != null && selectedDoctor.roomId != null)
            {
                roomId =  selectedDoctor.roomId;
                
            }
            
        }

        public string priority = "time";
        private DateTime fromDate;
        public DateTime FromDate
        {
            get => fromDate;
            set
            {
                if (fromDate != value)
                {
                    fromDate = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }
        private DateTime toDate;
        public DateTime ToDate
        {
            get => toDate;
            set
            {
                if (toDate != value)
                {
                    toDate = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }
        private int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private string JMBG="1231231231231";
        
    


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var b = sender as RadioButton;
            if (b != null)
                priority = b.Content as string ?? "time";
        }


        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Find(object sender, RoutedEventArgs e)
        {
            try
            {

                NavigationService.Navigate(new SuggestedAppointments(JMBG, selectedDoctor.JMBG, roomId, fromDate, toDate, duration, priority, Model.Enums.AppointmentType.regular));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public NewAppointment()
        {
            //this.appointments = new List<Appointment>();
            this.JMBG = JMBG;
            this.FromDate = DateTime.Today;
            this.ToDate = DateTime.Today.AddDays(7);
            this.Duration = 30;
            this.doctors = new ListCollectionView(doctorController.GetAll());
            this.doctors.GroupDescriptions.Add(new PropertyGroupDescription("specialization"));
            //this.rooms = roomController.GetAll();
            this.DataContext = this;
            InitializeComponent();
            CheckDoctorRoom();
        }




    }


}
