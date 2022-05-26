using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.ManagerUI.View {
   
    public partial class Rooms : Page, INotifyPropertyChanged {
        string val = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
     
        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> RoomsCollection {
            get => rooms;
            set {
                if (rooms != value) {
                    rooms = value;
                    OnPropertyChanged("RoomsCollection");
                }
            }
        }
        public RoomController roomController;
        RoomMergeController roomMergeController = GLOBALS.roomMergeController;
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;

        public Rooms(string value) {
            roomSeparateController.ExecuteRoomSeparating();
            roomMergeController.ExecuteRoomMerging();
            val = value;
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
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

        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e) {
            string? roomId = ((Button)sender).Tag as string;
            Console.WriteLine(roomId);
            if (roomId is null) return;
            roomController.Delete(roomId);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e) {
            string? roomId = ((Button)sender).Tag as string;
            Console.WriteLine(roomId);
            if (roomId is null) return;
            NavigationService.Navigate(new EditRoom(roomId, val));
        }

        private void Button_Click_New(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new AddRoom(val));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
      
    }
}
