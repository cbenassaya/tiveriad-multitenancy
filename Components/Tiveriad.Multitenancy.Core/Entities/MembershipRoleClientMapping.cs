using System.ComponentModel.DataAnnotations;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Core.Entities;

public class MembershipRoleClientMapping : IEntity<string>, IAuditable<string>
{
    [MaxLength(24)]
    public string Id { get; set; }
    public Client Client { get; set; }
    
    public Membership Membership { get; set; }
    public Role Role { get; set; }
    
    public string CreatedBy { get; set; }

    public DateTime Created { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }

}