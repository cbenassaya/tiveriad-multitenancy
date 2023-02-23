using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Queries.MembershipQueries;

public record GetMembershipByIdRequest(string Id) : IRequest<Membership>, IQueryRequest;