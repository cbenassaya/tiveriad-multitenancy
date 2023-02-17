using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System.Threading.Tasks;
using System.Threading;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;

namespace Tiveriad.Multitenancy.Application.Commands.MembershipCommands;
public class DeleteMembershipByIdRequestHandler : IRequestHandler<DeleteMembershipByIdRequest, bool>
{
    private readonly IRepository<Membership, string> _membershipRepository;
    private readonly IRepository<User, string> _userRepository;
    private readonly IDomainEventStore _store;
    public DeleteMembershipByIdRequestHandler(IRepository<Membership, string> membershipRepository, IRepository<User, string> userRepository, IDomainEventStore store)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
        _store = store;
    }

    public Task<bool> Handle(DeleteMembershipByIdRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        return Task.Run(() =>
        {
            var membership = _membershipRepository.GetById(request.Id);
            var result =  _membershipRepository.DeleteOne(membership) == 1;
            if (result)
                _store.Add<MembershipDomainEvent,string>( new MembershipDomainEvent() {Membership = membership, EventType = "DELETE"});
            return result;
        }, cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}