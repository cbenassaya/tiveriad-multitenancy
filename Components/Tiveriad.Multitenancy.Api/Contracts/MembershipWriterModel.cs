using System;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.Contracts;
public class MembershipWriterModel
{
    public string? Id { get; set; }

    public MembershipState? State { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? Created { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public string UserId { get; set; }

    public string MembershipId { get; set; }
}