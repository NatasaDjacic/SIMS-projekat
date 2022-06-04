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
  
    public partial class AddDrug : Page, INotifyPropertyChanged
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

        private string _name = "";
        private string ingredients = "";
        private string alternative = "";
        public string _Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string Ingredients { get { return ingredients; } set { ingredients = value; OnPropertyChanged("Ingredients"); } }
        public string Alternative { get { return alternative; } set { alternative = value; OnPropertyChanged("Alternative"); } }


        public DrugController drugController;
        private ObservableCollection<Drug> drugs;

        public ObservableCollection<Drug> DrugsCollection
        {
            get => drugs;
            set
            {
                if (drugs != value)
                {
                    drugs = value;
                    OnPropertyChanged("DrugsCollection");
                }
            }
        }
        public AddDrug(string value)
        {
            val = value;
            this.DataContext = this;
            DrugRepository drugRepository = new DrugRepository(@"..\..\..\Resource\Data\drug.json");
            DrugService drugService = new DrugService(drugRepository);
            drugController = new DrugController(drugService);
            DrugsCollection = new ObservableCollection<Drug>(drugController.GetAll());
            this.DataContext = this;
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
        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                drugController.Create(_name, ingredients, alternative);
                NavigationService.Navigate(new Drugs("srb"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
