﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PlanPro.Entities
{
    public class Tache
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int ProjetID { get; set; }
        public Projet Projet { get; set; }
    }
}