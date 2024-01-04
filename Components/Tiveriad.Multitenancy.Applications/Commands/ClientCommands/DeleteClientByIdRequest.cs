using MediatR;

namespace Tiveriad.Multitenancy.Applications.Commands.ClientCommands;

public record DeleteClientByIdRequest(string Id) : IRequest<bool>,ICommandRequest;