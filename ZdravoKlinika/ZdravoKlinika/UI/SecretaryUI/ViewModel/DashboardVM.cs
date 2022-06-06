using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ZdravoKlinika.UI.SecretaryUI.ViewModel {
    public class DashboardVM: BaseVM {

        public string HiText { get; set; }
        public ActivityHistoryService ActivityHistoryInstance { get; set; }
        NavigationService navigationService { get; set; }

        public RelayCommand NavigateNewMeeting { get; set; }
        public RelayCommand NavigatePatients { get; set; }
        public RelayCommand NavigateNewPatients { get; set; }
        public RelayCommand NavigateAppointments { get; set; }
        public RelayCommand NavigateRegularAppointment { get; set; }
        public RelayCommand NavigateMitting { get; set; }
        public RelayCommand NavigateSickDays { get; set; }
        public RelayCommand NavigateRoomReport { get; set; }
        public RelayCommand NavigateOrderDynamicEquipment { get; set; }
        public RelayCommand NavigateEmergencyAppointment { get; set; }

        public DashboardVM(NavigationService navigationService) : base() {
            this.HiText = String.Format("Hi, {0} {1}!", GLOBALS.authService.user.lastName, GLOBALS.authService.user.firstName);
            this.ActivityHistoryInstance = ActivityHistoryService.Instance;
            this.NavigateAppointments = new RelayCommand(Execute_Appointments);
            this.NavigateEmergencyAppointment = new RelayCommand(Execute_EmergencyAppointment);
            this.NavigateMitting = new RelayCommand(Execute_Mitting);
            this.NavigateNewMeeting = new RelayCommand(Execute_NewMitting);
            this.NavigateNewPatients = new RelayCommand(Execute_NewPatients);
            this.NavigateOrderDynamicEquipment = new RelayCommand(Execute_OrderDynamicEquipment);
            this.NavigatePatients = new RelayCommand(Execute_Patients);
            this.NavigateRegularAppointment = new RelayCommand(Execute_RegularAppointment);
            this.NavigateRoomReport = new RelayCommand(Execute_RoomReport);
            this.NavigateSickDays = new RelayCommand(Execute_SickDays);
            
            this.navigationService = navigationService;
        }

        private void Execute_Patients(object? obj) {
            navigationService.Navigate(new View.Patients());
        }
        private void Execute_NewPatients(object? obj) {
            navigationService.Navigate(new View.AddPatient());
        }

        private void Execute_Appointments(object? obj) {
            navigationService.Navigate(new View.Appointments());
        }

        private void Execute_RegularAppointment(object? obj) {
            navigationService.Navigate(new View.RegularAppointment());
        }

        private void Execute_Mitting(object? obj) {
            navigationService.Navigate(new View.Meetings());
        }
        private void Execute_NewMitting(object? obj) {
            navigationService.Navigate(new View.NewMeeting());
        }
        private void Execute_SickDays(object? obj) {
            navigationService.Navigate(new View.SickDays());

        }
        private void Execute_RoomReport(object? obj) {
            navigationService.Navigate(new View.RoomOccupancy());
        }

        private void Execute_OrderDynamicEquipment(object? obj) {
            navigationService.Navigate(new View.OrderDynamicEquipment());

        }
        private void Execute_EmergencyAppointment(object? obj) {
            navigationService.Navigate(new View.EmergencyAppointment());
        }
    }
}
