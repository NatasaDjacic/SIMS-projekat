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
    
    public partial class RenovationsAppointments : Page, INotifyPropertyChanged
    {
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
        private ObservableCollection<Renovation> renovations;
        public ObservableCollection<Renovation> DateCollection
        {
            get => renovations;
            set
            {
                if (renovations != value)
                {
                    renovations = value;
                    OnPropertyChanged("DateCollection");
                }
            }
        }
        public RoomController roomController = GLOBALS.roomController;

        public event PropertyChangedEventHandler? PropertyChanged;
        SuggestionController suggestionController = GLOBALS.suggestionController;
        RenovationController renovationController = GLOBALS.renovationController;
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

        public RenovationsAppointments(String roomId, DateTime StartDate, DateTime EndDate, int Duration)
        {
            System.Collections.IList list = suggestionController.getRenovationSuggestion(roomId, StartDate, EndDate, Duration);
            DateCollection = new ObservableCollection<Renovation>((List<Renovation>)list);

           
         
            //var first = suggestionController.getRenovationSuggestion(roomId, StartDate, EndDate, Duration)[index];
            //renovationController.SaveRenovation(first.startTime, first.duration, first.roomId, desc);
          
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
            InitializeComponent();
        }
        
        private void Button_Click_Renovation(object sender, RoutedEventArgs e)
        {
           renovationController.SaveRenovation(selectedRenovation.startTime, selectedRenovation.duration, selectedRenovation.roomId, Description);
            NavigationService.Navigate(new Renovation());
        }
        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRoom("srb"));
        }
    }
}
