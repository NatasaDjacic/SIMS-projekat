﻿using System;
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

        public static AuthService authService = new AuthService(patientRepository, doctorRepository, managerRepository, secretaryRepository);
        public static DoctorService doctorService = new DoctorService(doctorRepository);
        public static AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorService);
        public static EquipmentService equipmentService =  new EquipmentService(equipmentRepository);
        public static PatientService patientService = new PatientService(patientRepository);
        public static RoomService roomService = new RoomService(roomRepository);



        public static PatientController patientController = new PatientController(patientService);
        public static DoctorController doctorController = new DoctorController(doctorService);
        public static AppointmentController appointmentController = new AppointmentController(appointmentService, doctorService);
        public static EquipmentController equipmentController = new EquipmentController(equipmentService);
        public static RoomController roomController = new RoomController(roomService);
    }
}