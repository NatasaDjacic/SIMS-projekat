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
    public partial class EditDrug : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string drugId = "";
        private string _name = "";
        private string ingredients = "";
        private string commentDoctor = "";
        private string alternative = "";

        public string Alternative { get { return alternative; } set { alternative = value; OnPropertyChanged("Alternative"); } }
        public string DrugId { get { return drugId; } set { drugId = value; OnPropertyChanged("DrugId"); } }
        public string _Name { get { return _name; } set { _name = value; OnPropertyChanged("_Name"); } }
        public string Ingredients { get { return ingredients; } set { ingredients = value; OnPropertyChanged("Ingredients"); } }
        public string CommentDoctor { get { return commentDoctor; } set { commentDoctor = value; OnPropertyChanged("CommentDoctor"); } }

        public DrugController drugController;
        Drug? drug;
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

        public EditDrug(int drugId)
        {
            this.DataContext = this;
            DrugRepository drugRepository = new DrugRepository(@"..\..\..\Resource\Data\drug.json");
            DrugService drugService = new DrugService(drugRepository);
            drugController = new DrugController(drugService);
            drug = drugController.GetById(drugId);
            if (drug is not null)
            {
                _Name = drug.name;
                Ingredients = drug.ingredients;
                CommentDoctor = drug.comment;
                Alternative = drug.alternative;
            }
            else { NavigationService.GoBack(); }
            DrugsCollection = new ObservableCollection<Drug>(drugController.GetAll());
            this.DataContext = this;
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
                if (drug is not null)
                {
                    drug.name = _Name;
                    drug.ingredients = Ingredients;
                    drug.alternative = Alternative;

                    drugController.Update(drug);
                }
                NavigationService.Navigate(new Drugs());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
