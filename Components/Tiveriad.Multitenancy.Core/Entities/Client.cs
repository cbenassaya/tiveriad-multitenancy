using System.ComponentModel.DataAnnotations;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Core.Entities;

public class Client : IEntity<string>, IAuditable<string>
{
    [MaxLength(24)]
    public string Id { get; set; }

    public string Name { get; set; }
    
    public string Description { get; set; }

    public string CreatedBy { get; set; }

    public DateTime Created { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }
    
}