using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserCommands;
public class DeleteUserByIdPreValidator : AbstractValidator<DeleteUserByIdRequest>
{
    private IRepository<User, string> _userRepository;
    public DeleteUserByIdPreValidator(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }
}