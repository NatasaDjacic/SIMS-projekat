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
    public partial class NewReminder : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }



        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    
                    OnPropertyChanged("Name");
                }
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    CheckDates();
                    OnPropertyChanged("Date");
                }
            }
        }

        public NewReminder()
        {
            this.name = name;
            this.date = DateTime.Now;
            this.DataContext = this;
            InitializeComponent();
            CheckDates();
        }

        PatientReminderController patientReminderController = GLOBALS.patientReminderController;
        AuthService authService = GLOBALS.authService;


        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(name == null) { name = "Alarm " + patientReminderController.GenerateNewId(); }
            PatientReminder reminder = new PatientReminder(patientReminderController.GenerateNewId(), authService.user.JMBG, date, name);
            patientReminderController.Save(reminder);
            NavigationService.Navigate(new Reminders());
        }



        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Reminders());
        }



        private void CheckDates()
        {
            if (dateTB == null) return;
            if (date.CompareTo(DateTime.Now.AddDays(-1)) < 0)
            {


               dateTB.Text = "Date of reminder can't be before today. Choose again!";

                dateTB.Foreground = Brushes.Red;
            }
            else
            {
                dateTB.Text = " ";
                dateTB.Foreground = Brushes.Gray;
            }

        }

    }
}
