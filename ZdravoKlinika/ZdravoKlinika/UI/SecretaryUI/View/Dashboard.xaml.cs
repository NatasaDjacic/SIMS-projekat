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
using ZdravoKlinika.UI.SecretaryUI.ViewModel;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page { 
        public Dashboard() {
            var sw = Application.Current.Windows
           .Cast<Window>()
           .FirstOrDefault(window => window is SecretaryMainWindow) as SecretaryMainWindow;

            this.DataContext = new DashboardVM(sw.ContentFrame.NavigationService);
            InitializeComponent();
            
        }
    }
}
