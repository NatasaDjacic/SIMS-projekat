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
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.UI.SecretaryUI {
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window {
        private AuthController authController = GLOBALS.authController;
        public SecretaryMainWindow() {
            InitializeComponent();
            ContentFrame.Navigate(new View.Patients());
        }

        private void Logout_Click(object sender, RoutedEventArgs e) {
            authController.Logout();
            var window = new MainWindow();
            this.Hide();
            Application.Current.MainWindow = window;
            window.Show();
            this.Close();

        }
    }
}
