using MediatR;
using Tiveriad.Multitenancy.Core.Entities;
using System;
using Tiveriad.Multitenancy.Application.Queries;

public record GetOrganizationByIdRequest(string Id) : IRequest<Organization>, IQueryRequest;