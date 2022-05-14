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
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;

namespace ZdravoKlinika.UI.ManagerUI.View
{
  
    public partial class AddDrug : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string name = "";
        private string ingredients = "";
        public string _Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string Ingredients { get { return ingredients; } set { ingredients = value; OnPropertyChanged("Ingredients"); } }

        public DrugController drugController;
        public AddDrug()
        {
            this.DataContext = this;
            DrugRepository drugRepository = new DrugRepository(@"..\..\..\Resource\Data\drug.json");
            DrugService drugService = new DrugService(drugRepository);
            drugController = new DrugController(drugService);

            InitializeComponent();
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                drugController.Create(name, ingredients);
                NavigationService.Navigate(new Drugs());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
