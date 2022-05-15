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
    
    public partial class Drugs : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private ObservableCollection<Drug> drugs;

        public ObservableCollection<Drug> DrugsCollection
        {
            get => drugs;
            set
            {
                if(drugs != value)
                {
                    drugs = value;
                    OnPropertyChanged("DrugsCollection");
                }
            }
        }
        public DrugController drugController;
        public Drugs()
        {
            DrugRepository drugRepository = new DrugRepository(@"..\..\..\Resource\Data\drug.json");
            DrugService drugService = new DrugService(drugRepository);
            drugController = new DrugController(drugService);
            DrugsCollection = new ObservableCollection<Drug>(drugController.GetAll());
            this.DataContext = this;
            InitializeComponent();
        }

        private void Button_Click_New_Drug(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDrug());
        }
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            int drugId = Convert.ToInt32(((Button)sender).Tag);
            Console.WriteLine(drugId);
            if (drugId == 0) return;
            NavigationService.Navigate(new EditDrug((drugId)));
        }
    }
}
