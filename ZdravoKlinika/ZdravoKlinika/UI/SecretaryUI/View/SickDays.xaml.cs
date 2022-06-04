using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ZdravoKlinika.Model;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for SickDays.xaml
    /// </summary>
    public partial class SickDays : Page, INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        HolidayRequestController holidayRequestController = GLOBALS.holidayRequestController;
        DoctorController doctorController = GLOBALS.doctorController;
        public static Dictionary<string, Doctor> doctorsLookup { get; set; }
        public ListCollectionView doctors { get; set; }
        private ObservableCollection<HolidayRequest> holidayRequests;
        public ObservableCollection<HolidayRequest> HolidayRequests {
            get => holidayRequests; set {
                holidayRequests = value;
                OnPropertyChanged("HolidayRequests");
            }
        }
        private int active;
        private List<Doctor> selectedDoctors;

        public SeriesCollection SeriesCollection { get; set; }
        private ObservableCollection<string> labels;
        public ObservableCollection<string> Labels { get=>labels; set {
                labels = value;
                OnPropertyChanged("Labels");

            } }
        public Func<double, string> Formatter { get; set; }

        public SickDays() {
            loadHR();
            var temp = this.doctorController.GetAll();

            doctorsLookup = temp.ToDictionary(d => d.JMBG);
            this.doctors = new ListCollectionView(temp);
            this.doctors.GroupDescriptions.Add(new PropertyGroupDescription("specialization"));

            this.DataContext = this;
            this.Formatter = value => value.ToString("N");
            this.Labels = new ObservableCollection<string>();
            this.SeriesCollection = new SeriesCollection();
            this.selectedDoctors = new List<Doctor>();
            InitializeComponent();
        }
        private void loadHR() {
            this.HolidayRequests = new ObservableCollection<HolidayRequest>(this.holidayRequestController.GetAll().Where(hr => hr.status == Model.Enums.RequestType.PENDING));

        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var addedDoctors = e.AddedItems.Cast<Doctor>().ToList();
            var removedDoctors = e.RemovedItems.Cast<Doctor>().ToList();

            addedDoctors.ForEach(doctor => {
                selectedDoctors.Add(doctor);
            });
            removedDoctors.ForEach(doctor => {
                selectedDoctors.Remove(doctor);
            });
            UpdateChartData();
        }
        public void UpdateChartData() {
            var hrs = this.holidayRequestController.GetAll().Where(hr => hr.status == Model.Enums.RequestType.ACCEPTED);
            var thisYear = new ChartValues<int>();
            var lastYear = new ChartValues<int>();
            this.SeriesCollection.Clear();
            this.Labels.Clear();
            selectedDoctors.ForEach(doctor => {
                Labels.Add(doctor.firstName + " "+ doctor.lastName);
                thisYear.Add(hrs.Where(hr=>hr.doctorJMBG == doctor.JMBG && hr.startTime.Year == DateTime.Now.Year)
                   .Select(hr => hr.duration)
                   .Aggregate(0, (sum, dur)=>sum+dur));
                lastYear.Add(hrs.Where(hr => hr.doctorJMBG == doctor.JMBG && hr.startTime.Year == DateTime.Now.Year-1)
                   .Select(hr => hr.duration)
                   .Aggregate(0, (sum, dur) => sum + dur));
            });
            this.SeriesCollection.Add(new ColumnSeries {
                Title = DateTime.Now.Year.ToString(),
                Values = thisYear
            });

            this.SeriesCollection.Add(new ColumnSeries {
                Title = (DateTime.Now.Year-1).ToString(),
                Values = lastYear
            });
        }
        private void Button_Click(object sender, RoutedEventArgs e) {
            int id = int.Parse(((Button)sender).Tag.ToString());
            this.holidayRequestController.Accept(id);
            loadHR();
            UpdateChartData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {

            active = int.Parse(((Button)sender).Tag.ToString());
            var sw = Application.Current.Windows
            .Cast<Window>()
            .FirstOrDefault(window => window is SecretaryMainWindow) as SecretaryMainWindow;
            if (sw != null) {
                sw.TextInputBtn.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click_Save));
                sw.TextInputBtn.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click_Save));
                sw.TextInput.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e) {



            var sw = Application.Current.Windows
            .Cast<Window>()
            .FirstOrDefault(window => window is SecretaryMainWindow) as SecretaryMainWindow;
            if (sw != null) { 

                this.holidayRequestController.Decline(active, sw.TextInputTB.Text);
                sw.TextInput.Visibility = Visibility.Hidden;
                sw.TextInputTB.Text = "";
                loadHR();
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e) {
            var sw = Application.Current.Windows
           .Cast<Window>()
           .FirstOrDefault(window => window is SecretaryMainWindow) as SecretaryMainWindow;
           if (sw != null) {
                sw.TextInputBtn.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click_Save));
                sw.TextInput.Visibility = Visibility.Hidden;
                sw.TextInputTB.Text = "";
            }
        }
    }
}
