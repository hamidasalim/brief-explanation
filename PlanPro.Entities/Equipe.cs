using PlanPro.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanPro.Entities
{
    public class Equipe
    {
        public int ID { get; set; }
        public string IDChef { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<ApplicationUser> Members { get; set; }
        public List<string> IdMembers { get; set; }
    }
}
