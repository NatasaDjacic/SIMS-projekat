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

namespace ZdravoKlinika.UI.SecretaryUI.View {

    public partial class RegularAppointment : Page, INotifyPropertyChanged, IDataErrorInfo {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string Error {
            get {
                return null;
            }
        }
        public string this[string name] {
            get {
                string result = null;
                switch (name) {
                    case "SelectedAppointment":
                        if (SelectedAppointment == null) result = "You didn't select appointment.";
                        break;
                    case "FromDate":
                        if (FromDate < DateTime.Today) result = "From date must be greather than today.";
                        break;
                    case "ToDate":
                        if (ToDate < FromDate) result = "To date can't be greater then from date.";
                        break;
                    case "JMBG":
                        if (JMBG.Trim() == "") result = "JMBG should be set.";
                        else if (!Regex.IsMatch(JMBG, "^[0-9]*$")) result = "JMBG should countain only numbers.";
                        else if (JMBG.Length != 13) result = "JMBG should have 13 digits.";
                        else if(!patientFound) result = "Patient not found";
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

        private bool patientFound = false;
        private string jmbg;
        public string JMBG {
            get => jmbg;
            set {
                if (jmbg != value) {
                    jmbg = value;
                    CheckPatientJMBG();
                    OnPropertyChanged("JMBG");
                }
            }
        }
        public ListCollectionView doctors { get; set; }
        private Doctor? selectedDoctor;
        public Doctor? SelectedDoctor {
            get => selectedDoctor;
            set {
                if (selectedDoctor != value) {
                    selectedDoctor = value;
                    CheckDoctorRoom();
                    OnPropertyChanged("SelectedDoctor");
                }
            }
        }

        public List<Room> rooms { get; set; }
        private Room? selectedRoom;
        public Room? SelectedRoom {
            get => selectedRoom;
            set {
                if (selectedRoom != value) {
                    selectedRoom = value;
                    OnPropertyChanged("SelectedRoom");
                }
            }
        }
        public string priority = "time";
        private DateTime fromDate;
        public DateTime FromDate {
            get => fromDate;
            set {
                if (fromDate != value) {
                    fromDate = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }
        private DateTime toDate;
        public DateTime ToDate {
            get => toDate;
            set {
                if (toDate != value) {
                    toDate = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }
        private int duration;
        public int Duration {
            get => duration;
            set {
                if (duration != value) {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }
        public List<Appointment> appointments;
        public List<Appointment> AppointmentsList {
            get => appointments;
            set {
                appointments = value;
                OnPropertyChanged("AppointmentsList");
            }
        }
        public Appointment? selectedAppointment;
        public Appointment? SelectedAppointment {
            get => selectedAppointment;
            set {
                if (selectedAppointment != value) {
                    selectedAppointment = value;
                    OnPropertyChanged("SelectedAppointment");

                }
            }
        }
        public RegularAppointment() {
            this.appointments = new List<Appointment>();
            this.jmbg = "";
            this.FromDate = DateTime.Today;
            this.ToDate = DateTime.Today.AddDays(7);
            this.Duration = 30;
            this.doctors = new ListCollectionView(doctorController.GetAll());
            this.doctors.GroupDescriptions.Add(new PropertyGroupDescription("specialization"));
            this.rooms = roomController.GetAll();
            this.DataContext = this;
            InitializeComponent();
            CheckDoctorRoom();
            
        }

        private void CheckPatientJMBG() {
            if(jmbg.Length >= 13) {
                var pat = patientController.GetById(JMBG);
                PatientTB.Text = pat != null ? ("Found: "+pat.firstName + " " + pat.lastName) : "Not Found";
                if (pat != null) {
                    patientFound = true;
                    PatientTB.Foreground = Brushes.Green;
                } else {
                    patientFound = false;
                    PatientTB.Foreground = Brushes.Red;
                }
            } else {
                patientFound = false;
                PatientTB.Text = "-------------------------------";
                PatientTB.Foreground = Brushes.Gray;
            }
        }
        private void CheckDoctorRoom() {
            if (RoomCB == null) return;
            if(selectedDoctor != null && selectedDoctor.roomId != null ) {
                SelectedRoom = rooms.Find(r => r.roomId == selectedDoctor.roomId);
                RoomCB.IsEnabled = false;
            } else {
                RoomCB.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            if(selectedDoctor != null && selectedRoom != null && patientFound) {
                AppointmentsList = suggestionController.GetAppointmentSuggestion(JMBG, selectedDoctor.JMBG, selectedRoom.roomId, fromDate, toDate, duration, priority, Model.Enums.AppointmentType.regular);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e) {
            var b = sender as RadioButton;
            if(b != null) {
                priority = b.Content as string ?? "time";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            if (SelectedAppointment != null) {
                // Todo create controler method
                appointmentController.CreateAppointmentFromSuggestion(SelectedAppointment);
                ActivityHistoryService.Instance.NewActivity(ActivityType.APPOINTMENT, "New Appointment", string.Format("For {0} at {1} \nin room {2}", SelectedAppointment.patientJMBG, SelectedAppointment.startTime, SelectedAppointment.roomId));

                NavigationService.GoBack();
            }
        }
    }
}
