using System;
using System.Collections.Generic;
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
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;
using System.ComponentModel;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.UI.ManagerUI.View;
using System.Collections;
using System.Collections.ObjectModel;

namespace ZdravoKlinika.UI.ManagerUI.View {
   
    public partial class AddRoom : Page, INotifyPropertyChanged, IDataErrorInfo
    
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        
        private readonly Dictionary<string, List<string>> _propertyErrors = new Dictionary<string, List<string>>();
        
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _name;
        private string _description ;
        private string _type;
        private string _roomId;

      

        public string _Name { get { return _name; } set { _name = value; OnPropertyChanged("_Name"); } }
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged("Description"); } }
        public string Type { get { return _type; } set { _type = value; OnPropertyChanged("Type"); } }


        public string RoomId 
        {
            get 
            {
                return _roomId; 
            } 
            set 
            {
                if (value != _roomId)
                {
                    _roomId = value;
                    OnPropertyChanged("RoomId");
                }
            }
        }

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

                    case "Type":
                        if (_type.Length == 0) result = "Please enter Room type.";

                        break;
                    default: break;
                }
                return result;
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
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;
        public AddRoom(string value) {
            roomSeparateController.ExecuteRoomSeparating();
            this.DataContext = this;
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
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
            IdTb.Focus();

        }


        private void Button_Click_Cancel(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e) {
            try {
                roomController.Create(RoomId, _Name, Description, Type);
                NavigationService.Navigate(new AddRoom("srb"));

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }
       

    }
}
