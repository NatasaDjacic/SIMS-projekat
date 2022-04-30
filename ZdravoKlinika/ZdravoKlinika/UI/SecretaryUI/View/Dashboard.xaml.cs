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

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page {
        public Dashboard() {
            InitializeComponent();
        }

        private void Patients_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.Patients());
        }
        private void New_Patients_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.AddPatient());
        }

        private void Appointments_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new View.Appointments());
        }

        private void RegularAppointment_Click(object sender, RoutedEventArgs e) {

            NavigationService.Navigate(new View.RegularAppointment());
        }
    }
}
