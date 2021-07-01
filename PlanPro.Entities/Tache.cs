using PlanPro.Entities.Models;
using System;
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjetID { get; set; }
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        //add FK
        public Projet Projet { get; set; }
        public string RealisateurID { get; set; }
        public ApplicationUser Realisateur { get; set; }

    }
}
