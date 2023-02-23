using MediatR;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;
public class SaveOrUpdateOrganizationRequestHandler : IRequestHandler<SaveOrUpdateOrganizationRequest, Organization>
{
    private readonly IRepository<Organization, string> _organizationRepository;
    private readonly IDomainEventStore _store;
    public SaveOrUpdateOrganizationRequestHandler(IRepository<Organization, string> organizationRepository, IDomainEventStore store)
    {
        _organizationRepository = organizationRepository;
        _store = store;
    }

    public Task<Organization> Handle(SaveOrUpdateOrganizationRequest request, CancellationToken cancellationToken)
    {
        var query = _organizationRepository.Queryable.Where(x => x.Id == request.Organization.Id);
        return Task.Run(async () =>
        {
            //<-- START CUSTOM CODE-->
            var result = query.ToList().FirstOrDefault();
            if (result == null)
            {
                await _organizationRepository.AddOneAsync(request.Organization, cancellationToken);
                _store.Add<OrganizationDomainEvent,string>( new OrganizationDomainEvent() {Organization = request.Organization, EventType = "INSERT"});
                return request.Organization;
            }
            else
            {
                result.Name = request.Organization.Name;
                result.Description = request.Organization.Description;
                result.State = request.Organization.State;
                result.CreatedBy = request.Organization.CreatedBy;
                result.Created = request.Organization.Created;
                result.LastModifiedBy = request.Organization.LastModifiedBy;
                result.LastModified = request.Organization.LastModified;
                _store.Add<OrganizationDomainEvent,string>( new OrganizationDomainEvent() {Organization = result, EventType = "UPDATE"});
                return result;
            }
        //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}