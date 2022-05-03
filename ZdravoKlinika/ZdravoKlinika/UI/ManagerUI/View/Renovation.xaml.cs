﻿using System;
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
    /// <summary>
    /// Interaction logic for Renovation.xaml
    /// </summary>
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
        public Renovations(string value)
        {
            val = value;
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
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

     

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string val;
            System.Collections.IList list = suggestionController.getRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration);
       
            foreach(Renovation ren in suggestionController.getRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration))
            {
                Console.WriteLine(ren.startTime.ToString());
                //Console.WriteLine(ren.renovationId.ToString());
                //Console.WriteLine(ren.startTime.ToString());

            }
            Console.Write("Enter index: ");
            val = Console.ReadLine();
            Console.WriteLine("Enter description: ");
            string desc = Console.ReadLine();
            int index = Convert.ToInt32(val);
            var first = suggestionController.getRenovationSuggestion(SelectedRoom.roomId, StartDate, EndDate, Duration)[index];
            renovationController.SaveRenovation(first.startTime, first.duration, first.roomId, desc);



        }

    }
}