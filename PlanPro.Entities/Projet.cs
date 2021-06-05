using PlanPro.Entities.Models;
using System;
using System.Collections.Generic;

namespace PlanPro.Entities
{
    public class Projet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public List<Tache> Tasks { get; set; }
        public string ChefProjetID { get; set; }
        public ApplicationUser ChefProjet { get; set; }
        public List<ApplicationUser> Participants { get; set; }
    }
}
