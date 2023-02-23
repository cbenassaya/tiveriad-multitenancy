using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

public record GetUserByIdRequest(string Id) : IRequest<User>;