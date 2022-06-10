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
    public partial class NewAppointment : Page, INotifyPropertyChanged, IDataErrorInfo
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
                    case "FromDate":
                        if(fromDate.CompareTo(DateTime.Now) < 0) result = "Start date can't be before today. Choose again!";
                      break;
                    case "ToDate":
                        if (toDate.CompareTo(DateTime.Now) < 0 || toDate.CompareTo(fromDate) < 0) result = "End date can't be before today or before start date. Choose again!";
                       break;

                    default: break;
                }
                return result;
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
        private DateTime fromDate=DateTime.Now;
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
        private DateTime toDate=DateTime.Now.AddDays(1);
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

        public NewAppointment()
        {
            this.JMBG = JMBG;
          
            this.Duration = 30;
            this.doctors = new ListCollectionView(doctorController.GetAll());
            this.doctors.GroupDescriptions.Add(new PropertyGroupDescription("specialization"));

            this.DataContext = this;
            InitializeComponent();
            CheckDoctorRoom();
          
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var b = sender as RadioButton;
            if (b != null)
                priority = b.Content as string ?? "time";
        }



        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }


        private void Find_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Find_Executed(object sender, ExecutedRoutedEventArgs e)
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

        
        /*


        private void CheckDates()
        {
            if (StartDateTB == null) return;
            if (fromDate.CompareTo(DateTime.Now) < 0)
            {


                StartDateTB.Text = "Start date can't be today or before. Choose again!";

                StartDateTB.Foreground = Brushes.Red;
            }
            else
            {
                StartDateTB.Text = " ";
                StartDateTB.Foreground = Brushes.Gray;
            }


            if (EndDateTB == null) return;
            if (toDate.CompareTo(DateTime.Now) < 0 || toDate.CompareTo(fromDate) < 0)
            {


                EndDateTB.Text = "End date can't be before today or before start date. Choose again!";

                EndDateTB.Foreground = Brushes.Red;
            }
            else
            {
                EndDateTB.Text = " ";
                EndDateTB.Foreground = Brushes.Gray;
            }


        }*/

    }


}
