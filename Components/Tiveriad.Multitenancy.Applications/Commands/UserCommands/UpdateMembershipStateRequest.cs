using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;

public record UpdateMembershipStateRequest(string OrganizationId, string userId , MembershipState state) : IRequest<User>,ICommandRequest;