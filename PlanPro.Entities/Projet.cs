using PlanPro.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public List<string> IdTasks { get; set; }
        public string ChefProjetID { get; set; }
        public ApplicationUser ChefProjet { get; set; }
        public List<ApplicationUser> Participants { get; set; }
        [NotMapped]
        public List<string> IdParticipants { get; set; }
    }
}
