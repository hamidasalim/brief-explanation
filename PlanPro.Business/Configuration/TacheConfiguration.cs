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

            builder.HasOne(p => p.Projet);

            builder.HasOne(p => p.Realisateur)
                 .WithMany(a => a.Tasks);

            builder
                .ToTable("Taches");
        }
    }
}
