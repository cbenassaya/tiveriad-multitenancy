using Microsoft.EntityFrameworkCore;
using MediatR;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Tiveriad.Multitenancy.Application.Commands.MembershipCommands;
public class SaveOrUpdateMembershipRequestHandler : IRequestHandler<SaveOrUpdateMembershipRequest, Membership>
{
    private readonly IRepository<Organization, string> _organizationRepository;
    private readonly IRepository<Membership, string> _repository;
    private readonly IRepository<User, string> _userRepository;
    public SaveOrUpdateMembershipRequestHandler(IRepository<Organization, string> organizationRepository, IRepository<User, string> userRepository, IRepository<Membership, string> repository)
    {
        _organizationRepository = organizationRepository;
        _userRepository = userRepository;
        _repository = repository;
    }

    public Task<Membership> Handle(SaveOrUpdateMembershipRequest request, CancellationToken cancellationToken)
    {
        var query = _repository.Queryable.Include(x => x.User).Include(x => x.Organization).Where(x => x.Id == request.Membership.Id);
        return Task.Run(async () =>
        {
            //<-- START CUSTOM CODE-->
            var result = query.ToList().FirstOrDefault();
            if (result == null)
            {
                await _repository.AddOneAsync(request.Membership, cancellationToken);
                return request.Membership;
            }
            else
            {
                result.State = request.Membership.State;
                result.CreatedBy = request.Membership.CreatedBy;
                result.Created = request.Membership.Created;
                result.LastModifiedBy = request.Membership.LastModifiedBy;
                result.LastModified = request.Membership.LastModified;
                result.User = (request.Membership.User != null) ? await _userRepository.GetByIdAsync(request.Membership.User.Id, cancellationToken) : null;
                result.Organization = (request.Membership.Organization != null) ? await _organizationRepository.GetByIdAsync(request.Membership.Organization.Id, cancellationToken) : null;
                return result;
            }
        //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}