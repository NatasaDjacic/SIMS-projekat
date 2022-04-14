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


        private int _duration = 0;
        private string _doctorJMBG = "";
        private DateTime _startTime = DateTime.Now;
        

        public int duration { get { return _duration; } set { _duration = value; OnPropertyChanged("duration"); } }
        public string doctorJMBG { get { return _doctorJMBG; } set { _doctorJMBG = value; OnPropertyChanged("doctorJMBG"); } }
        public DateTime startTime { get { return _startTime; } set { _startTime = value; OnPropertyChanged("startTime"); } }
      

        public AppointmentController appointmentController;
        public NewAppointment()
        {
            this.DataContext = this;
            AppointmentRepository appointmentRepository = new AppointmentRepository(@"..\..\..\Resource\Data\appointment.json");
            AppointmentService appointmentService = new AppointmentService(appointmentRepository);
            DoctorRepository doctorRepository = new DoctorRepository(@"..\..\..\Resource\Data\doctor.json");
            DoctorService doctorService = new DoctorService(doctorRepository);
            appointmentController = new AppointmentController(appointmentService, doctorService);
            InitializeComponent();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                
                appointmentController.CreateAppointmentPatient(startTime, duration, doctorJMBG);
                NavigationService.Navigate(new Appointments());


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }







    }


}
