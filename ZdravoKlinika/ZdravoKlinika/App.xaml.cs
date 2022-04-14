﻿using System;
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
                patientController.Create("Veljko", "Todorovic", "1231231231231", "todorovicveljko1", "123", "todorovicveljko1@gmail.com", "Srbija", "Kraljevo", "Todorovica  38", Gender.Male, BloodType.A_Pos, new List<string>());
            } catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
            RoomRepository roomRepository = new RoomRepository(@"..\..\..\Resource\Data\room.json");
            RoomService roomService = new RoomService(roomRepository);
            RoomController roomController = new RoomController(roomService);

            roomController.Create("5", "Soba", "Soba za operaciju", "Soba za operaciju");

        }
    }
}