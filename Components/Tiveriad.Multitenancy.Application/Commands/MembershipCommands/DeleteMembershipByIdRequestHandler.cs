using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace Tiveriad.Multitenancy.Application.Commands.MembershipCommands;
public class DeleteMembershipByIdRequestHandler : IRequestHandler<DeleteMembershipByIdRequest, bool>
{
    private readonly IRepository<Membership, string> _membershipRepository;
    private readonly IRepository<User, string> _userRepository;
    public DeleteMembershipByIdRequestHandler(IRepository<Membership, string> membershipRepository, IRepository<User, string> userRepository)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
    }

    public Task<bool> Handle(DeleteMembershipByIdRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        return Task.Run(() => _membershipRepository.DeleteMany(x => x.Id == request.Id) == 1, cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}