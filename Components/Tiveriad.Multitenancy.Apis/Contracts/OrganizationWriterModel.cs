using System;
using System.ComponentModel.DataAnnotations;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.Contracts;
public class OrganizationWriterModel
{
    public string? Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }
}