using MediatR;
using Microsoft.EntityFrameworkCore;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.MembershipCommands;
public class SaveMembershipRequestHandler : IRequestHandler<SaveMembershipRequest, Membership>
{
    private readonly IRepository<Membership, string> _repository;
    private readonly IDomainEventStore _store;
    public SaveMembershipRequestHandler(IRepository<Membership, string> repository, IDomainEventStore store)
    {
        _repository = repository;
        _store = store;
    }

    public Task<Membership> Handle(SaveMembershipRequest request, CancellationToken cancellationToken)
    {
        var query = _repository.Queryable.Include(x => x.User).Include(x => x.Organization).Where(x => x.Id == request.Membership.Id);
        return Task.Run(async () =>
        {
            await _repository.AddOneAsync(request.Membership, cancellationToken);
            _store.Add<MembershipDomainEvent,string>( new MembershipDomainEvent() {Membership = request.Membership, EventType = "INSERT"});
            return request.Membership;
        //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}