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
            PatientRepository patientRepository = new PatientRepository(@"..\..\..\Resource\Data\patient.json");
            PatientService patientService = new PatientService(patientRepository);
            PatientController patientController = new PatientController(patientService);
            try {
                patientController.Create("Veljko", "Todorovic", "1231231231231", "todorovicveljko1", DateTime.Now, "123", "todorovicveljko1@gmail.com", "Srbija", "Kraljevo", "Todorovica  38", Gender.Male, BloodType.A_Pos, new List<string>());
            } catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            RoomController roomController = new RoomController(roomService);

            roomController.Create("5", "Soba", "Soba za operaciju", "Soba za operaciju");

            DoctorRepository doctorRepository = new DoctorRepository(@"..\..\..\Resource\Data\doctor.json");
            DoctorService doctorService = new DoctorService(doctorRepository);


            AppointmentRepository appointmentRepository = new AppointmentRepository(@"..\..\..\Resource\Data\appointment.json");
            AppointmentService appointmentService = new AppointmentService(appointmentRepository);

            DoctorController doctorController = new DoctorController(doctorService, appointmentService);
            AppointmentController appointmentController = new AppointmentController(appointmentService, doctorService);



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
            appointmentController.CreateAppointmentPatient(date, 30, "1111111111111");

            DateTime date1 = new DateTime(2022, 04, 26);
            appointmentController.MoveAppointmentById(1,date1);

        }
    }
}
