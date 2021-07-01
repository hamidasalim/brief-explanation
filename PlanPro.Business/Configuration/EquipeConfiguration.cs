using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using PlanPro.Entities;
using System.Collections.Generic;

namespace PlanPro.Business.Configuration
{
    class EquipeConfiguration : IEntityTypeConfiguration<Equipe>
    {
        public void Configure(EntityTypeBuilder<Equipe> builder)
        {
            builder
                .HasKey(a => a.ID);

            builder
                .Property(m => m.ID)
                .UseIdentityColumn();

            builder.HasOne(l => l.Chef)
                .WithMany(n => n.Teams);

           // builder.HasMany(w => w.Members);
                
                    
                

            builder.Property(p => p.IdMembers)
                    .HasConversion(
                        v => JsonConvert.SerializeObject(v),
                        v => JsonConvert.DeserializeObject<List<string>>(v));

            builder
                .ToTable("Equipes");
        }
    }
}
