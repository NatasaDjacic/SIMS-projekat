using System;
using System.IO;
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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Calendar
    {
    public Calendar() { InitializeComponent(); }

        private void Print_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Print_Executed(object sender, ExecutedRoutedEventArgs e)
        {
          
            PdfWriter writer = new PdfWriter(@"..\..\..\Resource\PDFs\PatientTheraphy.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            iText.Layout.Element.Paragraph header = new iText.Layout.Element.Paragraph(" Theraphy schedule ")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(20);
            
            document.Add(header);
            // Table
            iText.Layout.Element.Table table = new iText.Layout.Element.Table(8, false);
            Cell cell11 = new Cell(1, 1)
               .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
               .SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Time"));
            Cell cell12 = new Cell(1, 1)
               .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
               .SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Monday"));
            Cell cell13 = new Cell(1, 1)
               .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
               .SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Tuesday"));
            Cell cell14 = new Cell(1, 1)
               .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
               .SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Wednesday"));
            Cell cell15 = new Cell(1, 1)
               .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
               .SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Thursday"));
            Cell cell16 = new Cell(1, 1)
               .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
               .SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Friday"));
            Cell cell17 = new Cell(1, 1)
               .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
               .SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Saturday"));
            Cell cell18 = new Cell(1, 1)
                .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
                .SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(" Sunday "));

         

            Cell cell21 = new Cell(1, 1)
               .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY).SetBold()
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("08:00"));

            Cell cell22 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Dymista kapi"));

            Cell cell23 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Dymista kapi"));
            Cell cell24 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Dymista kapi"));

            Cell cell25 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Dymista kapi"));
            Cell cell26 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Dymista kapi"));
            Cell cell27 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Dymista kapi"));
            Cell cell28 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Dymista kapi"));

            Cell cell31 = new Cell(1, 1)
              .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
             .SetBold()
             .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
             .Add(new iText.Layout.Element.Paragraph("10:00"));
            Cell cell32 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));

            Cell cell33 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell34 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Cink  Imunoglukan"));

            Cell cell35 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell36 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Cink  Imunoglukan"));
            Cell cell37 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell38 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));


            Cell cell41 = new Cell(1, 1)
              .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
              .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE)
             .SetBold()
             .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
             .Add(new iText.Layout.Element.Paragraph("12:00"));
            Cell cell42 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));

            Cell cell43 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell44 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));

            Cell cell45 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell46 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell47 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Vitamin C"));
            Cell cell48 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Vitamin C"));

            Cell cell51 = new Cell(1, 1)
             .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
              .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE)
            .SetBold()
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
            .Add(new iText.Layout.Element.Paragraph("14:00"));
            Cell cell52 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));

            Cell cell53 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Hepatoprotect   "));
            Cell cell54 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));

            Cell cell55 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Hepatoprotect   "));
            Cell cell56 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell57 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Hepatoprotect   "));
            Cell cell58 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            
            Cell cell61 = new Cell(1, 1)
              .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
              .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE)
             .SetBold()
             .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
             .Add(new iText.Layout.Element.Paragraph("16:00"));
            Cell cell62 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Magnezijum      "));

            Cell cell63 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell64 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Magnezijum      "));

            Cell cell65 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell66 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph("Magnezijum      "));
            Cell cell67 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell68 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));

            Cell cell71 = new Cell(1, 1)
             .SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY)
             .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.MIDDLE)
            .SetBold()
            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
            .Add(new iText.Layout.Element.Paragraph("18:00"));
            Cell cell72 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));

            Cell cell73 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell74 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));

            Cell cell75 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell76 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell77 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));
            Cell cell78 = new Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new iText.Layout.Element.Paragraph(""));


            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell13);
            table.AddCell(cell14);
            table.AddCell(cell15);
            table.AddCell(cell16);
            table.AddCell(cell17);
            table.AddCell(cell18);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell23);
            table.AddCell(cell24);
            table.AddCell(cell25);
            table.AddCell(cell26);
            table.AddCell(cell27);
            table.AddCell(cell28);
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell33);
            table.AddCell(cell34);
            table.AddCell(cell35);
            table.AddCell(cell36);
            table.AddCell(cell37);
            table.AddCell(cell38);
            table.AddCell(cell41);
            table.AddCell(cell42);
            table.AddCell(cell43);
            table.AddCell(cell44);
            table.AddCell(cell45);
            table.AddCell(cell46);
            table.AddCell(cell47);
            table.AddCell(cell48);
            table.AddCell(cell51);
            table.AddCell(cell52);
            table.AddCell(cell53);
            table.AddCell(cell54);
            table.AddCell(cell55);
            table.AddCell(cell56);
            table.AddCell(cell57);
            table.AddCell(cell58);
            table.AddCell(cell61);
            table.AddCell(cell62);
            table.AddCell(cell63);
            table.AddCell(cell64);
            table.AddCell(cell65);
            table.AddCell(cell66);
            table.AddCell(cell67);
            table.AddCell(cell68);
            table.AddCell(cell71);
            table.AddCell(cell72);
            table.AddCell(cell73);
            table.AddCell(cell74);
            table.AddCell(cell75);
            table.AddCell(cell76);
            table.AddCell(cell77);
            table.AddCell(cell78);

            document.Add(table);



            document.Close();

            pdfTB.Text = "Your PDF is ready!";
            


        }
    }
}
