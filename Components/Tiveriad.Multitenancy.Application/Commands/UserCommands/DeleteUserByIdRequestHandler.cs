using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Tiveriad.Multitenancy.Application.Commands.UserCommands;
public class DeleteUserByIdRequestHandler : IRequestHandler<DeleteUserByIdRequest, bool>
{
    private readonly IRepository<User, string> _userRepository;
    public DeleteUserByIdRequestHandler(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<bool> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        return Task.Run(() => _userRepository.DeleteMany(x => x.Id == request.Id) == 1, cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}