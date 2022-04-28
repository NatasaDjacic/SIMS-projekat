﻿
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
        public AppointmentController appointmentController = GLOBALS.appointmentController ;
        public Appointments()
        {
            AppointmentCollection = new ObservableCollection<Appointment>(appointmentController.GetAllAppointments());
            this.DataContext = this;
            InitializeComponent();

        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e) {
            int id = Convert.ToInt32(((Button)sender).Tag);
            Console.WriteLine(id);
            appointmentController.DeleteAppointmentById(id);
            AppointmentCollection = new ObservableCollection<Appointment>(appointmentController.GetAllAppointments());
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).Tag);
            Console.WriteLine(((Button)sender).Tag);
            NavigationService.Navigate(new EditAppointment(id));

        }

        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new NewAppointment());

        }


    }
}
