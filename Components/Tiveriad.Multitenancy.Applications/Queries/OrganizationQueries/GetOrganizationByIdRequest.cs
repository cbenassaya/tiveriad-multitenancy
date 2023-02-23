using MediatR;
using Tiveriad.Multitenancy.Application.Queries;
using Tiveriad.Multitenancy.Core.Entities;

public record GetOrganizationByIdRequest(string Id) : IRequest<Organization>, IQueryRequest;