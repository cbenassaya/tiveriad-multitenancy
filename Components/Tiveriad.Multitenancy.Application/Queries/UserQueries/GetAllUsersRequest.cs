using MediatR;
using System.Collections.Generic;
using Tiveriad.Multitenancy.Core.Entities;

public record GetAllUsersRequest() : IRequest<IEnumerable<User>>;