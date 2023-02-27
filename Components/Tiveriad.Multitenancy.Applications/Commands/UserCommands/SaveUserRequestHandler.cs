using MediatR;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;
public class SaveUserRequestHandler : IRequestHandler<SaveUserRequest, User>
{
    private readonly IRepository<User, string> _userRepository;
    private readonly IDomainEventStore _store;
    public SaveUserRequestHandler(IRepository<User, string> userRepository, IDomainEventStore store)
    {
        _userRepository = userRepository;
        _store = store;
    }

    public Task<User> Handle(SaveUserRequest request, CancellationToken cancellationToken)
    {
        var query = _userRepository.Queryable.Where(x => x.Id == request.User.Id);
        return Task.Run(async () =>
        {
            //<-- START CUSTOM CODE-->
            var result = query.ToList().FirstOrDefault();
            if (result == null)
            {
                await _userRepository.AddOneAsync(request.User, cancellationToken);
                _store.Add<UserDomainEvent,string>( new UserDomainEvent() {User = request.User, EventType = "INSERT"});
                return request.User;
            }
            else
            {
                result.Email = request.User.Email;
                result.Firstname = request.User.Firstname;
                result.Lastname = request.User.Lastname;
                result.Description = request.User.Description;
                result.State = request.User.State;
                result.CreatedBy = request.User.CreatedBy;
                result.Created = request.User.Created;
                result.LastModifiedBy = request.User.LastModifiedBy;
                result.LastModified = request.User.LastModified;
                _store.Add<UserDomainEvent,string>( new UserDomainEvent() {User = result, EventType = "UPDATE"});
                return result;
            }
        //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}