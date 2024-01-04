using MediatR;
using Microsoft.EntityFrameworkCore;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Multitenancy.Core.Exceptions;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserRoleClientMappingCommands;

public class UpdateUserRoleClientMappingRequestHandler : IRequestHandler<UpdateMembershipRoleClientMappingRequest, MembershipRoleClientMapping>
{
    private readonly IRepository<MembershipRoleClientMapping, string> _repository;
    private readonly IRepository<Membership, string> _membershipRepository;
    private readonly IRepository<Role, string> _roleRepository;
    private readonly IRepository<Client, string> _clientRepository;
    private readonly IDomainEventStore _store;
    public UpdateUserRoleClientMappingRequestHandler( IRepository<Membership, string> membershipRepository, IRepository<MembershipRoleClientMapping, string> repository, IDomainEventStore store, IRepository<Role, string> roleRepository, IRepository<Client, string> clientRepository)
    {
        _membershipRepository = membershipRepository;
        _roleRepository = roleRepository;
        _clientRepository = clientRepository;
        _repository = repository;
        _store = store;
    }

    public Task<MembershipRoleClientMapping> Handle(UpdateMembershipRoleClientMappingRequest request, CancellationToken cancellationToken)
    {
        var query = _repository.Queryable.Include(x => x.Membership).Where(x => x.Id == request.MembershipRoleClientMapping.Id);
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
                result.Membership = (request.MembershipRoleClientMapping.Membership != null) ? await _membershipRepository.GetByIdAsync(request.MembershipRoleClientMapping.Membership.Id, cancellationToken) : null;
                result.Role = (request.MembershipRoleClientMapping.Role != null) ? await _roleRepository.GetByIdAsync(request.MembershipRoleClientMapping.Role.Id, cancellationToken) : null;
                result.Client = (request.MembershipRoleClientMapping.Client != null) ? await _clientRepository.GetByIdAsync(request.MembershipRoleClientMapping.Client.Id, cancellationToken) : null;
                _store.Add<UserRoleClientMappingDomainEvent,string>( new UserRoleClientMappingDomainEvent() {MembershipRoleClientMapping = result, EventType = "UPDATE"});
                return result;
            }
            //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}