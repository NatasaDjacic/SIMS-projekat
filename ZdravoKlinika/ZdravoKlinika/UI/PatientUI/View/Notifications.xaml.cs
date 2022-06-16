
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


namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Notifications: Page
    {

            public Notifications()
            {
                var sw = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is PatientMainWindow) as PatientMainWindow;

                this.DataContext = new NotificationsVM(sw.ContentFrame.NavigationService);
                InitializeComponent();
            }
        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
    }
}


