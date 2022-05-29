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

namespace ZdravoKlinika.UI.ManagerUI.View
{
   
    public partial class SeparateAppointments : Page, INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<DoctorsMarkDTO> rooms;
        public ObservableCollection<DoctorsMarkDTO> RoomsCollection
        {
            get => rooms;
            set
            {
                if (rooms != value)
                {
                    rooms = value;
                    OnPropertyChanged("RoomsCollection");
                }
            }
        }

        public RoomController roomController = GLOBALS.roomController;

        public event PropertyChangedEventHandler? PropertyChanged;
        SuggestionController suggestionController = GLOBALS.suggestionController;
        RoomMergeController roomMergeController = GLOBALS.roomMergeController;
        RenovationController renovationController = GLOBALS.renovationController;
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;
        string val = string.Empty;
        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        private int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private DoctorsMarkDTO selectedRoom;
        public DoctorsMarkDTO SelectedRoom
        {
            get => selectedRoom;
            set
            {
                if (selectedRoom != value)
                {
                    selectedRoom = value;
                    OnPropertyChanged("SelectedRoom");
                }
            }
        }

        private Renovation selectedRenovation;
        public Renovation SelectedRenovation
        {
            get => selectedRenovation;
            set
            {
                if (selectedRenovation != value)
                {
                    selectedRenovation = value;
                    OnPropertyChanged("SelectedRenovation");
                }
            }
        }

        private RoomMerge selectedMerging;
        public RoomMerge SelectedMerging
        {
            get => selectedMerging;
            set
            {
                if (selectedMerging != value)
                {
                    selectedMerging = value;
                    OnPropertyChanged("SelectedMerging");
                }
            }
        }
        private string description;
        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string newRoomId;
        public string NewRoomId
        {
            get => newRoomId;
            set
            {
                if (newRoomId != value)
                {
                    newRoomId = value;
                    OnPropertyChanged("NewRoomId");
                }
            }
        }

        private string newRoomName;
        public string NewRoomName
        {
            get => newRoomName;
            set
            {
                if (newRoomName != value)
                {
                    newRoomName = value;
                    OnPropertyChanged("NewRoomName");
                }
            }
        }

        private string newRoomType;
        public string NewRoomType
        {
            get => newRoomType;
            set
            {
                if (newRoomType != value)
                {
                    newRoomType = value;
                    OnPropertyChanged("NewRoomType");
                }
            }
        }

        private string newRoomDescription;
        public string NewRoomDescription
        {
            get => newRoomDescription;
            set
            {
                if (newRoomDescription != value)
                {
                    newRoomDescription = value;
                    OnPropertyChanged("NewRoomDescription");
                }
            }
        }

        private string newRoomId2;
        public string NewRoomId2
        {
            get => newRoomId2;
            set
            {
                if (newRoomId2 != value)
                {
                    newRoomId2 = value;
                    OnPropertyChanged("NewRoomId2");
                }
            }
        }

        private string newRoomName2;
        public string NewRoomName2
        {
            get => newRoomName2;
            set
            {
                if (newRoomName2 != value)
                {
                    newRoomName2 = value;
                    OnPropertyChanged("NewRoomName2");
                }
            }
        }

        private string newRoomType2;
        public string NewRoomType2
        {
            get => newRoomType2;
            set
            {
                if (newRoomType2 != value)
                {
                    newRoomType2 = value;
                    OnPropertyChanged("NewRoomType2");
                }
            }
        }

        private string newRoomDescription2;
        public string NewRoomDescription2
        {
            get => newRoomDescription2;
            set
            {
                if (newRoomDescription2 != value)
                {
                    newRoomDescription2 = value;
                    OnPropertyChanged("NewRoomDescription2");
                }
            }
        }

        private ObservableCollection<Renovation> separatings;
        public ObservableCollection<Renovation> DateCollection
        {
            get => separatings;
            set
            {
                if (separatings != value)
                {
                    separatings = value;
                    OnPropertyChanged("DateCollection");
                }
            }
        }

        string roomId = "";

        public SeparateAppointments(string RoomId, DateTime StartDate, DateTime EndDate, int Duration)
        {
            roomId = RoomId;
            roomSeparateController.ExecuteRoomSeparating();
            roomMergeController.ExecuteRoomMerging();
            System.Collections.IList list = suggestionController.getRenovationSuggestion(roomId, StartDate, EndDate, Duration);
            DateCollection = new ObservableCollection<Renovation>((List<Renovation>)list);

            RoomsCollection = new ObservableCollection<DoctorsMarkDTO>(roomController.GetAll());
            this.DataContext = this;

            InitializeComponent();
        }
        private void Button_Click_Renovation(object sender, RoutedEventArgs e)
        {
            renovationController.SaveRenovation(selectedRenovation.startTime, selectedRenovation.duration, roomId, "Separating");
            roomSeparateController.Save(selectedRenovation.startTime, selectedRenovation.duration, roomId, NewRoomId, NewRoomName, NewRoomType, NewRoomDescription, NewRoomId2, NewRoomName2, NewRoomType2, NewRoomDescription2);

            NavigationService.Navigate(new Renovations("srb"));
        }
        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRoom("srb"));
        }
    }
}
