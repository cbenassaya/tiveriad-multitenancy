using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Queries.UserQueries;

public record GetAllUsersRequest() : IRequest<IEnumerable<User>>;