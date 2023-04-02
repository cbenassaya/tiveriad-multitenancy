using System.ComponentModel.DataAnnotations;

namespace Tiveriad.Multitenancy.Apis.Contracts;
public class UserWriterModel
{
    public string? Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string Firstname { get; set; }

    [Required]
    [MaxLength(50)]
    public string Lastname { get; set; }

    public string? Description { get; set; }
}