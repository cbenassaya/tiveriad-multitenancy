using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Commands.MembershipCommands;
public class DeleteMembershipByIdPreValidator : AbstractValidator<DeleteMembershipByIdRequest>
{
    private IRepository<Membership, string> _membershipRepository;
    private IRepository<User, string> _userRepository;
    public DeleteMembershipByIdPreValidator(IRepository<Membership, string> membershipRepository, IRepository<User, string> userRepository)
    {
        _membershipRepository = membershipRepository;
        _userRepository = userRepository;
    }
}