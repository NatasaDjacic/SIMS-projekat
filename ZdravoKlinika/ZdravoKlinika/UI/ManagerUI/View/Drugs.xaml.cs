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
        string val = string.Empty;
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
        public Drugs(string value)
        {
            val = value;
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

        private void Button_Click_New_Drug(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDrug(val));
        }
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            int drugId = Convert.ToInt32(((Button)sender).Tag);
            DrugRepository drugRepository = new DrugRepository(@"..\..\..\Resource\Data\drug.json");
            if (drugRepository.GetById(drugId).approved is 0 or (Model.Enums.DrugStatus)2)
            {
                Console.WriteLine(drugRepository.GetById(drugId).approved.ToString());
              
                if(val == "en")
                {
                    MessageBox.Show("You can only change medications that have not been verified by a doctor!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                } else
                if (val == "rus")
                {
                    MessageBox.Show("Менять лекарства можно только не проверенные врачом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               else
                {
                    MessageBox.Show("Možete menjati samo lekove koji nisu verifikovani od strane lekara!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                //NavigationService.Navigate(new Drugs(val));
            }
            if (drugId == 0) return;
            NavigationService.Navigate(new EditDrug(drugId, val));
        }
    }
}
