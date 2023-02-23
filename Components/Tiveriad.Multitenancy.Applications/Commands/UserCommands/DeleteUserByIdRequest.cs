using MediatR;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;

public record DeleteUserByIdRequest(string Id) : IRequest<bool>,ICommandRequest;