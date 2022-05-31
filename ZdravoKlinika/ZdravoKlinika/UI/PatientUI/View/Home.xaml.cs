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

namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Home
    {
        public Home()
        {
            this.DataContext = this;
            InitializeComponent();
            CheckAlarm();
        }

        PatientReminderController patientReminderController = GLOBALS.patientReminderController;
        AuthService authService = GLOBALS.authService;

        private void CheckAlarm()
        {
            if (ReminderTB == null) return;
            var list=patientReminderController.GetByPatientJMBG(authService.user.JMBG);
            List<PatientReminder> reminders = new List<PatientReminder>();
            foreach(var item in list)
            {
                if(item.date.CompareTo(DateTime.Now.AddMinutes(30)) < 0 && item.date.CompareTo(DateTime.Now) >= 0)
                {
                    reminders.Add(item);
                }
            }

            foreach(var item in reminders)
            {
               if(item.date==reminders.Min(a => a.date))
                {
                    ReminderTB.Text = "ALARM at " + item.date + " name " + item.name;
                    ReminderTB.Foreground = Brushes.Red;
                }
            
            }
            

        }

    }
}

