using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;

public class UpdateOrganizationPreValidator : AbstractValidator<UpdateOrganizationRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public UpdateOrganizationPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}