using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Queries.UserQueries;

public record GetUserByIdRequest(string Id) : IRequest<User>;