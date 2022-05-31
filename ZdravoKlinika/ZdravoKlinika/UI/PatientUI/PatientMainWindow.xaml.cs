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
       
        private void Home_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Enable_Home(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Home());
        }


        private void Appointments_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Enable_Appointments(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Appointments());
        }

        private void New_Appointment_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Enable_New_Appointment(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.NewAppointment());
        }


        private void Notifications_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Enable_Notifications(object sender, ExecutedRoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Reminders());
        }

        private void Log_Out_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Enable_Log_Out(object sender, ExecutedRoutedEventArgs e)
        {
            authController.Logout();
            var window = new MainWindow();
            this.Hide();
            Application.Current.MainWindow = window;
            window.Show();
            this.Close();

        }

        public void Restricted()
        {
            var window = new MainWindow();
            this.Hide();
            Application.Current.MainWindow = window;
            window.Show();
            this.Close();

        }

        private void Click_reports(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Reports());
        }

    }
}

