using System;
using System.Collections.Generic;
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
   
    public partial class EditRoom : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
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

        public RoomController roomController;
        Room? r;
        public EditRoom(string roomId) {
            this.DataContext = this;
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            r = roomController.GetById(roomId);
            if (r is not null) {
                RoomId = r.roomId;
                _Name = r.name;
                Description = r.description;
                Type = r.type;
            } else { NavigationService.GoBack(); }
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
                NavigationService.Navigate(new Rooms());


            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

        }

      
    }
}
