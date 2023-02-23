using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;
public class DeleteOrganizationByIdPreValidator : AbstractValidator<DeleteOrganizationByIdRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public DeleteOrganizationByIdPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}