using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.ManagerUI.View {
   
    public partial class EditRoom : Page, INotifyPropertyChanged, IDataErrorInfo
    {
        string val = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _roomId = "";
        private string _name = "";
        private string _description = "";
        private string _type = "";

        public string RoomId { get { return _roomId; } set { _roomId = value; OnPropertyChanged("RoomId"); } }
        public string _Name { get { return _name; } set { _name = value; OnPropertyChanged("_Name"); } }
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged("Description"); } }
        public string Type { get { return _type; } set { _type = value; OnPropertyChanged("Type"); } }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "RoomId":
                        if (_roomId.Length == 0) result = "Please enter Room ID.";
                       
                        break;
                    case "_Name":
                        if (_name.Length == 0) result = "Please enter Room name.";
                       
                        break;
                    default: break;
                }
                return result;
            }
        }

        public RoomController roomController;
        Room? r;
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;
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

        public EditRoom(string roomId, string value) {
            val = value;
            roomSeparateController.ExecuteRoomSeparating();
            this.DataContext = this;
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            r = roomController.GetById(roomId);
            if (r is not null) {
                RoomId = r.roomId;
                _Name = r.name;
                Description = r.description;
                Type = r.type;
            } else { NavigationService.GoBack(); }

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
            Idtb.Focus();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e) {
            try {
                if (r is not null) {
                    r.roomId = RoomId;
                    r.name = _Name;
                    r.description = Description;
                    r.type = Type;
                    roomController.Update(r);
                }
                NavigationService.Navigate(new Rooms("srb"));
                

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
