using System.ComponentModel.DataAnnotations;

namespace Tiveriad.Multitenancy.Apis.Contracts;
public class OrganizationWriterModel
{
    public string? Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }
}