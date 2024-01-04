using MediatR;
using Microsoft.EntityFrameworkCore;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserRoleClientMappingCommands;
public class SaveUserRoleClientMappingRequestHandler : IRequestHandler<SaveUserRoleClientMappingRequest, MembershipRoleClientMapping>
{
    private readonly IRepository<MembershipRoleClientMapping, string> _repository;
    private readonly IDomainEventStore _store;
    public SaveUserRoleClientMappingRequestHandler(IRepository<MembershipRoleClientMapping, string> repository, IDomainEventStore store)
    {
        _repository = repository;
        _store = store;
    }

    public Task<MembershipRoleClientMapping> Handle(SaveUserRoleClientMappingRequest request, CancellationToken cancellationToken)
    {
        var query = _repository.Queryable.Include(x => x.Membership).Where(x => x.Id == request.MembershipRoleClientMapping.Id);
        return Task.Run(async () =>
        {
            await _repository.AddOneAsync(request.MembershipRoleClientMapping, cancellationToken);
            _store.Add<UserRoleClientMappingDomainEvent,string>( new UserRoleClientMappingDomainEvent() {MembershipRoleClientMapping = request.MembershipRoleClientMapping, EventType = "INSERT"});
            return request.MembershipRoleClientMapping;
        //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}