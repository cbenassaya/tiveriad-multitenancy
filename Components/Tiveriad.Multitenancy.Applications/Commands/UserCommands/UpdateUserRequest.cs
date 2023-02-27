using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;

public record UpdateUserRequest(User User) : IRequest<User>,ICommandRequest;