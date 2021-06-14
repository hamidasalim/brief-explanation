using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlanPro.Business.Configuration;
using PlanPro.Entities;

namespace PlanPro.Business
{
    public class PlanProDbContext : IdentityDbContext
    {
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Tache> Taches { get; set; }
        public DbSet<Equipe> Equipes { get; set; }

        public PlanProDbContext(DbContextOptions<PlanProDbContext> options)
       : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //other configs
            builder.ApplyConfiguration(new ProjetConfiguration());
            builder.ApplyConfiguration(new TacheConfiguration());
            builder.ApplyConfiguration(new EquipeConfiguration());
        }
    }
}
