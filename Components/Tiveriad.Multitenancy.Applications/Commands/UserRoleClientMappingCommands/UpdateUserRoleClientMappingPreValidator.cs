using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.UserRoleClientMappingCommands;

public class UpdateUserRoleClientMappingPreValidator : AbstractValidator<UpdateMembershipRoleClientMappingRequest>
{
    private IRepository<MembershipRoleClientMapping, string> _userRoleClientMappingRepository;
    private IRepository<User, string> _userRepository;
    public UpdateUserRoleClientMappingPreValidator(IRepository<MembershipRoleClientMapping, string> userRoleClientMappingRepository, IRepository<User, string> userRepository)
    {
        _userRoleClientMappingRepository = userRoleClientMappingRepository;
        _userRepository = userRepository;
    }
}