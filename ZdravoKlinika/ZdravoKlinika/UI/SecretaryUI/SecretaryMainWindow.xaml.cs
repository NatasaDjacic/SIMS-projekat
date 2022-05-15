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
using System.Windows.Shapes;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.UI.SecretaryUI {
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class SecretaryMainWindow : Window, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string FullName { get; set; }
        private AuthController authController = GLOBALS.authController;
        public SecretaryMainWindow() {
            FullName = GLOBALS.authService.user.lastName + " " + GLOBALS.authService.user.firstName;
            this.DataContext = this;
            InitializeComponent();
            ContentFrame.Navigate(new View.Dashboard());
        }

        private void Home_Click(object sender, RoutedEventArgs e) {
            ContentFrame.NavigationService.Navigate(new View.Dashboard());
        }

        private void Logout_Click(object sender, RoutedEventArgs e) {
            authController.Logout();
            var window = new MainWindow();
            this.Hide();
            Application.Current.MainWindow = window;
            window.Show();
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            ContentFrame.NavigationService.Navigate(new View.Settings());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            ContentFrame.NavigationService.Navigate(new View.Notifications());

        }
    }
}
