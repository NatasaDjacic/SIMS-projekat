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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;
using ZdravoKlinika.Model;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZdravoKlinika.UI.DoctorUI
{
    /// <summary>
    /// Interaction logic for DoctorMainWindow.xaml
    /// </summary>
    public partial class DoctorMainWindow : Window
    {

        public AppointmentController appointmentController;
        public DoctorMainWindow()
        {
            InitializeComponent();

            DoctorRepository doctorRepository = new DoctorRepository(@"..\..\..\Resource\Data\doctor.json");
            DoctorService doctorService = new DoctorService(doctorRepository);
            AppointmentRepository appointmentRepository = new AppointmentRepository(@"..\..\..\Resource\Data\appointment.json");
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorService);
            appointmentController = new AppointmentController(appointmentService, doctorService);
            string DOCTORJMBG = "1111111111111";
            string DOCTORROOM = "5";
            int choice = 0;
            int id = 0;
            Appointment app;
            do {
                Console.WriteLine("1. Appointments");
                Console.WriteLine("2. Create Appointments");
                Console.WriteLine("3. Move Appointments");
                Console.WriteLine("4. Delete Appointments");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) {
                    case 1:
                        var aps = appointmentController.GetDoctorAppointments();
                        foreach(var a in aps) {
                            Console.WriteLine("ID: "+ a.id.ToString());
                            Console.WriteLine("PatientJMBG: " + a.patientJMBG);
                            Console.WriteLine("RoomID: " + a.roomId);
                            Console.WriteLine("StartDateAndTime: " + a.startTime.ToString());
                            Console.WriteLine("Duration: " + a.duration.ToString());
                            Console.WriteLine("-------------------------");
                        }
                        break;
                    case 2:
                        app = new Appointment();
                        Console.WriteLine("PatientJMBG: ");
                        app.patientJMBG = Console.ReadLine() ?? "";
                        Console.WriteLine("DoctorJMBG(emtpy for my jmbg):");
                        app.doctorJMBG = Console.ReadLine() ?? DOCTORJMBG;
                        if (app.doctorJMBG.Trim().Equals("")) {
                            app.doctorJMBG = DOCTORJMBG;
                        }
                        if (!app.doctorJMBG.Equals(DOCTORJMBG)) { 
                            Console.WriteLine("RoomID: ");
                            app.roomId = Console.ReadLine();
                        } else {
                            app.roomId = DOCTORROOM;
                        }
                        Console.WriteLine("StartDateAndTime (mm/dd/yyyy hh:mm:ss): ");
                        app.startTime = DateTime.Parse(Console.ReadLine());
                        Console.WriteLine("Duration in mins:");
                        app.duration = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Is surgery (y/n)");
                        var s = Console.ReadLine();
                        if (s.Trim().Equals("y")) {
                            app.appointmentType = Model.Enums.AppointmentType.surgery;
                        } else {
                            app.appointmentType = Model.Enums.AppointmentType.regular;

                        }
                        try {
                            appointmentController.CreateAppointmentDoctor(app.startTime, app.duration, app.patientJMBG, app.doctorJMBG, app.roomId, false, app.appointmentType);
                        } catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter ID of appointment to move:");
                        id = Convert.ToInt32(Console.ReadLine());
                        if (!appointmentController.IsDoctorAppointment(id)) {
                            Console.WriteLine("You doesn't have access to this appointment.");
                            break;
                        }
                        app = appointmentController.GetAppointmentById(id);

                        Console.WriteLine("ID: " + app.id.ToString());
                        Console.WriteLine("PatientJMBG: " + app.patientJMBG);
                        Console.WriteLine("RoomID: " + app.roomId);
                        Console.WriteLine("StartDateAndTime: " + app.startTime.ToString());
                        Console.WriteLine("Duration: " + app.duration.ToString());
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("Enter start time and date to move (mm/dd/yyyy hh:mm:ss)");
                        var startTime = DateTime.Parse(Console.ReadLine());
                        if(appointmentController.MoveAppointmentById(id, startTime)) {
                            Console.WriteLine("Appointment moved;");
                        } else {
                            Console.WriteLine("Appointment is not moved beacuse date diff is more then 4days");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Enter ID of appointment to delete:");
                        id = Convert.ToInt32(Console.ReadLine());
                        if (appointmentController.IsDoctorAppointment(id)) {
                            appointmentController.DeleteAppointmentById(id);
                            Console.WriteLine("Appointment deleted.");
                        } else {
                            Console.WriteLine("You doesn't have access to this appointment.");
                            break;
                        }
                        break;
                }
            } while (true);

        }
    }
}
