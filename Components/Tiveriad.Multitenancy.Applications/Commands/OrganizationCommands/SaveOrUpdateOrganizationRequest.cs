using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;

public record SaveOrUpdateOrganizationRequest(Organization Organization) : IRequest<Organization>,ICommandRequest;