using Microsoft.EntityFrameworkCore;
using PlanPro.Business.Configuration;
using PlanPro.Entities;

namespace PlanPro.Business
{
    public class PlanProDbContext : DbContext
    {
        public DbSet<Projet> Projets { get; set; }
        public DbSet<Tache> Taches { get; set; }

        public PlanProDbContext(DbContextOptions<PlanProDbContext> options)
       : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProjetConfiguration());
            builder.ApplyConfiguration(new TacheConfiguration());
        }
    }
}
