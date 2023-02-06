using Microsoft.EntityFrameworkCore;
using Tiveriad.Multitenancy.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        builder.Property(e => e.State).HasConversion(v => v.ToString(), v => (UserState)Enum.Parse(typeof(UserState), v));
    // <-- Object -->
    }
}