using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class EditAppointment: Page, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private int _duration = 0;
        private string _doctorJMBG = "";
        private DateTime _startTime = DateTime.Now;


        public int duration { get { return _duration; } set { _duration = value; OnPropertyChanged("duration"); } }
        public string doctorJMBG { get { return _doctorJMBG; } set { _doctorJMBG = value; OnPropertyChanged("doctorJMBG"); } }
        public DateTime startTime { get { return _startTime; } set { _startTime = value; OnPropertyChanged("startTime"); } }


        public AppointmentController appointmentController;

        Appointment? a;

        public EditAppointment(int id) {
            DoctorRepository doctorRepository = new DoctorRepository(@"..\..\..\Resource\Data\doctor.json");
            DoctorService doctorService = new DoctorService(doctorRepository);
            this.DataContext = this;
            AppointmentRepository appointmentRepository = new AppointmentRepository(@"..\..\..\Resource\Data\appointment.json");
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorService);
            appointmentController = new AppointmentController(appointmentService, doctorService);

            a = appointmentController.GetAppointmentById(id);


            if (a is not null)
            {

                duration = a.duration;
                doctorJMBG = a.doctorJMBG;
                startTime = a.startTime;
               
            }
            else { NavigationService.GoBack(); }
            InitializeComponent();

        }


        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                if (a is not null)
                {
                    a.duration=duration;
                    a.doctorJMBG= doctorJMBG ;
                    a.startTime= startTime ;

                    appointmentController.MoveAppointmentById(a.id, a.startTime);
                }
                NavigationService.Navigate(new Appointments());


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }



    }
}



