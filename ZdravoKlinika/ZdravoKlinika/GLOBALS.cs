using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Repository;
using ZdravoKlinika.Repository.Interfaces;
using ZdravoKlinika.Service;
using ZdravoKlinika.Controller;

namespace ZdravoKlinika
{
    public static class GLOBALS
    {
        // Repositories
        public static IAppointmentRepository appointmentRepository = new AppointmentRepository(@"..\..\..\Resource\Data\appointment.json");
        public static IPatientRepository patientRepository = new PatientRepository(@"..\..\..\Resource\Data\patient.json");
        public static IDoctorRepository doctorRepository = new DoctorRepository(@"..\..\..\Resource\Data\doctor.json");
        public static IDrugRepository drugRepository = new DrugRepository(@"..\..\..\Resource\Data\drug.json");
        public static IEquipmentRepository equipmentRepository = new EquipmentRepository(@"..\..\..\Resource\Data\equipment.json");
        public static IManagerRepository managerRepository = new ManagerRepository(@"..\..\..\Resource\Data\manager.json");
        public static IRoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
        public static ISecretaryRepository secretaryRepository = new SecretaryRepository(@"..\..\..\Resource\Data\secretary.json");
        public static INotificationRepository notificationRepository = new NotificationRepository(@"..\..\..\Resource\Data\notification.json");
        public static IRenovationRepository renovationRepository = new RenovationRepository(@"..\..\..\Resource\Data\renovation.json");
        public static IEquipMovingRepository equipMovingRepository = new EquipMovingRepository(@"..\..\..\Resource\Data\equipMoving.json");
        public static IOrderEquipmentRepository orderEquipmentRepository = new OrderEquipmentRepository(@"..\..\..\Resource\Data\order_equipment.json");
        public static IRoomSeparateRepository roomSeparateRepository = new RoomSeparateRepository(@"..\..\..\Resource\Data\roomSeparation.json");
        public static IRoomMergeRepository roomMergeRepository = new RoomMergeRepository(@"..\..\..\Resource\Data\roomMerge.json");
        public static ICancellationRepository cancellationRepository = new CancellationRepository(@"..\..\..\Resource\Data\cancellation.json");
        public static IMarkRepository markRepository = new MarkRepository(@"..\..\..\Resource\Data\mark.json");
        public static IHolidayRequestRepository holidayRequestRepository = new HolidayRequestRepository(@"..\..\..\Resource\Data\holiday_request.json");
        public static IMeetingRepository meetingRepository = new MeetingRepository(@"..\..\..\Resource\Data\meeting.json");
        public static IPatientReminderRepository patientReminderRepository = new PatientReminderRepository(@"..\..\..\Resource\Data\reminders.json");
        public static IDoctorsMarksRepository doctorsMarksRepository = new DoctorsMarksRepository(@"..\..\..\Resource\Data\doctorMarks.json");

        // Services
        public static PatientReminderService patientReminderService = new PatientReminderService(patientReminderRepository);
        public static MarkService markService = new MarkService(markRepository);
        public static CancellationService cancellationService = new CancellationService(cancellationRepository);
        public static AuthService authService = new AuthService(patientRepository, doctorRepository, managerRepository, secretaryRepository);
        public static DoctorService doctorService = new DoctorService(doctorRepository);
        public static AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorService, cancellationService, authService);
        public static EquipmentService equipmentService =  new EquipmentService(equipmentRepository);
        public static PatientService patientService = new PatientService(patientRepository);
        public static RoomService roomService = new RoomService(roomRepository);
        public static NotificationService notificationService = new NotificationService(notificationRepository, authService);
        public static RenovationService renovationService = new RenovationService(renovationRepository);
        public static EquipMovingService equipMovingService = new EquipMovingService(equipMovingRepository, roomService, equipmentService);
        public static ReportService reportService = new ReportService(patientService);
        public static PrescriptionService prescriptionService = new PrescriptionService(patientService);
        public static OrderEquipmentService orderEquipmentService = new OrderEquipmentService(orderEquipmentRepository, equipmentRepository);
        public static RoomSeparateService roomSeparateService = new RoomSeparateService(roomSeparateRepository);
        public static RoomMergeService roomMergeService = new RoomMergeService(roomMergeRepository);
        public static HolidayRequestService holidayRequestService = new HolidayRequestService(holidayRequestRepository);
        public static EmployeService employeService = new EmployeService(doctorRepository, managerRepository, secretaryRepository);
        public static MeetingService meetingService = new MeetingService(meetingRepository);

        public static SuggestionService suggestionService = new SuggestionService(appointmentService, doctorService, renovationService, holidayRequestService, meetingService, employeService);
        public static EmergencyAppointmentService emergencyAppointmentService = new EmergencyAppointmentService(suggestionService, appointmentService, roomService, doctorService);
        public static DoctorsMarksService doctorsMarksService = new DoctorsMarksService(doctorsMarksRepository, doctorRepository);

        // Controllers
        public static PatientReminderController patientReminderController = new PatientReminderController(patientReminderService);
        public static PatientController patientController = new PatientController(patientService);
        public static DoctorController doctorController = new DoctorController(doctorService);
        public static AppointmentController appointmentController = new AppointmentController(appointmentService, doctorService, authService, suggestionService, notificationService, emergencyAppointmentService);
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
        public static RoomSeparateController roomSeparateController = new RoomSeparateController(roomSeparateService);
        public static RoomMergeController roomMergeController = new RoomMergeController(roomMergeService);
        public static HolidayRequestController holidayRequestController = new HolidayRequestController(holidayRequestService);
        public static MeetingController meetingController = new MeetingController(meetingService, notificationService);
        public static EmployeController employeController = new EmployeController(employeService);

        public static DoctorMarksController doctorsMarksController = new DoctorMarksController(doctorsMarksService);
        
    }
}
