using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.Text;

namespace ZdravoKlinika.UI.ManagerUI.View {
   
    public partial class Rooms : Page, INotifyPropertyChanged {
        string val = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DateTime fromDate;
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
        private DateTime toDate;
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


        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> RoomsCollection {
            get => rooms;
            set {
                if (rooms != value) {
                    rooms = value;
                    OnPropertyChanged("RoomsCollection");
                }
            }
        }
        public RoomController roomController;
        RoomMergeController roomMergeController = GLOBALS.roomMergeController;
        RoomSeparateController roomSeparateController = GLOBALS.roomSeparateController;

        public Rooms(string value) {
            fromDate = DateTime.Now;
            toDate = DateTime.Now.AddDays(7);
            roomSeparateController.ExecuteRoomSeparating();
            roomMergeController.ExecuteRoomMerging();
            val = value;
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
            this.DataContext = this;
            Console.WriteLine(value);
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (value)
            {
                case "en":
                    Console.WriteLine("en");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.en.xaml", UriKind.Relative);
                    break;
                case "rus":
                    Console.WriteLine("rus");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.rus.xaml", UriKind.Relative);
                    break;
                case "srb":
                    Console.WriteLine("srb");
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.xaml", UriKind.Relative);
                    break;
                default:
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dictionary);
            InitializeComponent();

        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e) {
            string? roomId = ((Button)sender).Tag as string;
            Console.WriteLine(roomId);
            if (roomId is null) return;
            roomController.Delete(roomId);
            RoomsCollection = new ObservableCollection<Room>(roomController.GetAll());
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e) {
            string? roomId = ((Button)sender).Tag as string;
            Console.WriteLine(roomId);
            if (roomId is null) return;
            NavigationService.Navigate(new EditRoom(roomId, val));
        }

        private void Button_Click_New(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new AddRoom(val));
        }
        SuggestionService suggestionService = GLOBALS.suggestionService;
        AppointmentService appointmentService = GLOBALS.appointmentService;
        private void GeneratePDF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var appointments = appointmentService.GetAllInInterval(fromDate, toDate);
                var rooms = new List<Room>();
                rooms = roomController.GetAll();
                PdfWriter writer = new PdfWriter(@"..\..\..\Resource\PDFs\roomsReport.pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                Paragraph paragraph = new Paragraph("Hospital Zdravo")
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                   .SetFontSize(11);
                document.Add(paragraph);

                Paragraph header = new Paragraph("Room occupancy report")
                       .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                       .SetFontSize(20);
                document.Add(header);


                foreach (var room in rooms)
                {
                    var roomBusyIntervals = suggestionService.getRoomBusyInterval(room.roomId, appointments, fromDate, toDate);
                    var counts = new List<int>();
                    for (var temp = fromDate; temp <= toDate; temp = temp.AddDays(1))
                    {
                        counts.Add(0);
                    }
                    roomBusyIntervals.ForEach(interval =>
                    {
                        counts[(int)((interval[0] - fromDate).TotalDays)] += 1;
                    });

                    Paragraph subheader = new Paragraph("Room occupancy report for " + room.name)
                       .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                       .SetFontSize(17);
                    LineSeparator lineSeparator = new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine());
                    Table table = new Table(4, true);
                    document.Add(subheader);
                    document.Add(lineSeparator);
                    Cell cell;
                    cell = new Cell();
                    cell.Add(new Paragraph("Date"));
                    table.AddCell(cell);
                    cell = new Cell();
                    cell.Add(new Paragraph("Start Time"));
                    table.AddCell(cell);
                    cell = new Cell();
                    cell.Add(new Paragraph("End Time"));
                    table.AddCell(cell);
                    cell = new Cell();
                    cell.Add(new Paragraph("Duration"));
                    table.AddCell(cell);
                    int i = 0, j = 0;
                    roomBusyIntervals.ForEach(interval =>
                    {
                        while (counts[i] == 0 && i < counts.Count) i++;
                        if (counts[i] == j)
                        {
                            i++;
                            j = 0;
                        }
                        Console.WriteLine(counts[i]);
                        if (j == 0)
                        {
                            cell = new Cell(counts[i], 1);
                            cell.Add(new Paragraph(interval[0].ToShortDateString()));
                            table.AddCell(cell);
                        }
                        cell = new Cell();
                        cell.Add(new Paragraph(interval[0].ToShortTimeString()));
                        table.AddCell(cell);
                        cell = new Cell();
                        cell.Add(new Paragraph(interval[1].ToShortTimeString()));
                        table.AddCell(cell);
                        cell = new Cell();
                        cell.Add(new Paragraph(ToPrettyFormat(interval[1] - interval[0])));
                        table.AddCell(cell);
                        j++;
                    });
                    document.Add(table);
                    document.Add(lineSeparator);
                }
                Paragraph blankSpace = new Paragraph("")
                      .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                      .SetFontSize(20);
                document.Add(blankSpace);
                LineSeparator ls = new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.DashedLine());
                document.Add(ls);
                Paragraph paragraph1 = new Paragraph("Manager Bojan Radojicic")
                .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                .SetFontSize(11);
                document.Add(paragraph1);
                document.Close();
                if (val == "en")
                {
                    MessageBox.Show("You have successfully created a room occupancy report.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
             if (val == "rus")
                {
                    MessageBox.Show("Вы успешно создали отчет о занятости номеров.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    MessageBox.Show("Uspešno ste kreirali izveštaj o zauzetosti prostorija.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

            }
            catch
            {
                if (val == "en")
                {
                    MessageBox.Show("PDF document is open. Close it and try again!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
              if (val == "rus")
                {
                    MessageBox.Show("PDF-документ уже открыт. Закройте его и попробуйте еще раз!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("PDF dokument je već otvoren. Zatvorite ga i pokušajte ponovo!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
        }

        public string ToPrettyFormat(TimeSpan span)
        {

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
