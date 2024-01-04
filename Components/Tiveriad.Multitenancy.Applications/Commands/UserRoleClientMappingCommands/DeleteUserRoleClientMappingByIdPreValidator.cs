using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserRoleClientMappingCommands;
public class DeleteUserRoleClientMappingByIdPreValidator : AbstractValidator<DeleteUserRoleClientMappingByIdRequest>
{
    private IRepository<MembershipRoleClientMapping, string> _userRoleClientMappingRepository;
    private IRepository<User, string> _userRepository;
    public DeleteUserRoleClientMappingByIdPreValidator(IRepository<MembershipRoleClientMapping, string> userRoleClientMappingRepository, IRepository<User, string> userRepository)
    {
        _userRoleClientMappingRepository = userRoleClientMappingRepository;
        _userRepository = userRepository;
    }
}