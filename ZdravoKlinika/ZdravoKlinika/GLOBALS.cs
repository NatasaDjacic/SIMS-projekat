using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Service;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika
{
    public static class GLOBALS
    {
        public static AppointmentRepository appointmentRepository = new AppointmentRepository(@"..\..\..\Resource\Data\appointment.json");
        public static PatientRepository patientRepository = new PatientRepository(@"..\..\..\Resource\Data\patient.json");
        public static DoctorRepository doctorRepository = new DoctorRepository(@"..\..\..\Resource\Data\doctor.json");
        public static DrugRepository drugRepository = new DrugRepository(@"..\..\..\Resource\Data\drug.json");
        public static EquipmentRepository equipmentRepository = new EquipmentRepository(@"..\..\..\Resource\Data\equipment.json");
        public static ManagerRepository managerRepository = new ManagerRepository(@"..\..\..\Resource\Data\manager.json");
        public static RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
        public static SecretaryRepository secretaryRepository = new SecretaryRepository(@"..\..\..\Resource\Data\secretary.json");
        public static NotificationRepository notificationRepository = new NotificationRepository(@"..\..\..\Resource\Data\notification.json");
        public static RenovationRepository renovationRepository = new RenovationRepository(@"..\..\..\Resource\Data\renovation.json");
        public static EquipMovingRepository equipMovingRepository = new EquipMovingRepository(@"..\..\..\Resource\Data\equipMoving.json");
        public static OrderEquipmentRepository orderEquipmentRepository = new OrderEquipmentRepository(@"..\..\..\Resource\Data\order_equipment.json");

        public static AuthService authService = new AuthService(patientRepository, doctorRepository, managerRepository, secretaryRepository);
        public static DoctorService doctorService = new DoctorService(doctorRepository);
        public static AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorService);
        public static EquipmentService equipmentService =  new EquipmentService(equipmentRepository);
        public static PatientService patientService = new PatientService(patientRepository);
        public static RoomService roomService = new RoomService(roomRepository);
        public static NotificationService notificationService = new NotificationService(notificationRepository, authService);
        public static RenovationService renovationService = new RenovationService(renovationRepository);
        public static SuggestionService suggestionService = new SuggestionService(appointmentService, doctorService, renovationService);
        public static EquipMovingService equipMovingService = new EquipMovingService(equipMovingRepository, roomService, equipmentService);
        public static ReportService reportService = new ReportService(patientService);
        public static PrescriptionService prescriptionService = new PrescriptionService(patientService);
        public static OrderEquipmentService orderEquipmentService = new OrderEquipmentService(orderEquipmentRepository, equipmentRepository);

        public static PatientController patientController = new PatientController(patientService);
        public static DoctorController doctorController = new DoctorController(doctorService);
        public static AppointmentController appointmentController = new AppointmentController(appointmentService, doctorService, authService, suggestionService, notificationService);
        public static EquipmentController equipmentController = new EquipmentController(equipmentService);
        public static RoomController roomController = new RoomController(roomService);
        public static AuthController authController = new AuthController(authService);
        public static NotificationController notificationController = new NotificationController(notificationService);
        public static SuggestionController suggestionController = new SuggestionController(suggestionService);
        public static EquipMovingController equipMovingController = new EquipMovingController(equipMovingService, roomService);
        public static RenovationController renovationController = new RenovationController(renovationService);
        public static ReportController reportController = new ReportController(reportService);
        public static PrescriptionController prescriptionController = new PrescriptionController(prescriptionService);
        public static OrderEquipmentController orderEquipmentController = new OrderEquipmentController(orderEquipmentService);
    }
}
