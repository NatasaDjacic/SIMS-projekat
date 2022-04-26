using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
            PatientController patientController = GLOBALS.patientController;
            try {
                patientController.Create("Veljko", "Todorovic", "1231231231231", "todorovicveljko1", DateTime.Now, "123", "todorovicveljko1@gmail.com", "Srbija", "Kraljevo", "Todorovica  38", Gender.Male, BloodType.A_Pos, new List<string>());
            } catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
            RoomController roomController = GLOBALS.roomController;

            roomController.Create("5", "Soba", "Soba za operaciju", "Soba za operaciju");

            EquipmentController equipmentController = GLOBALS.equipmentController;

            try
            {
                equipmentController.Create(7, "Stalak za infuziju", "10", "Složena", 100);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            DoctorController doctorController = GLOBALS.doctorController;
            AppointmentController appointmentController = GLOBALS.appointmentController;


            try
            {
                doctorController.Create("Mika", "Mikic", "3213213213213", "mikamikic", "321", "miki@gmail.com", "Srbija", "Kraljevo", "Todorovica  55", Gender.Male, "regular", "6");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                doctorController.Create("Ana", "Anic", "1111111111111", "anna", "111", "ana@gmail.com", "Srbija", "Beograd", "Nikole Pasica 4", Gender.Female, "regular", "5");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            DateTime date = new DateTime(2022, 04, 20);
            //appointmentController.CreateAppointmentPatient(date, 30, "1111111111111");

            DateTime date1 = new DateTime(2022, 04, 26);
            //appointmentController.MoveAppointmentById(1,date1);
            Console.WriteLine(new DateTime(2022, 04, 20,11,0,0));
            Console.WriteLine(new DateTime(2022, 04, 21,18,0,0));
            AuthService authService = GLOBALS.authService;
            Console.WriteLine(authService.login("mikamikic", "zdravo"));
            Console.WriteLine(authService.user_role);
            //appointmentService.GetAppointmentStartSuggestions("1231231231231", "1111111111111", "5", new DateTime(2022, 04, 20, 11, 0, 0), new DateTime(2022, 04, 20, 18, 0, 0), 30);
        }
    }
}
