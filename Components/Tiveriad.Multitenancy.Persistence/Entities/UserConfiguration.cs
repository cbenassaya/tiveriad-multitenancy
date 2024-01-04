using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Persistence.Entities;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("T_User");
        // <-- Id -->
        builder.HasKey(b => b.Id).HasName("PK_UserId");
        // <-- ManyToOne -->
        // <-- OneToMany -->
        // <-- Enum -->
        // <-- Object -->
    }
}