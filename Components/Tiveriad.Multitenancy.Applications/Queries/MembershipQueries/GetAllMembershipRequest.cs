using MediatR;
using Tiveriad.Multitenancy.Application.Queries;
using Tiveriad.Multitenancy.Core.Entities;

public record GetAllMembershipRequest() : IRequest<IEnumerable<Membership>>, IQueryRequest;