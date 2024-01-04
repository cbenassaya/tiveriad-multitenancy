using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Persistence.Entities;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("T_Role");
        // <-- Id -->
        builder.HasKey(b => b.Id).HasName("PK_RoleId");
        // <-- ManyToOne -->
        // <-- OneToMany -->
        builder.HasOne<Client>(m => m.Client);
        // <-- Enum -->

        // <-- Object -->
    }
}