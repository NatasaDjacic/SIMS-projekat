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
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.ManagerUI.View
{
   
    public partial class RoomsSeparate : Page, INotifyPropertyChanged
    {
        string val = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
        SuggestionController suggestionController = GLOBALS.suggestionController;
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;
        RenovationController renovationController = GLOBALS.renovationController;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    CheckDates();
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
                    CheckDates();
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

        private Room selectedRoom;
        public Room SelectedRoom
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

        public RoomController roomController;
        RoomMergeController roomMergeController = GLOBALS.roomMergeController;

        public RoomsSeparate(string value)
        {
            val = value;
            roomSeparateController.ExecuteRoomSeparating();
            roomMergeController.ExecuteRoomMerging();
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
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
            CheckDates();
            InitializeComponent();
        }

        private void CheckDates()
        {
            if (StartDateTB == null) return;
            if (startDate.CompareTo(DateTime.Today) < 0)
            {


                StartDateTB.Text = "Start date cannot be in past. Choose again!";
                StartDateTB.Foreground = Brushes.Red;
            }
            else
            {
                StartDateTB.Text = " ";
                StartDateTB.Foreground = Brushes.Gray;
            }


            if (EndDateTB == null) return;
            if (endDate.CompareTo(DateTime.Today) < 0 || endDate.CompareTo(startDate) < 0)
            {


                EndDateTB.Text = "End date cannot be in past or before start date. Choose again!";

                EndDateTB.Foreground = Brushes.Red;
            }
            else
            {
                EndDateTB.Text = " ";
                EndDateTB.Foreground = Brushes.Gray;
            }


        }
        private void Button_Click_Check(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SeparateAppointments(SelectedRoom.roomId, StartDate, EndDate, Duration, val));
           /* string val;
            System.Collections.IList list = suggestionController.getRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration);

            foreach (Renovation ren in suggestionController.GetRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration))
            {
                Console.WriteLine(ren.startTime.ToString());
            }
            Console.Write("Enter index: ");
            val = Console.ReadLine();
            int index = Convert.ToInt32(val);
            var first = suggestionController.GetRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration)[index];
            Console.Write("Enter identification of first room: ");
            var firstRoomId = Console.ReadLine();
            Console.Write("Enter name of first room: ");
            var firstRoomName = Console.ReadLine();
            Console.Write("Enter type of first room: ");
            var firstRoomType = Console.ReadLine();
            Console.Write("Enter description of first room: ");
            var firstRoomDescription = Console.ReadLine();
            Console.Write("Enter identification of second room: ");
            var secondRoomId = Console.ReadLine();
            Console.Write("Enter name of second room: ");
            var secondRoomName = Console.ReadLine();
            Console.Write("Enter type of second room: ");
            var secondRoomType = Console.ReadLine();
            Console.Write("Enter description of second room: ");
            var secondRoomDescription = Console.ReadLine();


            roomSeparateController.Save(first.startTime, first.duration, first.roomId, firstRoomId, firstRoomName, firstRoomType, firstRoomDescription, secondRoomId, secondRoomName, secondRoomType, secondRoomDescription);
            renovationController.SaveRenovation(first.startTime, first.duration, first.roomId, "Separation");
           */
        }
    }
}
