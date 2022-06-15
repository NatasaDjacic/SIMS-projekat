
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
using ZdravoKlinika.Model.DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ZdravoKlinika.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        private List<AppointmentDTO> appointments;
        public List<AppointmentDTO> AppointmentCollection
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

        private DateTime selectedDate=DateTime.Now;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    Console.WriteLine(selectedDate);
                    ChangedDate();
                    OnPropertyChanged("SelectedDate");
                }
            }
        }

        private AppointmentDTO? selectedAppointments;
        public AppointmentDTO? SelectedAppointment
        {
            get => selectedAppointments;
            set
            {
                if (selectedAppointments != value)
                {
                    selectedAppointments = value;
                    OnPropertyChanged("SelectedAppointment");
                }
            }
        }



        public AppointmentController appointmentController = GLOBALS.appointmentController ;
        public AuthService authService=GLOBALS.authService ;
        public Appointments()
        {

            if (selectedDate == DateTime.Now) 
            { AppointmentCollection = new List<AppointmentDTO>(appointmentController.GetDTOAppointmentsByPatient("1231231231231")); }
            else {

                var appointmentDTOs = new List<AppointmentDTO>(appointmentController.GetDTOAppointmentsByPatient("1231231231231"));
                
                foreach (var a in appointmentDTOs)
                {
                    if (a.startTime.Date == selectedDate)
                    {
                        Console.WriteLine(a);


                        AppointmentCollection.Add(a);
                    }
                }


            }
            

            this.DataContext = this;

            InitializeComponent();

        }

        private void ChangedDate()
        {
            var appointmentDTOs = new List<AppointmentDTO>(appointmentController.GetDTOAppointmentsByPatient("1231231231231"));
            AppointmentCollection = new List<AppointmentDTO>();
            foreach (var a in appointmentDTOs)
            {
                if (a.startTime.Date == selectedDate )
                {
                    Console.WriteLine(a);


                    AppointmentCollection.Add(a);
                }
            }
            

        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int id = selectedAppointments.id;
            
            appointmentController.DeleteAppointmentById(id);

            if (authService.user == null)
            {

                var window = new MainWindow();
                Application.Current.MainWindow.Hide();

                Application.Current.MainWindow = window;
                window.Show();

            }
            else
            {
                this.ChangedDate();
            }
        }


        private void Edit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Edit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            int id = selectedAppointments.id;
           // Console.WriteLine(((Button)sender).Tag);
            NavigationService.Navigate(new EditAppointment(id));
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }




        private List<DateTime> significantDates;


        private void calendarButton_Loaded(object sender, EventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
            button.DataContextChanged += new DependencyPropertyChangedEventHandler(calendarButton_DataContextChanged);
        }


        private List<DateTime> GetDates()
        {
            significantDates = new List<DateTime>();

            var appointments = appointmentController.GetDTOAppointmentsByPatient(authService.user.JMBG).ToList();
            foreach (var a in appointments)
            {
                significantDates.Add(a.startTime);
            }

            return significantDates;

        }

        private void HighlightDay(CalendarDayButton button, DateTime date)
        {
            significantDates=this.GetDates();


            if (significantDates.Contains(date))
                button.Background = Brushes.Red;
            else
                button.Background = BackgroundCal;
        }

        private void calendarButton_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CalendarDayButton button = (CalendarDayButton)sender;
            DateTime date = (DateTime)button.DataContext;
            HighlightDay(button, date);
        }






    }
}
