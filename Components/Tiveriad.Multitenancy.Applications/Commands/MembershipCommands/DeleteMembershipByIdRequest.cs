using MediatR;

namespace Tiveriad.Multitenancy.Applications.Commands.MembershipCommands;

public record DeleteMembershipByIdRequest(string Id) : IRequest<bool>,ICommandRequest;