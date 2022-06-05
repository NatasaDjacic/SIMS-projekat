using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string HiText { get; set; }
        public ActivityHistoryService ActivityHistoryInstance {get; set;}
        public Dashboard() {
            this.HiText = String.Format("Hi, {0} {1}!", GLOBALS.authService.user.lastName, GLOBALS.authService.user.firstName);
            this.ActivityHistoryInstance = ActivityHistoryService.Instance;
            this.DataContext = this;
            InitializeComponent();
            
        }

        private void Patients_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.Patients());
        }
        private void New_Patients_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.AddPatient());
        }

        private void Appointments_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.Appointments());
        }

        private void RegularAppointment_Click(object sender, RoutedEventArgs e) {

            NavigationService.Navigate(new View.RegularAppointment());
        }

        private void Mitting_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.Meetings());

        }
        private void NewMitting_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.NewMeeting());

        }
        private void SickDays_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.SickDays());

        }
        private void RoomReport_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.RoomOccupancy());

        }

        private void OrderDynamicEquipment_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.OrderDynamicEquipment());

        }
        private void EmergencyAppointment_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.EmergencyAppointment());

        }
    }
}
