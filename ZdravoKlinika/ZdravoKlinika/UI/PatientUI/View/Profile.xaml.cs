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
using System.Collections.ObjectModel;
using System.ComponentModel;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;


namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Profile : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string password;
        AuthService authService = GLOBALS.authService;
        PatientController patientController = GLOBALS.patientController;
        public Profile()
        {   this.DataContext = this;
            InitializeComponent();
            var patient=patientController.GetById(authService.user.JMBG);

            if (nameTB == null) Console.WriteLine("null");
            nameTB.Text = patient.firstName;

            if (surnameTB == null) Console.WriteLine("null");
            surnameTB.Text = patient.lastName;

            if (jmbgTB == null) Console.WriteLine("null");
            jmbgTB.Text = patient.JMBG;

            if (emailTB == null) Console.WriteLine("null");
            emailTB.Text = patient.email;


            if (phoneTB == null) Console.WriteLine("null");
            phoneTB.Text = patient.phone;

            if (passwordTB == null) Console.WriteLine("null");
            passwordTB.Text = patient.password;



            if (adressTB == null) Console.WriteLine("null");
            adressTB.Text = patient.address;
                
            if (usernameTB == null) Console.WriteLine("null");
            usernameTB.Text = patient.username;


        }

    }
}
