using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Queries.OrganizationQueries;

public record GetAllOrganizationsRequest() : IRequest<IEnumerable<Organization>>, IQueryRequest;