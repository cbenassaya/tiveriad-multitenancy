using Microsoft.EntityFrameworkCore;
using MediatR;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Tiveriad.Multitenancy.Application.Queries.UserQueries;
public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, User>
{
    private readonly IRepository<User, string> _userRepository;
    public GetUserByIdRequestHandler(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var query = _userRepository.Queryable.Where(x => x.Id == request.Id);
        //<-- END CUSTOM CODE-->
        return Task.Run(() => query.ToList().FirstOrDefault(), cancellationToken);
    }
}