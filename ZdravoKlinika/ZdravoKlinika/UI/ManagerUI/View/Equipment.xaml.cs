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
using ZdravoKlinika.Model.DTO;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.ManagerUI.View
{

    public partial class Equipments : Page, INotifyPropertyChanged
    {
        string val = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string roomId)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(roomId));
            }
        }

        private ObservableCollection<EquipRoomGroupDTO> equipments;
        public ObservableCollection<EquipRoomGroupDTO> EquipmentsCollection
        {
            get => equipments;
            set
            {
                if (equipments != value)
                {
                    equipments = value;
                    OnPropertyChanged("EquipmentsCollection");
                }
            }
        }

        private DoctorsMarkDTO selectedRoom;
        public DoctorsMarkDTO SelectedRoom
        {
            get => selectedRoom;
            set
            {
                if (selectedRoom != value)
                {
                    selectedRoom = value;
                    LoadEquipmentForRoom();
                    OnPropertyChanged("SelectedRoom");
                }
            }
        }
        private DoctorsMarkDTO selectedRoomTo;
        public DoctorsMarkDTO SelectedRoomTo
        {
            get => selectedRoomTo;
            set
            {
                if (selectedRoomTo != value)
                {
                    selectedRoomTo = value;
                    OnPropertyChanged("SelectedRoomTo");
                }
            }
        }
        
        private string equipQuantity;
        public string EquipQuantity
        {
            get => equipQuantity;
            set
            {
                if (equipQuantity != value)
                {
                    equipQuantity = value;
                    OnPropertyChanged("EquipQuantity");
                }
            }
        }

        private string searchEquip;
        public string SearchEquip
        {
            get => searchEquip;
            set
            {
                if (searchEquip != value)
                {
                    searchEquip = value;
                    OnPropertyChanged("SearchEquip");
                }
            }
        }


        private EquipRoomGroupDTO equip;
        public EquipRoomGroupDTO Equip
        {
            get => equip;
            set
            {
                if (equip != value)
                {
                    equip = value;
                    OnPropertyChanged("Equip");
                }
            }
        }
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    CheckDates();
                    OnPropertyChanged("SelectedDate");
                }
            }
        }
      

        private ObservableCollection<EquipRoomGroupDTO> equipmentsRoom;
        public ObservableCollection<EquipRoomGroupDTO> EquipmentsRoomCollection
        {
            get => equipmentsRoom;
            set
            {
                if (equipmentsRoom != value)
                {
                    equipmentsRoom = value;
                    OnPropertyChanged("EquipmentsRoomCollection");
                }
            }
        }
        EquipMovingController equipMovingController = GLOBALS.equipMovingController;
        EquipmentController equipmentController = GLOBALS.equipmentController;
        RoomController roomController = GLOBALS.roomController;

        public Equipments()
        {
            EquipmentRepository equipmentRepository = new EquipmentRepository(@"..\..\..\Resource\Data\equipment.json");
            EquipmentService equipmentService = new EquipmentService(equipmentRepository);
            equipmentController = new EquipmentController(equipmentService);
            DynamicEquipmentCollection = new ObservableCollection<Equipment>(equipmentController.GetAllDynamic());
            equipMovingController.CheckEquipMoving();
            RoomsCollection = new ObservableCollection<DoctorsMarkDTO>(roomController.GetAll());
            this.DataContext = this;
            ResourceDictionary dictionary = new ResourceDictionary();
            var g = EquipRoomGroupDTO.groupEquip(equipmentController.GetAll().FindAll(e => e.type == "Staticka"));
            EquipmentsCollection = new ObservableCollection<EquipRoomGroupDTO>(g);
            SelectedDate = DateTime.Now;
            InitializeComponent();
            CheckDates();

        }

        private void CheckDates()
        {
            if (DateTB == null) return;
            if (SelectedDate.CompareTo(DateTime.Now) < 0)
            {


                DateTB.Text = "Start date can't be today or before. Choose again!";

                DateTB.Foreground = Brushes.Red;
            }
            else
            {
                DateTB.Text = " ";
                DateTB.Foreground = Brushes.Gray;
            }

        }

        private ObservableCollection<DoctorsMarkDTO> rooms;
        public ObservableCollection<DoctorsMarkDTO> RoomsCollection
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
        public void LoadEquipmentForRoom()
        {
            var g = EquipRoomGroupDTO.groupEquip(equipmentController.GetAll().FindAll(e => e.roomId == selectedRoom.roomId && e.type == "Staticka"));
            EquipmentsRoomCollection = new ObservableCollection<EquipRoomGroupDTO>(g);
        }

        public void EquipMoving_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                equipMovingController.CreateEquipMovingRoomFrom(SelectedDate, SelectedRoom.roomId, SelectedRoomTo.roomId, equipMovingController.GetEquipIdsForMove(Equip.equipIds, int.Parse(EquipQuantity)));
            }
            catch (Exception ex) { Console.WriteLine("Not enough equip in selected room."); };
        }

        private ObservableCollection<Equipment> dynEquips;
        public ObservableCollection<Equipment> DynamicEquipmentCollection
        {
            get => dynEquips;
            set
            {
                if (dynEquips != value)
                {
                    dynEquips = value;
                    OnPropertyChanged("DynamicEquipmentCollection");
                }
            }
        }
        private ObservableCollection<Equipment> searchedEquips;
        public ObservableCollection<Equipment> EquipSearchCollection
        {
            get => searchedEquips;
            set
            {
                if (searchedEquips != value)
                {
                    searchedEquips = value;
                    OnPropertyChanged("EquipSearchCollection");
                }
            }
        }
        public void Search_Click(object sender, RoutedEventArgs e)
        {
            string search = searchEquip;
            DynamicEquipmentCollection = new ObservableCollection<Equipment>(equipmentController.GetAllDynamic().FindAll(equip => equip.name.Contains(search)));

            var g = EquipRoomGroupDTO.groupEquip(equipmentController.FindAll(search).FindAll(e => e.type == "Staticka"));
            EquipmentsCollection = new ObservableCollection<EquipRoomGroupDTO>(g);

        }

    }
}
