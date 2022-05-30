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
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;

namespace ZdravoKlinika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            OnLoginRedirect();
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e) {
            if (GLOBALS.authController.Login(Username_TB.Text, Password_TB.Text)) {
                OnLoginRedirect();
            } else {
                try {
                    Console.WriteLine("Not valid username or password or account restricted due too much cancellation.");
                } catch (Exception ex) {
                    System.Diagnostics.Trace.WriteLine(ex.Message);
                }
            }

        }
        private void OnLoginRedirect() {
            var role = GLOBALS.authService.user_role;
            Window window;
            if(role == ROLE.MANAGER) {
                window = new UI.ManagerUI.ManagerMainWindow();
            }else if(role == ROLE.SECRETARY) {
                window = new UI.SecretaryUI.SecretaryMainWindow();
            }else if(role == ROLE.PATIENT) {
                window = new UI.PatientUI.PatientMainWindow();
            }else if(role == ROLE.DOCTOR) {
                window = new UI.DoctorUI.DoctorMainWindow();
            } else {
                return;
            }
            Application.Current.MainWindow = window;
            this.Close();
            window.Show();
        }

        //Leagacy
        private void Button_Click(object sender, RoutedEventArgs e) {
            var window = new UI.DoctorUI.DoctorMainWindow();
            window.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            var window = new UI.ManagerUI.ManagerMainWindow();
            window.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) {
            var window = new UI.PatientUI.PatientMainWindow();
            window.Show();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e) {
            var window = new UI.SecretaryUI.SecretaryMainWindow();
            window.Show();
        }
    }
}
