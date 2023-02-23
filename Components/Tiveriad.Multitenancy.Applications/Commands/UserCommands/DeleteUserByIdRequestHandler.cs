using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System.Threading.Tasks;
using System.Threading;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;

namespace Tiveriad.Multitenancy.Application.Commands.UserCommands;
public class DeleteUserByIdRequestHandler : IRequestHandler<DeleteUserByIdRequest, bool>
{
    private readonly IRepository<User, string> _userRepository;
    private readonly IDomainEventStore _store;
    public DeleteUserByIdRequestHandler(IRepository<User, string> userRepository, IDomainEventStore store)
    {
        _userRepository = userRepository;
        _store = store;
    }

    public Task<bool> Handle(DeleteUserByIdRequest request, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        return Task.Run(() =>
        {
            var user = _userRepository.GetById(request.Id);
            var result =  _userRepository.DeleteOne(user) == 1;
            if (result)
                _store.Add<UserDomainEvent,string>( new UserDomainEvent() {User = user, EventType = "DELETE"});
            return result;
        }, cancellationToken);
    //<-- END CUSTOM CODE-->
    }
}