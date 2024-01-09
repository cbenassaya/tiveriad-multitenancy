using System.ComponentModel.DataAnnotations;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Core.Entities;
public class Organization : IEntity<string>, IAuditable<string>
{


    [MaxLength(24)] public string Id { get; set; } = string.Empty;

    [MaxLength(50)] public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }= string.Empty;

    public OrganizationState? State { get; set; } = OrganizationState.Pending;

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime Created { get; set; } = DateTime.Now;

    public string? LastModifiedBy { get; set; } = string.Empty;

    public DateTime? LastModified { get; set; }
    
    protected bool Equals(Organization other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Organization)obj);
    }

    public override int GetHashCode()
    {
        return string.IsNullOrEmpty(Id) ? 0: Id.GetHashCode();
    }

    public static bool operator ==(Organization? left, Organization? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Organization? left, Organization? right)
    {
        return !Equals(left, right);
    }
}