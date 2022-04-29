﻿using System;
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
            this.selectedPeriod = "weekly";
            this.selectedDate = DateTime.Today;
            this.jmbg = "";
            this.DataContext = this;
            InitializeComponent();
        }

        private void Show_Click(object sender, RoutedEventArgs e) {
            Console.WriteLine(this.jmbg);
            this.AppointmentsList = appointmentController.GetAllAppointments().FindAll(a =>
               (a.startTime > this.selectedDate && a.startTime < this.selectedDate.AddDays(this.selectedPeriod == "weekly" ? 7 : 30)) &&
               (this.selectedDoctor == null || a.doctorJMBG == this.selectedDoctor.JMBG) &&
               (this.selectedRoom == null || a.roomId == this.selectedRoom.roomId) &&
               (this.jmbg == "" || a.patientJMBG == this.jmbg));
        }
        private void ShowDetails() {
            if (this.selectedAppointment != null) {
                Console.WriteLine(this.selectedAppointment.doctorJMBG);
            } else {
                Console.WriteLine("NULL");
            }
        }


    }
    
}
