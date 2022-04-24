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
using System.Windows.Shapes;

namespace ZdravoKlinika.UI.SecretaryUI {
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window {
        public SecretaryMainWindow() {
            InitializeComponent();
            ContentFrame.Navigate(new View.Dashboard());
        }

        private void Home_Click(object sender, RoutedEventArgs e) {
            ContentFrame.NavigationService.Navigate(new View.Dashboard());
        }
    }
}
