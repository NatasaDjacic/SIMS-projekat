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
        private ObservableCollection<Appointment> appointments;

        public ObservableCollection<Appointment> NewAppointmentCollection
        {
            get => appointments;
            set
            {
                if (appointments != value)
                {
                    appointments = value;
                    OnPropertyChanged("PatientsCollection");
                }
            }
        }
        public AppointmentController appointmentController;
        public NewAppointment()
        {

            AppointmentRepository appointmentRepository = new AppointmentRepository(@"..\..\..\Resource\Data\appointment.json");
            AppointmentService appointmentService = new AppointmentService(appointmentRepository);
            AppointmentController appointmentController = new AppointmentController(appointmentService);
            NewAppointmentCollection = new ObservableCollection<Appointment>(appointmentController.GetAllAppointments());
            this.DataContext = this;
            InitializeComponent();

        }

        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            string? id = ((Button)sender).Tag as string;
            Console.WriteLine(id);
            if (id is null) return;
        }



    }


}
