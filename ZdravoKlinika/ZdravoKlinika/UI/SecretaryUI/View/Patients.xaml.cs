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

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page, INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private ObservableCollection<Patient> patients;
        public ObservableCollection<Patient> PatientsCollection {
            get => patients;
            set {
                if (patients != value) {
                    patients = value;
                    OnPropertyChanged("PatientsCollection");
                }
            }
        }
        public PatientController patientController;
        public Patients() {

            PatientRepository patientRepository = new PatientRepository(@"..\..\..\Resource\Data\patient.json");
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            PatientsCollection = new ObservableCollection<Patient>(patientController.GetAll());
            this.DataContext = this;
            InitializeComponent();
            
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e) {
            string? jmbg = ((Button)sender).Tag as string;
            Console.WriteLine(jmbg);
            if (jmbg is null) return;
            patientController.Delete(jmbg);
            PatientsCollection = new ObservableCollection<Patient>(patientController.GetAll());
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e) {
            string? jmbg = ((Button)sender).Tag as string;
            Console.WriteLine(jmbg);
            if (jmbg is null) return;
        }
    }
}
