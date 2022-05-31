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
using ZdravoKlinika.UI;

namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Reminders : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private ObservableCollection<PatientReminder> reminders;
        public ObservableCollection<PatientReminder> RemindersCollection
        {
            get => reminders;
            set
            {
                if (reminders != value)
                {
                    reminders = value;
                    OnPropertyChanged("RemindersCollection");
                }
            }
        }

        PatientReminderController patientReminderController = GLOBALS.patientReminderController;
        AuthService authService = GLOBALS.authService;
        public Reminders()
        {


            RemindersCollection = new ObservableCollection<PatientReminder>(patientReminderController.GetByPatientJMBG(authService.user.JMBG));
            this.DataContext = this;
            InitializeComponent();

        }

        private void NewReminder_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewReminder_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new NewReminder());
        }

        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }

    }
}



