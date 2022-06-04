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
    public partial class RoomsMerge : Page
    {
        string val = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
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
        SuggestionController suggestionController = GLOBALS.suggestionController;
        RoomMergeController roomMergeController = GLOBALS.roomMergeController;
        RenovationController renovationController = GLOBALS.renovationController;
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;

        public RoomsMerge(string value)
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
            InitializeComponent();
            CheckDates();
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
            /*string val;
            System.Collections.IList list = suggestionController.getTwoRoomsRenovationSuggestion(SelectedRoomFrom.roomId, SelectedRoomTo.roomId, StartDate, EndDate, Duration);
            foreach (Renovation ren in suggestionController.getTwoRoomsRenovationSuggestion(SelectedRoomFrom.roomId, SelectedRoomTo.roomId, StartDate, EndDate, Duration))
            {
                Console.WriteLine(ren.startTime.ToString());
            }*/

            /*Console.Write("Enter index: ");
            val = Console.ReadLine();
            int index = Convert.ToInt32(val);
            var first = suggestionController.GetTwoRoomsRenovationSuggestion(SelectedRoomFrom.roomId, SelectedRoomTo.roomId, StartDate, EndDate, Duration)[index];
            Console.Write("Enter identification of new room: ");
            var newRoomId = Console.ReadLine();
            Console.Write("Enter name of new room: ");
            var newRoomName = Console.ReadLine();
            Console.Write("Enter type of new room: ");
            var newRoomType = Console.ReadLine();
            Console.Write("Enter description of new room: ");
            var newRoomDescription = Console.ReadLine();*/
            NavigationService.Navigate(new MergeAppointments(SelectedRoomFrom.roomId, SelectedRoomTo.roomId, StartDate, EndDate, Duration, val));



            /*roomMergeController.Save(first.startTime, first.duration, selectedRoomFrom.roomId, selectedRoomTo.roomId, newRoomId, newRoomName, newRoomType, newRoomDescription);
            renovationController.SaveRenovation(first.startTime, first.duration, selectedRoomFrom.roomId, "Merging");
            renovationController.SaveRenovation(first.startTime, first.duration, selectedRoomTo.roomId, "Merging");
            */
        }

    }
}
