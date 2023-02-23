using System.ComponentModel.DataAnnotations;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Core.Entities;
public class Organization : IEntity<string>, IAuditable<string>
{
    [MaxLength(24)]
    public string Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public OrganizationState? State { get; set; }

    public string CreatedBy { get; set; }

    public DateTime Created { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }
}