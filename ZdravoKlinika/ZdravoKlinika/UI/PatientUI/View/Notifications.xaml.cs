﻿
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
    public partial class Notifications: Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private ObservableCollection<Notification> prescriptions;
        public ObservableCollection<Notification> PrescriptionsCollection
        {
            get => prescriptions;
            set
            {
                if (prescriptions != value)
                {
                    prescriptions = value;
                    OnPropertyChanged("PrescriptionsCollection");
                }
            }
        }

        NotificationService notificationService= GLOBALS.notificationService;
        public Notifications()
        {
            

            PrescriptionsCollection = new ObservableCollection<Notification>(notificationService.getPatientNotifications());
            this.DataContext = this;
            InitializeComponent();

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


