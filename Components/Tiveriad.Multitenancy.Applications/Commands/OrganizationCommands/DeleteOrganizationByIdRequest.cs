using MediatR;

namespace Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;

public record DeleteOrganizationByIdRequest(string Id) : IRequest<bool>,ICommandRequest;