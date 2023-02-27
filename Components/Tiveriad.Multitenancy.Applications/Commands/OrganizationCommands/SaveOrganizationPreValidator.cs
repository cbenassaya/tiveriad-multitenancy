using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;
public class SaveOrganizationPreValidator : AbstractValidator<SaveOrganizationRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public SaveOrganizationPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}