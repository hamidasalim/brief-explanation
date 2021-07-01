using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PlanPro.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int IdPaticipProject { get; set; }
        public Projet PaticipProject { get; set; }
        public virtual List<Projet> ChefProjects { get; set; }
        public virtual List<Tache> Tasks { get; set; }
        public virtual List<Tache> SupervisorTasks { get; set; }
        public virtual List<Equipe> Teams { get; set; }
    }
}