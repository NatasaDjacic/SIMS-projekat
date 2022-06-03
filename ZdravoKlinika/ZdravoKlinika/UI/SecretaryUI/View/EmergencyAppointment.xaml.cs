using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.DTO;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for EmergencyAppointment.xaml
    /// </summary>
    public partial class EmergencyAppointment : Page, INotifyPropertyChanged, IDataErrorInfo {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string Error {
            get {
                return null;
            }
        }
        public string this[string name] {
            get {
                string result = null;
                switch (name) {
                    case "JMBG":
                        if (JMBG.Trim() == "") result = "JMBG should be set.";
                        else if (JMBG.Length != 13) result = "JMBG should have 13 digits.";
                        else if (!Regex.IsMatch(JMBG, "^[1-9]*$")) result = "JMBG should have 13 digits.";
                        else if (!patientFound) {
                            result = "Patient not found";
                        };
                        break;
                    default: break;
                }
                return result;
            }
        }

        DoctorController doctorController = GLOBALS.doctorController;
        AppointmentController appointmentController = GLOBALS.appointmentController;
        PatientController patientController = GLOBALS.patientController;
        EmergencyAppointmentSuggestionDTO emergencyAppointmentSuggestionDTO;
        public EmergencyAppointmentSuggestionDTO EAS {
            get => emergencyAppointmentSuggestionDTO;
            set {
                emergencyAppointmentSuggestionDTO = value;
                OnPropertyChanged("EAS");
            }
        }
        DoctorAndMove selected;
        public DoctorAndMove Selected {
            get => selected;
            set {
                selected = value;
                OnPropertyChanged("Selected");
            }
        } 
        public List<DoctorAndMove> DAM {
            get; set;
        }

        private bool patientFound = false;
        private bool patientNotFound = false;
        public bool PatientNotFound { get => patientNotFound; set { patientNotFound = value; OnPropertyChanged("PatientNotFound");} }
        private string jmbg;
        public string JMBG {
            get => jmbg;
            set {
                if (jmbg != value) {
                    jmbg = value;
                    CheckPatientJMBG();
                    OnPropertyChanged("JMBG");
                }
            }
        }
        private string specialization;
        public string Specialization {
            get => specialization;
            set {
                if (specialization != value) {
                    specialization = value;
                    OnPropertyChanged("Specialization");
                }
            }
        }

        private string _firstName = "";
        private string _lastName = "";
        public string FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged("FirstName"); } }
        public string LastName { get { return _lastName; } set { _lastName = value; OnPropertyChanged("LastName"); } }
        public EmergencyAppointment() {
            this.jmbg = "";
            this.specialization = "";
            this.DataContext = this;
            this.patientNotFound = false;
            InitializeComponent();
        }

        private void CheckPatientJMBG() {
            if (jmbg.Length >= 13) {
                var pat = patientController.GetById(JMBG);
                PatientTB.Text = pat != null ? ("Found: " + pat.firstName + " " + pat.lastName) : "Not Found";
                if (pat != null) {
                    patientFound = true;
                    patientFound = false;
                    PatientTB.Foreground = Brushes.Green;
                    FirstName = pat.firstName;
                    LastName = pat.lastName;
                } else {
                    patientFound = false;
                    patientNotFound = true;
                    PatientTB.Foreground = Brushes.Red;
                    FirstName = "";
                    LastName = "";
                }
            } else {
                patientFound = false;
                patientNotFound = false;
                PatientTB.Text = "-------------------------------";
                PatientTB.Foreground = Brushes.Gray;
                FirstName = "";
                LastName = "";
            }
            OnPropertyChanged("PatientNotFound");
        }


        private void Button_Click(object sender, RoutedEventArgs e) {
            var pat = patientController.GetById(JMBG);
            if(pat is null) {
                patientController.CreateGuestAccount(FirstName, LastName, JMBG);
                pat = patientController.GetById(JMBG);
            }
            try {
                this.EAS = appointmentController.createOrSuggestEmergencyAppointment(pat, specialization);
                if (!this.EAS.found) {
                    this.DAM = new List<DoctorAndMove>();
                    for (int i = 0; i < this.EAS.pairsOfAppointmentAndMovedAppointment.Count; i++) {
                        var pair = this.EAS.pairsOfAppointmentAndMovedAppointment[i];
                        var doc = doctorController.GetById(pair.Item2.doctorJMBG);
                        DAM.Add(new DoctorAndMove(i, doc.firstName + " " + doc.lastName, (pair.Item2.startTime - pair.Item1.startTime).Days));
                    }
                    OnPropertyChanged("DAM");
                } else {
                    NavigationService.Navigate(new Dashboard());
                }


            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) {
            if(Selected != null) {
                this.appointmentController.CreateAppointmentFromSuggestion(this.EAS.pairsOfAppointmentAndMovedAppointment[Selected.index].Item1);
                this.appointmentController.MoveAppointmentSecretary(this.EAS.pairsOfAppointmentAndMovedAppointment[Selected.index].Item2, this.EAS.pairsOfAppointmentAndMovedAppointment[Selected.index].Item2.startTime);
            }
        }


        public class DoctorAndMove {
            public int index { get; set; }
            public string fName { get; set; }
            public int days { get; set; }

            public DoctorAndMove(int index, string fName, int days) {
                this.index = index;
                this.fName = fName;
                this.days = days;
            }
            public override string ToString() {
                return fName + " " + days + " days";
            }
        }

    }
}
