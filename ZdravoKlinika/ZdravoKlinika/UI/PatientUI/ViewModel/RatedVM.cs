using System;
using System.Collections.Generic;
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
using ZdravoKlinika.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ZdravoKlinika.UI.PatientUI.ViewModel;
using ZdravoKlinika.UI.SecretaryUI;

namespace ZdravoKlinika.UI.PatientUI.ViewModel
{
    public class RatedVM
    {
        NavigationService navigationService { get; set; }
        public RelayCommand NavigateHome { get; set; }

        public RatedVM(NavigationService navigationService)
        {
            this.NavigateHome= new RelayCommand(Execute_Home);
            this.navigationService = navigationService;
        
        }
        private void Execute_Home(object? obj)
        {
            navigationService.Navigate(new View.Home());
        }


    }
}
