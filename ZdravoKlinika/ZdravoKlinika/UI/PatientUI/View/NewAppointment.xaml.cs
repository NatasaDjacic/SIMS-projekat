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
using System.Threading;

namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class NewAppointment : Page, INotifyPropertyChanged, IDataErrorInfo
    {


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "FromDate":
                        if(fromDate.CompareTo(DateTime.Now) < 0) result = "Start date can't be before today or today. Choose again!";
                      break;
                    case "ToDate":
                        if (toDate.CompareTo(DateTime.Now) < 0 || toDate.CompareTo(fromDate) < 0) result = "End date can't be before today or before start date. Choose again!";
                       break;

                    default: break;
                }
                return result;
            }
        }

        DoctorController doctorController = GLOBALS.doctorController;
        RoomController roomController = GLOBALS.roomController;
        AppointmentController appointmentController = GLOBALS.appointmentController;
        PatientController patientController = GLOBALS.patientController;
        SuggestionController suggestionController = GLOBALS.suggestionController;
        

        public ListCollectionView doctors { get; set; }
        private Doctor? selectedDoctor;
        public Doctor? SelectedDoctor
        {
            get => selectedDoctor;
            set
            {
                if (selectedDoctor != value)
                {
                    selectedDoctor = value;
                    CheckDoctorRoom();
                    OnPropertyChanged("SelectedDoctor");
                }
            }
        }

        private string roomId;
        private void CheckDoctorRoom()
        {
            if (selectedDoctor != null && selectedDoctor.roomId != null)
            {
                roomId =  selectedDoctor.roomId;
                
            }
            
        }

        public string priority = "time";
        private DateTime fromDate=DateTime.Now;
        public DateTime FromDate
        {
            get => fromDate;
            set
            {
                if (fromDate != value)
                {
                    fromDate = value;
              
                    OnPropertyChanged("FromDate");
                }
            }
        }
        private DateTime toDate=DateTime.Now.AddDays(1);
        public DateTime ToDate
        {
            get => toDate;
            set
            {
                if (toDate != value)
                {
                    toDate = value;
                 
                    OnPropertyChanged("ToDate");
                }
            }
        }
        private int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        private string JMBG="1231231231231";

        public NewAppointment()
        {
            this.JMBG = JMBG;
          
            this.Duration = 30;
            this.doctors = new ListCollectionView(doctorController.GetAll());
            this.doctors.GroupDescriptions.Add(new PropertyGroupDescription("specialization"));

            this.DataContext = this;
            InitializeComponent();
            CheckDoctorRoom();
          
        }


        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var b = sender as RadioButton;
            if (b != null)
                priority = b.Content as string ?? "time";
        }



        private void Cancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Cancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new Home());
        }


        private void Find_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Find_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {

                NavigationService.Navigate(new SuggestedAppointments(JMBG, selectedDoctor.JMBG, roomId, fromDate, toDate, duration, priority, Model.Enums.AppointmentType.regular));


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void DemoStart_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DemoStart_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ExecuteDemo();
        }

        
        


        #region DEMO
        public Thread DemoThread { get; set; }
        public void ExecuteDemo()
        {
            DemoThread = new Thread(CallDemoMethods);
            DemoThread.Start();
        }

        public void CallDemoMethods()
        {
            while (true)
            {
                comboboxDemo(DoctorsCB);
                Thread.Sleep(500);
                datepickerDemo();
                Thread.Sleep(500);

                datepickerDemo2();
                Thread.Sleep(500);

                radioButtonDemo();
                Thread.Sleep(500);

                buttonDemo();
               
                               
            }
        }

      
      
        private void datepickerDemo()
        {
            try
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    startDateTB.BorderBrush = new SolidColorBrush(Colors.MediumSpringGreen);
                    startDateTB.IsEnabled = true;
                    startDateTB.IsDropDownOpen = true;
                    startDateTB.Text = new DateTime(2022, 06, 17).ToString();
                }));
                Thread.Sleep(300);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    startDateTB.BorderBrush = new SolidColorBrush(Colors.Gray);
                    startDateTB.IsDropDownOpen = false;
                    startDateTB.IsEnabled = false;
                }));
            }
            catch (Exception ex)
            {

            }


        }

        private void datepickerDemo2()
        {
            try
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    endDateTB.BorderBrush = new SolidColorBrush(Colors.MediumSpringGreen);
                    endDateTB.IsEnabled = true;
                    endDateTB.IsDropDownOpen = true;
                    endDateTB.Text = new DateTime(2022, 06, 20).ToString();
                }));
                Thread.Sleep(300);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    endDateTB.BorderBrush = new SolidColorBrush(Colors.Gray);
                    endDateTB.IsDropDownOpen = false;
                    endDateTB.IsEnabled = false;
                }));
            }
            catch (Exception ex)
            {

            }


        }

        private void radioButtonDemo()
        {
            try
            {
                Thread.Sleep(450);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    Doctor.IsChecked = true;
                    Time.IsChecked = false;
                }));
                Thread.Sleep(450);
            }
            catch (Exception ex) { }

        }
        private void comboboxDemo(ComboBox comboBox)
        {
            try
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    comboBox.BorderBrush = new SolidColorBrush(Colors.MediumSpringGreen);
                    comboBox.IsDropDownOpen = true;
                }));
                Thread.Sleep(450);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    comboBox.SelectedIndex = 1;
                    comboBox.IsDropDownOpen = false;
                    comboBox.BorderBrush = new SolidColorBrush(Colors.Gray);
                }));
            }
            catch (Exception ex) { }

        }

      


        private void buttonDemo()
        {
            try
            {
                //Thread.Sleep(850);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    SaveButton.IsEnabled = true;
                    SaveButton.Background = Brushes.GreenYellow;
                }));
                Thread.Sleep(300);
                this.Dispatcher.Invoke((Action)(() =>
                {
                    BrushConverter bc = new BrushConverter();
                    SaveButton.Background = (Brush)bc.ConvertFrom("#4267B2");
                    SaveButton.IsEnabled = false;
                }));
                Thread.Sleep(450);
            }
            catch (Exception ex) { }

        }

        

     

        private void DemoStop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DemoStop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                DemoThread.Abort();
            }
            catch (Exception ex) { }

            NavigationService.Navigate(new NewAppointment());
        }

        #endregion

    }


}
