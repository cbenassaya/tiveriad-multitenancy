using MediatR;
using Tiveriad.EnterpriseIntegrationPatterns.EventBrokers;
using Tiveriad.Multitenancy.Core.DomainEvents;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;
public class SaveUserRequestHandler : IRequestHandler<SaveUserRequest, User>
{
    private readonly IRepository<User, string> _userRepository;
    private readonly IRepository<Membership,string> _membershipRepository;
    private readonly IRepository<Organization,string> _organizationRepository;
    
    private readonly IDomainEventStore _store;
    public SaveUserRequestHandler(IRepository<User, string> userRepository, IDomainEventStore store, IRepository<Membership, string> membershipRepository, IRepository<Organization, string> organizationRepository)
    {
        _userRepository = userRepository;
        _store = store;
        _membershipRepository = membershipRepository;
        _organizationRepository = organizationRepository;
    }

    public Task<User> Handle(SaveUserRequest request, CancellationToken cancellationToken)
    {
        return Task.Run(async () =>
        {
            var organization = await _organizationRepository.GetByIdAsync(request.OrganizationId,cancellationToken);
            //<-- START CUSTOM CODE-->
            var user =  await _userRepository.GetByIdAsync(request.User.Id, cancellationToken);
            if (user == null)
            {
                user = request.User;
                await _userRepository.AddOneAsync(user, cancellationToken);
            }
            else
            {
                user.Email = request.User.Email;
                user.Firstname = request.User.Firstname;
                user.Lastname = request.User.Lastname;
                user.Description = request.User.Description;
                user.Username = request.User.Username;
            }
            var membership = new Membership()
            {
                Organization = organization,
                User = user,
                State = MembershipState.Pending
            };
            await _membershipRepository.AddOneAsync(membership, cancellationToken);
            _store.Add<MembershipDomainEvent, string>(new MembershipDomainEvent() { Membership = membership, EventType = "SAVE" });
            return user;
            //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}