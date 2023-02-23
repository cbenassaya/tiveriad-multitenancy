using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;

public record SaveOrUpdateUserRequest(User User) : IRequest<User>,ICommandRequest;