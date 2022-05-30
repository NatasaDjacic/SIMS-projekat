using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for NewMeeting.xaml
    /// </summary>
    public partial class NewMeeting : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string subject;
        public string Subject {
            get => subject;
            set {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }
        private int duration;
        public int Duration {
            get => duration;
            set {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }

        private DateTime dateTo;
        public DateTime DateTo {
            get => dateTo;
            set {
                dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }

        private DateTime dateFrom;
        public DateTime DateFrom {
            get => dateFrom;
            set {
                dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }
        public List<Room> Rooms { get; set; }
        public List<User> Employees { get; set; }
        private ObservableCollection<Meeting> meetingsSuggestion;
        public ObservableCollection<Meeting> MeetingsSuggestion {
            get => meetingsSuggestion;
            set {
                meetingsSuggestion = value;
                OnPropertyChanged("MeetingsSuggestion");
            }
        }
        private Meeting? selectedMeeting;
        public Meeting? SelectedMeeting {
            get => selectedMeeting;
            set {
                selectedMeeting = value;
                OnPropertyChanged("SelectedMeeting");
            }
        }

        private Room? selectedRoom;
        public Room? SelectedRoom {
            get => selectedRoom;
            set {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }
        private List<string> attendeesJMBG;
        EmployeController employeController = GLOBALS.employeController;
        SuggestionController suggestionController = GLOBALS.suggestionController;
        RoomController roomController = GLOBALS.roomController;
        MeetingController meetingController = GLOBALS.meetingController;
        public NewMeeting() {
            this.subject = "";
            this.duration = 60;
            this.dateFrom = DateTime.Now;
            this.dateTo = DateTime.Now.AddDays(7);
            this.Rooms = roomController.GetAll();
            this.Employees = employeController.GetAll();
            this.attendeesJMBG = new List<string>();
            this.meetingsSuggestion = new ObservableCollection<Meeting>();
            this.DataContext = this;
            InitializeComponent();

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var addedUsers = e.AddedItems.Cast<User>().ToList();
            var removedUsers = e.RemovedItems.Cast<User>().ToList();

            addedUsers.ForEach(user => {
                attendeesJMBG.Add(user.JMBG);
            });
            removedUsers.ForEach(user => {
                attendeesJMBG.Remove(user.JMBG);
            });

            Console.WriteLine();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            // Find sugetstions
            if(selectedRoom != null)
                MeetingsSuggestion =  new ObservableCollection<Meeting>(this.suggestionController.GetMeetingSuggestion(attendeesJMBG, selectedRoom.roomId, subject, dateFrom, dateTo, duration));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new Meetings());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            if (selectedMeeting != null) { 
                meetingController.SaveSuggestion(selectedMeeting);
                this.NavigationService.Navigate(new Meetings());
            }
        }
    }
}
