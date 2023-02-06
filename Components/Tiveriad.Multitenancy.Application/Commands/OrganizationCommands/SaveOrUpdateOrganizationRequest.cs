using MediatR;
using Tiveriad.Multitenancy.Application.Commands;
using Tiveriad.Multitenancy.Core.Entities;

public record SaveOrUpdateOrganizationRequest(Organization Organization) : IRequest<Organization>,ICommandRequest;