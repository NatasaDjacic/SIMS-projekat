using System;
using System.Collections.Generic;
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

namespace ZdravoKlinika.UI.SecretaryUI.View {
    public partial class OrderDynamicEquipment : Page, INotifyPropertyChanged, IDataErrorInfo {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string Error {
            get {
                return null;
            }
        }
        public string this[string name] {
            get {
                string result = null;
                switch (name) {
                    case "Quantity":
                        if (Quantity <= 0) result = "Quantity must be positive.";
                        break;
                    case "EquipmentName":
                        if (string.IsNullOrEmpty(EquipmentName)) result = "Equipment Name is requred.";
                        break;
                    default: break;
                }
                return result;
            }
        }



        private string equipmentName;
        public string EquipmentName { get { return equipmentName; } set { equipmentName = value; OnPropertyChanged("EquipmentName"); } }
        private int quantity;
        public int Quantity { get { return quantity; } set { quantity = value; OnPropertyChanged("Quantity"); } }


        OrderEquipmentController orderEquipmentController = GLOBALS.orderEquipmentController;
        public OrderDynamicEquipment() {
            equipmentName = "";
            quantity = 1;
            this.DataContext = this;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            orderEquipmentController.CrateOrderEquipment(EquipmentName, Quantity);
            NavigationService.Navigate(new Dashboard());
        }
    }
}
