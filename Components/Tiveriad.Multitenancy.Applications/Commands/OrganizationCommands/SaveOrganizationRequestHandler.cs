using MediatR;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;
public class SaveOrganizationRequestHandler : IRequestHandler<SaveOrganizationRequest, Organization>
{
    private readonly IRepository<Organization, string> _organizationRepository;
    private readonly IDomainEventStore _store;
    public SaveOrganizationRequestHandler(IRepository<Organization, string> organizationRepository, IDomainEventStore store)
    {
        _organizationRepository = organizationRepository;
        _store = store;
    }

    public Task<Organization> Handle(SaveOrganizationRequest request, CancellationToken cancellationToken)
    {
        var query = _organizationRepository.Queryable.Where(x => x.Id == request.Organization.Id);
        return Task.Run(async () =>
        {
            //<-- START CUSTOM CODE-->
            await _organizationRepository.AddOneAsync(request.Organization, cancellationToken);
            _store.Add<OrganizationDomainEvent,string>( new OrganizationDomainEvent() {Organization = request.Organization, EventType = "INSERT"});
            return request.Organization;
            //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}