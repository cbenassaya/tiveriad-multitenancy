using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;
public class SaveOrUpdateOrganizationPreValidator : AbstractValidator<SaveOrUpdateOrganizationRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public SaveOrUpdateOrganizationPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}