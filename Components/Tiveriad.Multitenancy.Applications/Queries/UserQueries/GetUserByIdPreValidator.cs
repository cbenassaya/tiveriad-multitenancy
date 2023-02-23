using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Queries.UserQueries;
public class GetUserByIdPreValidator : AbstractValidator<GetUserByIdRequest>
{
    private IRepository<User, string> _userRepository;
    public GetUserByIdPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}