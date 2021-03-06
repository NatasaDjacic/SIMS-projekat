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

        private Room selectedRoom;
        public Room SelectedRoom
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
        private Room selectedRoomTo;
        public Room SelectedRoomTo
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

        public Equipments(string value)
        {
            val = value;
            EquipmentRepository equipmentRepository = new EquipmentRepository(@"..\..\..\Resource\Data\equipment.json");
            EquipmentService equipmentService = new EquipmentService(equipmentRepository);
            equipmentController = new EquipmentController(equipmentService);
            DynamicEquipmentCollection = new ObservableCollection<Equipment>(equipmentController.GetAllDynamic());
            equipMovingController.CheckEquipMoving();
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
            var g = EquipRoomGroupDTO.groupEquip(equipmentController.GetAll().FindAll(e => e.type == "Staticka"));
            EquipmentsCollection = new ObservableCollection<EquipRoomGroupDTO>(g);
            SelectedDate = DateTime.Now;
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
            if (DateTB == null) return;
            if (SelectedDate.CompareTo(DateTime.Today) < 0)
            {


                DateTB.Text = "Start date cannot be in past. Choose again!";

                DateTB.Foreground = Brushes.Red;
            }
            else
            {
                DateTB.Text = " ";
                DateTB.Foreground = Brushes.Gray;
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
                if (val == "en")
                {
                    MessageBox.Show("You have successfully created appointment for moving equipments.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                          if (val == "rus")
                {
                    MessageBox.Show("Вы успешно создали отчет о занятости номеров.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Uspešno ste zakazali premeštanje opreme.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                NavigationService.Navigate(new Equipments(val));

            }
            catch
            {
                if (val == "en")
                {
                    MessageBox.Show("Not enough equipment in room!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
              if (val == "rus")
                {
                    MessageBox.Show("Закройте его и попробуйте еще раз!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Nedovoljno opreme u prostoriji!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
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
