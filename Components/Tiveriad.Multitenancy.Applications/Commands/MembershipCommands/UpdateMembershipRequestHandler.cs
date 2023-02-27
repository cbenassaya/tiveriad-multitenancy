using MediatR;
using Microsoft.EntityFrameworkCore;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Multitenancy.Core.Exceptions;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.MembershipCommands;

public class UpdateMembershipRequestHandler : IRequestHandler<UpdateMembershipRequest, Membership>
{
    private readonly IRepository<Organization, string> _organizationRepository;
    private readonly IRepository<Membership, string> _repository;
    private readonly IRepository<User, string> _userRepository;
    private readonly IDomainEventStore _store;
    public UpdateMembershipRequestHandler(IRepository<Organization, string> organizationRepository, IRepository<User, string> userRepository, IRepository<Membership, string> repository, IDomainEventStore store)
    {
        _organizationRepository = organizationRepository;
        _userRepository = userRepository;
        _repository = repository;
        _store = store;
    }

    public Task<Membership> Handle(UpdateMembershipRequest request, CancellationToken cancellationToken)
    {
        var query = _repository.Queryable.Include(x => x.User).Include(x => x.Organization).Where(x => x.Id == request.Membership.Id);
        return Task.Run(async () =>
        {
            //<-- START CUSTOM CODE-->
            var result = query.ToList().FirstOrDefault();
            if (result == null)
            {
                throw new MultiTenancyException(MultiTenancyError.BAD_REQUEST);
            }
            else
            {
                result.State = request.Membership.State;
                result.User = (request.Membership.User != null) ? await _userRepository.GetByIdAsync(request.Membership.User.Id, cancellationToken) : null;
                result.Organization = (request.Membership.Organization != null) ? await _organizationRepository.GetByIdAsync(request.Membership.Organization.Id, cancellationToken) : null;
                _store.Add<MembershipDomainEvent,string>( new MembershipDomainEvent() {Membership = result, EventType = "UPDATE"});
                return result;
            }
            //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}