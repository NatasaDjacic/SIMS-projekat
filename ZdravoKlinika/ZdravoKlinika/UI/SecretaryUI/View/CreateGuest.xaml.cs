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

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for CreateGuest.xaml
    /// </summary>
    
    public partial class CreateGuest : Page {
        PatientController patientController;
        public CreateGuest() {
            PatientRepository patientRepository = new PatientRepository(@"..\..\..\Resource\Data\patient.json");
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            InitializeComponent();
        }
        
        private void Button_Click_Save(object sender, RoutedEventArgs e) {
            try {
                patientController.CreateGuestAccount(FN_TB.Text, LN_TB.Text, JMBG_TB.Text);
                NavigationService.Navigate(new Patients());
            } catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs e) {

            NavigationService.GoBack();
        }
    }
}
