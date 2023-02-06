using MediatR;
using Tiveriad.Multitenancy.Application.Commands;
using Tiveriad.Multitenancy.Core.Entities;

public record SaveOrUpdateUserRequest(User User) : IRequest<User>,ICommandRequest;