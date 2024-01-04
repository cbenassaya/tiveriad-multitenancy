using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Persistence.Entities;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("T_Client");
        // <-- Id -->
        builder.HasKey(b => b.Id).HasName("PK_ClientId");
        // <-- ManyToOne -->
        // <-- OneToMany -->
        // <-- Enum -->

        // <-- Object -->
    }
}