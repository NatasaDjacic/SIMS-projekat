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
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private DoctorsMarkDTO selectedRoomFrom;
        public DoctorsMarkDTO SelectedRoomFrom
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

        private DoctorsMarkDTO selectedRoomTo;
        public DoctorsMarkDTO SelectedRoomTo
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
        public RoomController roomController;
        SuggestionController suggestionController = GLOBALS.suggestionController;
        RoomMergeController roomMergeController = GLOBALS.roomMergeController;
        RenovationController renovationController = GLOBALS.renovationController;
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;

        public RoomsMerge()
        {
            roomSeparateController.ExecuteRoomSeparating();
            roomMergeController.ExecuteRoomMerging();
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            RoomsCollection = new ObservableCollection<DoctorsMarkDTO>(roomController.GetAll());
            this.DataContext = this;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            InitializeComponent();
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
            var first = suggestionController.getTwoRoomsRenovationSuggestion(SelectedRoomFrom.roomId, SelectedRoomTo.roomId, StartDate, EndDate, Duration)[index];
            Console.Write("Enter identification of new room: ");
            var newRoomId = Console.ReadLine();
            Console.Write("Enter name of new room: ");
            var newRoomName = Console.ReadLine();
            Console.Write("Enter type of new room: ");
            var newRoomType = Console.ReadLine();
            Console.Write("Enter description of new room: ");
            var newRoomDescription = Console.ReadLine();*/
            NavigationService.Navigate(new MergeAppointments(SelectedRoomFrom.roomId, SelectedRoomTo.roomId, StartDate, EndDate, Duration));



            /*roomMergeController.Save(first.startTime, first.duration, selectedRoomFrom.roomId, selectedRoomTo.roomId, newRoomId, newRoomName, newRoomType, newRoomDescription);
            renovationController.SaveRenovation(first.startTime, first.duration, selectedRoomFrom.roomId, "Merging");
            renovationController.SaveRenovation(first.startTime, first.duration, selectedRoomTo.roomId, "Merging");
            */
        }

    }
}
