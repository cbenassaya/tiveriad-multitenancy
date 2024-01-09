using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Core.Entities;
public class User : IEntity<string>, IAuditable<string>
{
    [MaxLength(24)]
    public string Id { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(50)]
    public string Firstname { get; set; }

    [MaxLength(50)]
    public string Lastname { get; set; }
    
    [MaxLength(50)]
    public string Username { get; set; }
    
    [MaxLength(12)]
    [Required]
    [NotMapped]
    public string Password { get; set; }
    
    [MaxLength(50)]
    public string Locale { get; set; }

    [MaxLength(500)]
    [JsonIgnore]
    public string? Description { get; set; }

    [JsonIgnore]
    public string CreatedBy { get; set; }

    [JsonIgnore]
    public DateTime Created { get; set; }

    [JsonIgnore]
    public string? LastModifiedBy { get; set; }

    [JsonIgnore]
    public DateTime? LastModified { get; set; }
}