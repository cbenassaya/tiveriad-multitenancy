using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;

public record SaveUserRequest(string OrganizationId, User User) : IRequest<User>,ICommandRequest;