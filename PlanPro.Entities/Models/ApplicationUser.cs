using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PlanPro.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Projet> Projects { get; set; }
        public virtual List<Tache> Tasks { get; set; }
    }
}