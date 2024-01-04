using System.ComponentModel.DataAnnotations;

namespace Tiveriad.Multitenancy.Apis.Contracts;
public class OrganizationWriterModel
{
    public string? Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Required] public UserWriterModel Owner { get; set; } = new ();

}