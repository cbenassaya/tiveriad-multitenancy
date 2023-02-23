using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Collections.Generic;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Tiveriad.Multitenancy.Application.Queries.UserQueries;
public class GetAllUsersRequestHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<User>>
{
    private readonly IRepository<User, string> _userRepository;
    public GetAllUsersRequestHandler(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<IEnumerable<User>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var query = _userRepository.Queryable;
        return Task.Run(() => query.ToList().AsEnumerable(), cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}