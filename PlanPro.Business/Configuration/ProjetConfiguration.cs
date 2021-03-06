using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlanPro.Entities;

namespace PlanPro.Business.Configuration
{
    class ProjetConfiguration : IEntityTypeConfiguration<Projet>
    {
        public void Configure(EntityTypeBuilder<Projet> builder)
        {
            builder
                .HasKey(a => a.ID);

            builder
                .Property(m => m.ID)
                .UseIdentityColumn();

            //builder.HasMany(p => p.Tasks);

            builder.HasMany(p => p.Participants)
                .WithOne(x => x.PaticipProject);

            builder.HasOne(p => p.ChefProjet)
                .WithMany(a => a.ChefProjects);

            builder
                .ToTable("Projets");
        }
    }
}
