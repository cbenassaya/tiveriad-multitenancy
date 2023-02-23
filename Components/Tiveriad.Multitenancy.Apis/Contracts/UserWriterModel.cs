using System;
using System.ComponentModel.DataAnnotations;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.Contracts;
public class UserWriterModel
{
    public string? Id { get; set; }

    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Firstname { get; set; }

    [Required]
    public string? Lastname { get; set; }

    public string? Description { get; set; }
}