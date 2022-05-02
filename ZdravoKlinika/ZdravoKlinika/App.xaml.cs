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

namespace ZdravoKlinika
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            // AUTO LOGIN::
            //GLOBALS.authController.Login("secretary", "zdravo");
            // AUTO LOGIN MANAGER::
            GLOBALS.authController.Login("manager", "zdravo");

            var ge = EquipRoomGroupDTO.groupEquip(GLOBALS.equipmentController.GetAll());
            ge.ForEach(e => {
                Console.WriteLine(String.Format("{0} {1} {2}", e.roomId, e.name, e.equipIds.Count));
            });
        }
    }
}
 