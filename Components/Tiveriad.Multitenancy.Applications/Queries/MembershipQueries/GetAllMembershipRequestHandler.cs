using MediatR;
using Microsoft.EntityFrameworkCore;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Application.Queries.MembershipQueries;
public class GetAllMembershipRequestHandler : IRequestHandler<GetAllMembershipRequest, IEnumerable<Membership>>
{
    private readonly IRepository<Membership, string> _membershipRepository;
    private readonly IRepository<User, string> _userRepository;
    public GetAllMembershipRequestHandler(IRepository<Membership, string> membershipRepository, IRepository<User, string> userRepository)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
    }

    public Task<IEnumerable<Membership>> Handle(GetAllMembershipRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var query = _membershipRepository.Queryable.Include(x => x.User).Include(x => x.Organization);
        return Task.Run(() => query.ToList().AsEnumerable(), cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}