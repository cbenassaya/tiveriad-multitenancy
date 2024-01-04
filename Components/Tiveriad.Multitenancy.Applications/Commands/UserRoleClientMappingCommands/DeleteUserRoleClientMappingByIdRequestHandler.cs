using MediatR;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserRoleClientMappingCommands;
public class DeleteUserRoleClientMappingByIdRequestHandler : IRequestHandler<DeleteUserRoleClientMappingByIdRequest, bool>
{
    private readonly IRepository<MembershipRoleClientMapping, string> _userRoleClientMappingRepository;
    private readonly IRepository<User, string> _userRepository;
    private readonly IDomainEventStore _store;
    public DeleteUserRoleClientMappingByIdRequestHandler(IRepository<MembershipRoleClientMapping, string> userRoleClientMappingRepository, IRepository<User, string> userRepository, IDomainEventStore store)
    {
        _userRoleClientMappingRepository = userRoleClientMappingRepository;
        _userRepository = userRepository;
        _store = store;
    }

    public Task<bool> Handle(DeleteUserRoleClientMappingByIdRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        return Task.Run(() =>
        {
            var userRoleClientMapping = _userRoleClientMappingRepository.GetById(request.Id);
            var result =  _userRoleClientMappingRepository.DeleteOne(userRoleClientMapping) == 1;
            if (result)
                _store.Add<UserRoleClientMappingDomainEvent,string>( new UserRoleClientMappingDomainEvent() {MembershipRoleClientMapping = userRoleClientMapping, EventType = "DELETE"});
            return result;
        }, cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}