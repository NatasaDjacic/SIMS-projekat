﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.Model
{
    public class Report
    {
        public int reportId { get; set; }
        public string diagnostica { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public string patient_node { get; set; }
        public List<Prescription> prescriptions { get; set; }

        public Report(string diagnostica, string description, DateTime date)
        {
            this.diagnostica = diagnostica;
            this.description = description;
            this.date = date;
            this.patient_node = "";
            this.prescriptions = new List<Prescription>();

        }
    }
}