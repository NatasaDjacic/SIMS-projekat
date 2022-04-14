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
using System.Windows.Shapes;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.ManagerUI
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
    public partial class RoomWindow : Window, INotifyPropertyChanged
    {
        public RoomWindow()
        {
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
           
            InitializeComponent();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public RoomController roomController;
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
        }
    }
}
