using MediatR;
using Tiveriad.Multitenancy.Application.Queries;
using Tiveriad.Multitenancy.Core.Entities;

public record GetMembershipByIdRequest(string Id) : IRequest<Membership>, IQueryRequest;