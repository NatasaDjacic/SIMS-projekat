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

namespace ZdravoKlinika.UI.SecretaryUI.View {

    public partial class RegularAppointment : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        DoctorController doctorController = GLOBALS.doctorController;
        RoomController roomController = GLOBALS.roomController;
        AppointmentController appointmentController = GLOBALS.appointmentController;
        PatientController patientController = GLOBALS.patientController;
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
                var asss=appointmentController.getSuggestions(JMBG, selectedDoctor.JMBG, selectedRoom.roomId, fromDate, toDate, duration, priority, Model.Enums.AppointmentType.regular);
                AppointmentsList = asss;
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
                SelectedAppointment.id = GLOBALS.appointmentService.GenerateNewId();
                GLOBALS.appointmentService.SaveAppointment(SelectedAppointment);
                NavigationService.GoBack();
            }
        }
    }
}
