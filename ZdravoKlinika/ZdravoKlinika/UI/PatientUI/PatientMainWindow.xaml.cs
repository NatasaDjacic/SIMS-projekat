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


namespace ZdravoKlinika.UI.PatientUI {
    /// <summary>
    /// Interaction logic for PatientMainWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window {

        private AuthController authController = GLOBALS.authController;
        public PatientMainWindow() {
            InitializeComponent();
            ContentFrame.Navigate(new View.Home());
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Home());
        }

        private void Appointments_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Appointments());
        }

        
         private void New_Appointment_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.NewAppointment());
        }

        private void Notification_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Notifications());
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            authController.Logout();
            var window = new MainWindow();
            this.Hide();
            Application.Current.MainWindow = window;
            window.Show();
            this.Close();

        }

    }
}

