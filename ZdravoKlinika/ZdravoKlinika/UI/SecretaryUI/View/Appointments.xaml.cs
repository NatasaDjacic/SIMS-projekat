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
    /// <summary>
    /// Interaction logic for Appointments.xaml
    /// </summary>

    public partial class Appointments : Page, INotifyPropertyChanged {
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

        public List<string> period { get; set;}
        private string selectedPeriod;
        public string SelectedPeriod { 
            get=>selectedPeriod;
            set{
                if (selectedPeriod != value) {
                    selectedPeriod = value;
                    OnPropertyChanged("SelectedPeriod");
                }
            }
        }
        public List<Doctor> doctors { get; set; }
        public static Dictionary<string, Doctor> doctorsLookup { get; set; }
        public static Dictionary<string, Room> roomsLookup { get; set; }
        private Doctor? selectedDoctor;
        public Doctor? SelectedDoctor {
            get => selectedDoctor;
            set {
                if (selectedDoctor != value) {
                    selectedDoctor = value;
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

        private DateTime selectedDate;
        public DateTime SelectedDate {
            get => selectedDate;
            set {
                if (selectedDate != value) {
                    selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }
        private string jmbg;
        public string JMBG {
            get => jmbg;
            set {
                if (jmbg != value) {
                    jmbg = value;
                    OnPropertyChanged("JMBG");
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
                    ShowDetails();
                    OnPropertyChanged("SelectedAppointment");
                    
                }
            }
        }

        public Appointments() {
            this.period = new List<string> { "weekly", "monthly" };
            this.appointments = new List<Appointment>();
            this.doctors = doctorController.GetAll();
            this.rooms = roomController.GetAll();
            doctorsLookup = this.doctors.ToDictionary(d => d.JMBG);
            roomsLookup = this.rooms.ToDictionary(r => r.roomId);
            this.selectedPeriod = "weekly";
            this.selectedDate = DateTime.Today;
            this.jmbg = "";
            this.DataContext = this;
            InitializeComponent();
            RemoveBtn.Visibility = Visibility.Hidden;
            MoveBtn.Visibility = Visibility.Hidden;
            DetailSP.Visibility = Visibility.Hidden;
        }

        private void Show_Click(object sender, RoutedEventArgs e) {
            this.AppointmentsList = appointmentController.GetAllAppointments().FindAll(a =>
               (a.startTime > this.selectedDate && a.startTime < this.selectedDate.AddDays(this.selectedPeriod == "weekly" ? 7 : 30)) &&
               (this.selectedDoctor == null || a.doctorJMBG == this.selectedDoctor.JMBG) &&
               (this.selectedRoom == null || a.roomId == this.selectedRoom.roomId) &&
               (this.jmbg == "" || a.patientJMBG == this.jmbg));
        }
        private void ShowDetails() {
            if (this.selectedAppointment != null) {
                var doc = this.doctors.Find(d => d.JMBG == this.selectedAppointment.doctorJMBG);
                var pat = patientController.GetById(this.selectedAppointment.patientJMBG);
                var room = this.rooms.Find(r => r.roomId == this.selectedAppointment.roomId);
                DoctorTB.Text = doc != null ? (doc.firstName+" "+doc.lastName+" - "+doc.specialization):this.selectedAppointment.doctorJMBG;
                PatienteTB.Text = pat != null ? (pat.firstName + " "+pat.lastName): this.selectedAppointment.patientJMBG;
                RoomTB.Text = room!= null ? (room.name): this.selectedAppointment.roomId;
                StartDateTB.Text = this.selectedAppointment.startTime.ToString();
                RemoveBtn.Visibility = Visibility.Visible;
                MoveBtn.Visibility = Visibility.Visible;
                DetailSP.Visibility = Visibility.Visible;
            } else {
                DoctorTB.Text = "";
                PatienteTB.Text = "";
                RoomTB.Text = "";
                StartDateTB.Text = "";
                RemoveBtn.Visibility = Visibility.Hidden;
                MoveBtn.Visibility = Visibility.Hidden;
                DetailSP.Visibility = Visibility.Hidden;


            }
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e) {
            if(this.SelectedAppointment != null) {
                this.appointmentController.RemoveAppointmentSecretary(this.SelectedAppointment);
                this.Show_Click(sender, e);
                this.SelectedAppointment = null;
            }
        }

        private void MoveBtn_Click(object sender, RoutedEventArgs e) {
            if (this.SelectedAppointment != null) {
                NavigationService.Navigate(new MoveAppointment(this.SelectedAppointment));
            }
        }

    }
    
}
