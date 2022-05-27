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
using ZdravoKlinika.Model.DTO;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.ManagerUI.View
{
   
    public partial class Renovations : Page, INotifyPropertyChanged
    {
        string val = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
        SuggestionController suggestionController = GLOBALS.suggestionController;
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
        public RoomController roomController = GLOBALS.roomController;
        public Renovations(string value)
        {
            val = value;
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Console.WriteLine(value);
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
            if (startDate.CompareTo(DateTime.Now) < 0)
            {


                StartDateTB.Text = "Start date can't be today or before. Choose again!";

                StartDateTB.Foreground = Brushes.Red;
            }
            else
            {
                StartDateTB.Text = " ";
                StartDateTB.Foreground = Brushes.Gray;
            }


            if (EndDateTB == null) return;
            if (endDate.CompareTo(DateTime.Now) < 0 || endDate.CompareTo(startDate) < 0)
            {


                EndDateTB.Text = "End date can't be before today or before start date. Choose again!";

                EndDateTB.Foreground = Brushes.Red;
            }
            else
            {
                EndDateTB.Text = " ";
                EndDateTB.Foreground = Brushes.Gray;
            }


        }
        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            string? roomId = ((Button)sender).Tag as string;
            Console.WriteLine(roomId);
            if (roomId is null) return;
            roomController.Delete(roomId);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            string? roomId = ((Button)sender).Tag as string;
            Console.WriteLine(roomId);
            if (roomId is null) return;
            NavigationService.Navigate(new EditRoom(roomId, val));
        }

        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddRoom(val));
        }

     

        private void Button_Click_Check(object sender, RoutedEventArgs e)
        {
           // string val;
            //System.Collections.IList list = suggestionController.getRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration);
            NavigationService.Navigate(new RenovationsAppointments(SelectedRoom.roomId, StartDate, EndDate, Duration));

            /*foreach (Renovation ren in suggestionController.getRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration))
            {
                Console.WriteLine(ren.startTime.ToString());
                

            }
            Console.Write("Enter index: ");
            val = Console.ReadLine();
            Console.WriteLine("Enter description: ");
            string desc = Console.ReadLine();
            int index = Convert.ToInt32(val);
            var first = suggestionController.getRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration)[index];
            renovationController.SaveRenovation(first.startTime, first.duration, first.roomId, desc);

            */

        }

    }
}