using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Commands.MembershipCommands;

public record SaveMembershipRequest(Membership Membership) : IRequest<Membership>,ICommandRequest;