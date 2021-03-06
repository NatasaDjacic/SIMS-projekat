using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKlinika.Model.DTO;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model.Enums;
using ZdravoKlinika.Model;
using System.Globalization;
using System.Threading;

namespace ZdravoKlinika
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            //Patient? p = GLOBALS.patientController.GetById("1231231231231"); 
            //Report? r = GLOBALS.reportController.AddReport(p, "Neka dijagnostika", "Neki opis", DateTime.Now);
            //GLOBALS.prescriptionController.AddPrescription(p, r.reportId, 1, "Neki opis za koriscenje", 7, 3, 1);



            // AUTO LOGIN::
            //GLOBALS.authController.Login("secretary", "zdravo");
            // AUTO LOGIN MANAGER::
            GLOBALS.authController.Login("1231231231231", "zdravo");
            /*
            var ge = EquipRoomGroupDTO.groupEquip(GLOBALS.equipmentController.GetAll());
            ge.ForEach(e => {
                Console.WriteLine(String.Format("{0} {1} {2}", e.roomId, e.name, e.equipIds.Count));
            });*/


        }
    }
}
 