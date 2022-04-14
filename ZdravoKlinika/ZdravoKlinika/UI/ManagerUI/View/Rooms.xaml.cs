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
        public Rooms() {
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
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
            NavigationService.Navigate(new EditRoom(roomId));
        }

        private void Button_Click_New(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new AddRoom());
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
