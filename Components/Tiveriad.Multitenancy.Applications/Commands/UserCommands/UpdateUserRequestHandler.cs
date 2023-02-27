using MediatR;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Multitenancy.Core.Exceptions;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;

public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, User>
{
    private readonly IRepository<User, string> _userRepository;
    private readonly IDomainEventStore _store;
    public UpdateUserRequestHandler(IRepository<User, string> userRepository, IDomainEventStore store)
    {
        _userRepository = userRepository;
        _store = store;
    }

    public Task<User> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var query = _userRepository.Queryable.Where(x => x.Id == request.User.Id);
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
                result.Email = request.User.Email;
                result.Firstname = request.User.Firstname;
                result.Lastname = request.User.Lastname;
                result.Description = request.User.Description;
                result.State = request.User.State;
                _store.Add<UserDomainEvent,string>( new UserDomainEvent() {User = result, EventType = "UPDATE"});
                return result;
            }
            //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}