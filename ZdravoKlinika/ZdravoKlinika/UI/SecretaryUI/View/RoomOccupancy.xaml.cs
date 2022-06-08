using System;
using System.Collections.Generic;
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
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;
using LiveCharts;
using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for RoomOccupancy.xaml
    /// </summary>
    public partial class RoomOccupancy : Page, INotifyPropertyChanged, IDataErrorInfo {
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
                    case "ToDate":
                        if (ToDate < FromDate) result = "To date can't be greater then from date.";
                        break;
                    case "SelectedRoom":
                        if (selectedRoom == null) result = "You need to pick room.";
                        break;
                    default: break;
                }
                return result;
            }
        }

        public List<Room> rooms { get; set; }
        private Room? selectedRoom;
        public Room? SelectedRoom {
            get => selectedRoom;
            set {
                if (selectedRoom != value) {
                    selectedRoom = value;
                    OnPropertyChanged("SelectedRoom");
                }
            }
        }
        private DateTime fromDate;
        public DateTime FromDate {
            get => fromDate;
            set {
                if (fromDate != value) {
                    fromDate = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }
        private DateTime toDate;
        public DateTime ToDate {
            get => toDate;
            set {
                if (toDate != value) {
                    toDate = value;
                    OnPropertyChanged("ToDate");
                }
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        private ObservableCollection<string> labels;
        public ObservableCollection<string> Labels {
            get => labels; set {
                labels = value;
                OnPropertyChanged("Labels");

            }
        }
        public Func<double, string> Formatter { get; set; }

        RoomController roomController = GLOBALS.roomController;
        SuggestionService suggestionService = GLOBALS.suggestionService;
        AppointmentService appointmentService = GLOBALS.appointmentService;
        public RoomOccupancy() {
            fromDate = DateTime.Now;
            toDate = DateTime.Now.AddDays(7);
            rooms = roomController.GetAll();
            this.Formatter = value => value.ToString("N");
            this.Labels = new ObservableCollection<string>();
            this.SeriesCollection = new SeriesCollection();
            this.DataContext = this;
            InitializeComponent();
            

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var appointments = appointmentService.GetAllInInterval(fromDate, toDate);
            var roomBusyIntervals = suggestionService.GetRoomBusyInterval(selectedRoom.roomId, appointments, fromDate, toDate);
            this.Labels.Clear();
            this.SeriesCollection.Clear();
            var values = new ChartValues<double>();
            for (var temp = fromDate; temp <= toDate; temp = temp.AddDays(1)) {
                this.Labels.Add(temp.ToShortDateString());
                values.Add(0);
            }
            roomBusyIntervals.ForEach(interval => {
                values[(int)((interval[0] - fromDate).TotalDays)] += ((interval[1] - interval[0]).TotalHours);
            });
            this.SeriesCollection.Add(new ColumnSeries {
                Title = "Houres",
                Values = values
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            // Must have write permissions to the path folder

            var appointments = appointmentService.GetAllInInterval(fromDate, toDate);
            var roomBusyIntervals = suggestionService.GetRoomBusyInterval(selectedRoom.roomId, appointments, fromDate, toDate);
            var counts = new List<int>();
            for (var temp = fromDate; temp <= toDate; temp = temp.AddDays(1)) {
                counts.Add(0);
            }
            roomBusyIntervals.ForEach(interval => {
                counts[(int)((interval[0] - fromDate).TotalDays)] += 1;
            });
            PdfWriter writer = new PdfWriter(@"..\..\..\Resource\PDFs\roomReport-"+ selectedRoom.roomId + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            iText.Layout.Element.Paragraph header = new iText.Layout.Element.Paragraph("Room occupancy report for "+ selectedRoom.name)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(20);
            iText.Layout.Element.LineSeparator lineSeparator = new iText.Layout.Element.LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine());
            iText.Layout.Element.Table table = new iText.Layout.Element.Table(4, true);
            document.Add(header);
            document.Add(lineSeparator);
            iText.Layout.Element.Cell cell;
            cell = new iText.Layout.Element.Cell();
            cell.Add(new iText.Layout.Element.Paragraph("Date"));
            table.AddCell(cell);
            cell = new iText.Layout.Element.Cell();
            cell.Add(new iText.Layout.Element.Paragraph("Start Time"));
            table.AddCell(cell);
            cell = new iText.Layout.Element.Cell();
            cell.Add(new iText.Layout.Element.Paragraph("End Time"));
            table.AddCell(cell);
            cell = new iText.Layout.Element.Cell();
            cell.Add(new iText.Layout.Element.Paragraph("Duration"));
            table.AddCell(cell);
            int i = 0, j = 0;
            roomBusyIntervals.ForEach(interval => {
                while (counts[i] == 0 && i < counts.Count) i++;
                if(counts[i] == j) {
                    i++;
                    j = 0;
                }
                Console.WriteLine(counts[i]);
                if(j == 0) {
                    cell = new iText.Layout.Element.Cell(counts[i], 1);
                    cell.Add(new iText.Layout.Element.Paragraph(interval[0].ToShortDateString()));
                    table.AddCell(cell);
                }
                cell = new iText.Layout.Element.Cell();
                cell.Add(new iText.Layout.Element.Paragraph(interval[0].ToShortTimeString()));
                table.AddCell(cell);
                cell = new iText.Layout.Element.Cell();
                cell.Add(new iText.Layout.Element.Paragraph(interval[1].ToShortTimeString()));
                table.AddCell(cell);
                cell = new iText.Layout.Element.Cell();
                cell.Add(new iText.Layout.Element.Paragraph(ToPrettyFormat(interval[1] - interval[0])));
                table.AddCell(cell);
                j++;
            });
            document.Add(table);
            document.Add(lineSeparator);
            document.Close();
            ActivityHistoryService.Instance.NewActivity(ActivityType.ROOM_REPORT, "New Room Report", String.Format("Room report for room \"{1}\" can be found \ninside Resource/PDFs/roomReport-{0}.pdf", selectedRoom.roomId, selectedRoom.name));
        }
        public string ToPrettyFormat(TimeSpan span) {

            if (span == TimeSpan.Zero) return "0 minutes";

            var sb = new StringBuilder();
            if (span.Days > 0)
                sb.AppendFormat("{0} day{1} ", span.Days, span.Days > 1 ? "s" : String.Empty);
            if (span.Hours > 0)
                sb.AppendFormat("{0} hour{1} ", span.Hours, span.Hours > 1 ? "s" : String.Empty);
            if (span.Minutes > 0)
                sb.AppendFormat("{0} minute{1} ", span.Minutes, span.Minutes > 1 ? "s" : String.Empty);
            return sb.ToString();

        }
    }
}
