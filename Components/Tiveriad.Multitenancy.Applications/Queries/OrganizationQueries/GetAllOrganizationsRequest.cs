using MediatR;
using System.Collections.Generic;
using Tiveriad.Multitenancy.Application.Queries;
using Tiveriad.Multitenancy.Core.Entities;

public record GetAllOrganizationsRequest() : IRequest<IEnumerable<Organization>>, IQueryRequest;