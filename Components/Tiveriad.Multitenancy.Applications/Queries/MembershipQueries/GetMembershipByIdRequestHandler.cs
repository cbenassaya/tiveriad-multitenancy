using MediatR;
using Microsoft.EntityFrameworkCore;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Application.Queries.MembershipQueries;
public class GetMembershipByIdRequestHandler : IRequestHandler<GetMembershipByIdRequest, Membership>
{
    private readonly IRepository<Membership, string> _membershipRepository;
    private readonly IRepository<User, string> _userRepository;
    public GetMembershipByIdRequestHandler(IRepository<Membership, string> membershipRepository, IRepository<User, string> userRepository)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
    }

    public Task<Membership> Handle(GetMembershipByIdRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var query = _membershipRepository.Queryable.Include(x => x.User).Include(x => x.Organization).Where(x => x.Id == request.Id);
        //<-- END CUSTOM CODE-->
        return Task.Run(() => query.ToList().FirstOrDefault(), cancellationToken);
    }
}