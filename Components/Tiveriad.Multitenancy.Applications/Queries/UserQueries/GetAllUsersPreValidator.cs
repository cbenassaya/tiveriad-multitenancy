using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Application.Queries.UserQueries;
public class GetAllUsersPreValidator : AbstractValidator<GetAllUsersRequest>
{
    private IRepository<User, string> _userRepository;
    public GetAllUsersPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}