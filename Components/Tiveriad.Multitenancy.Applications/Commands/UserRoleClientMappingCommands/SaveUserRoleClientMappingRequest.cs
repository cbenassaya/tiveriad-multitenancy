using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Commands.UserRoleClientMappingCommands;

public record SaveUserRoleClientMappingRequest(MembershipRoleClientMapping MembershipRoleClientMapping) : IRequest<MembershipRoleClientMapping>,ICommandRequest;