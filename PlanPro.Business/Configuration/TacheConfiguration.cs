using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanPro.Entities;

namespace PlanPro.Business.Configuration
{
    class TacheConfiguration : IEntityTypeConfiguration<Tache>
    {
        public void Configure(EntityTypeBuilder<Tache> builder)
        {
            builder
                .HasKey(a => a.ID);

            builder
                .Property(m => m.ID)
                .UseIdentityColumn();

            builder.HasOne(p => p.Projet).WithMany(x => x.Tasks);

            builder.HasOne(p => p.Realisateur).WithMany(x=>x.Tasks);

            builder.HasOne(z => z.Creator).WithMany(x => x.SupervisorTasks);


            builder
                .ToTable("Taches");
        }
    }
}
