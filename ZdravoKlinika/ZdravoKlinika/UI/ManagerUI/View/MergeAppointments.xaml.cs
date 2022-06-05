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
    public partial class MergeAppointments : Page, INotifyPropertyChanged
    {
        string val = string.Empty;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> RoomsCollection
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
        private ObservableCollection<Renovation> merges;
        public ObservableCollection<Renovation> DateCollection
        {
            get => merges;
            set
            {
                if (merges != value)
                {
                    merges = value;
                    OnPropertyChanged("DateCollection");
                }
            }
        }
        public RoomController roomController = GLOBALS.roomController;

        public event PropertyChangedEventHandler? PropertyChanged;
        SuggestionController suggestionController = GLOBALS.suggestionController;
        RoomMergeController roomMergeController = GLOBALS.roomMergeController;
        RenovationController renovationController = GLOBALS.renovationController;
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;
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

        private Room selectedRoomFrom;
        public Room SelectedRoomFrom
        {
            get => selectedRoomFrom;
            set
            {
                if (selectedRoomFrom != value)
                {
                    selectedRoomFrom = value;
                    OnPropertyChanged("SelectedRoomFrom");
                }
            }
        }

        private Room selectedRoomTo;
        public Room SelectedRoomTo
        {
            get => selectedRoomTo;
            set
            {
                if (selectedRoomTo != value)
                {
                    selectedRoomTo = value;
                    OnPropertyChanged("SelectedRoomTo");
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
        string roomFirstId = "";
        string roomSecondId = "";
        public MergeAppointments(string roomIdFirst, string roomIdSecond, DateTime StartDate, DateTime EndDate, int Duration, string value)
        {
            val = value;
            roomFirstId = roomIdFirst;
            roomSecondId = roomIdSecond;
            roomSeparateController.ExecuteRoomSeparating();
            roomMergeController.ExecuteRoomMerging();
            System.Collections.IList list = suggestionController.GetTwoRoomsRenovationSuggestion(roomIdFirst, roomIdSecond, StartDate, EndDate, Duration);
            DateCollection = new ObservableCollection<Renovation>((List<Renovation>)list);

            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (value)
            {
                case "en":
                    Console.WriteLine("en");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.en.xaml", UriKind.Relative);
                    break;
                case "rus":
                    Console.WriteLine("rus");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.rus.xaml", UriKind.Relative);
                    break;
                case "srb":
                    Console.WriteLine("srb");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dictionary);

            InitializeComponent();
        }
        private void Button_Click_Renovation(object sender, RoutedEventArgs e)
        {
            renovationController.SaveRenovation(selectedRenovation.startTime, selectedRenovation.duration, roomFirstId, "Merging");
            renovationController.SaveRenovation(selectedRenovation.startTime, selectedRenovation.duration, roomSecondId, "Merging");
            roomMergeController.Save(selectedRenovation.startTime, selectedRenovation.duration, roomFirstId, roomSecondId, NewRoomId, NewRoomName, NewRoomType, NewRoomDescription);

            NavigationService.Navigate(new RoomsMerge(val));
        }
    }
}
