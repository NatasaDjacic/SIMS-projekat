using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Controller;
using ZdravoKlinika.UI.SecretaryUI;
using System.Windows.Navigation;

namespace ZdravoKlinika.UI.SecretaryUI.ViewModel {
    public class MeetingsVM {

        MeetingController meetingController = GLOBALS.meetingController;
        EmployeController employeController = GLOBALS.employeController;
        RoomController roomController = GLOBALS.roomController;

        public RelayCommand NavigateNewMeeting { get; set; }

        public NavigationService navigationService { get; set; }
        public List<Meeting> Meetings { get; set; }
        public static Dictionary<string, User> Employees { get; set; }
        public static Dictionary<string, Room> Rooms { get; set; }


        public MeetingsVM(NavigationService navigationService) {
            this.navigationService = navigationService;
            this.NavigateNewMeeting = new RelayCommand(Execute_NavigateNewMeeting);
            this.Meetings = meetingController.GetAll();
            Employees = employeController.GetAll().ToDictionary(employe => employe.JMBG);
            Rooms = roomController.GetAll().ToDictionary(room => room.roomId);
        }

        private void Execute_NavigateNewMeeting(object? obj) {
            this.navigationService.Navigate(new View.NewMeeting());
        }
    }
}
