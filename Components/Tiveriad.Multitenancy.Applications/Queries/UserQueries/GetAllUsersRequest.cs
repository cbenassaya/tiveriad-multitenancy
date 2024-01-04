using MediatR;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Applications.Queries.UserQueries;

public record GetAllUsersRequest(
    string OrganizationId,
    string? Id,
    string? Email,   string? Username,
    string? Firstname,   string? Lastname, 
    string[]? States,
    int? Page,  int? Limit,
    string? Q,  string[]? Orders
    ) : IRequest<IEnumerable<User>>;
    
    
    
