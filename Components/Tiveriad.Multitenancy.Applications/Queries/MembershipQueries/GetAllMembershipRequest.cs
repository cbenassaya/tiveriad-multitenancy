using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Queries.MembershipQueries;

public record GetAllMembershipRequest() : IRequest<IEnumerable<Membership>>, IQueryRequest;