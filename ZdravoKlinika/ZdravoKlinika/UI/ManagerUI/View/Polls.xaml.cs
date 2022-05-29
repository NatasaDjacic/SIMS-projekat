﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ZdravoKlinika.Controller;
using ZdravoKlinika.Model;
using ZdravoKlinika.Model.DTO;
using ZdravoKlinika.Repository;

namespace ZdravoKlinika.UI.ManagerUI.View
{
    public partial class Polls : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private int doctorJMBG;
        private int ones;
        private int twos;
        private int threes;
        private int fours;
        private int fives;
        private double average;

        public int DoctorJMBG { get { return doctorJMBG; } set { doctorJMBG = value; OnPropertyChanged("DoctorJMBG"); } }
        public int Ones { get { return ones; } set { ones = value; OnPropertyChanged("Ones"); } }
        public int Twos { get { return twos; } set { twos = value; OnPropertyChanged("Twos"); } }
        public int Threes { get { return threes; } set { threes = value; OnPropertyChanged("Threes"); } }
        public int Fours { get { return fours; } set { fours = value; OnPropertyChanged("Fours"); } }
        public int Fives { get { return fives; } set { fives = value; OnPropertyChanged("Fives"); } }
        public double Average { get { return average; } set { average = value; OnPropertyChanged("Average"); } }

        private List<Mark> marks;
        private int sum = 0;
        private int number = 0;

        private ObservableCollection<DoctorsMarks> rooms;
        public ObservableCollection<DoctorsMarks> DoctorsPollsCollection
        {
            get => rooms;
            set
            {
                if (rooms != value)
                {
                    rooms = value;
                    OnPropertyChanged("DoctorsPollsCollection");
                }
            }
        }
        DoctorMarksController doctorMarksController = GLOBALS.doctorsMarksController;
        public Polls()
        {
            MarkRepository markRepository = new MarkRepository(@"..\..\..\Resource\Data\mark.json");
            DoctorsMarksRepository doctorsMarksRepository = new DoctorsMarksRepository(@"..\..\..\Resource\Data\doctorsMarks.json");
            doctorMarksController.calculateMarks();
            marks = markRepository.GetAll();
            foreach(Mark mark in marks)
            {
               
                number++;
                sum = sum + mark.hospitalMark;
                switch (mark.hospitalMark)
                {
                    case 1:
                        Ones++;
                        break;
                    case 2:
                        Twos++;
                        break;
                    case 3:
                        Threes++;
                        break;
                    case 4:
                        Fours++;
                        break;
                    case 5:
                        Fives++;
                        break;
                    default: throw new Exception("Not valid marks.");
                }
            }
            Average = sum/number;
            DoctorsPollsCollection = new ObservableCollection<DoctorsMarks>(doctorMarksController.GetAll());
            this.DataContext = this;
            InitializeComponent();
        }
    }
}
