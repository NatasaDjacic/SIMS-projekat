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
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.ManagerUI.View
{

    public partial class Equipments : Page, INotifyPropertyChanged
    {
        string val = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<Equipment> equipments;
        public ObservableCollection<Equipment> EquipmentsCollection
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
        public EquipmentController equipmentController;
        public Equipments()
        {
            
            EquipmentRepository equipmentRepository = new EquipmentRepository(@"..\..\..\Resource\Data\equipment.json");
            EquipmentService equipmentService = new EquipmentService(equipmentRepository);
            equipmentController = new EquipmentController(equipmentService);
            EquipmentsCollection = new ObservableCollection<Equipment>(equipmentController.GetAll());
            this.DataContext = this;
            InitializeComponent();

        }
    }
}
