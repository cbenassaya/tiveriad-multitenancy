using Microsoft.EntityFrameworkCore;
using Tiveriad.Multitenancy.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tiveriad.Multitenancy.Persistence.Entities;
public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.ToTable("T_Membership");
        // <-- Id -->
        builder.HasKey(b => b.Id).HasName("PK_MembershipId");
        // <-- ManyToOne -->
        builder.HasOne(b => b.User);
        builder.HasOne(b => b.Organization);
        // <-- OneToMany -->
        // <-- Enum -->
        builder.Property(e => e.State).HasConversion(v => v.ToString(), v => (MembershipState)Enum.Parse(typeof(MembershipState), v));
    // <-- Object -->
    }
}