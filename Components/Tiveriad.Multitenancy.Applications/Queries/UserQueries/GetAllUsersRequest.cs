using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

public record GetAllUsersRequest() : IRequest<IEnumerable<User>>;