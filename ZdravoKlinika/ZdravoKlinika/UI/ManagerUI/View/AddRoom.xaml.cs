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

namespace ZdravoKlinika.UI.ManagerUI.View {
   
    public partial class AddRoom : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private string _name = "";
        private string _description = "";
        private string _type = "";
        private string _roomId = "";

        public string _Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string Description { get { return _description; } set { _description = value; OnPropertyChanged("Description"); } }
        public string Type { get { return _type; } set { _type = value; OnPropertyChanged("Type"); } }
      
        public string RoomId { get { return _roomId; } set { _roomId = value; OnPropertyChanged("RoomId"); } }

        public RoomController roomController;
        public AddRoom() {
            this.DataContext = this;
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            InitializeComponent();
            IdTb.Focus();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e) {
            try {
                roomController.Create(RoomId, _Name, Description, Type);
                NavigationService.Navigate(new Rooms());
                

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }

        
    }
}
