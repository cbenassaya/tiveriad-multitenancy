using MediatR;

namespace Tiveriad.Multitenancy.Applications.Commands.UserRoleClientMappingCommands;

public record DeleteUserRoleClientMappingByIdRequest(string Id) : IRequest<bool>,ICommandRequest;