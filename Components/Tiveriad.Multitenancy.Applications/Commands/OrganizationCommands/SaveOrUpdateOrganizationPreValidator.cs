using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Commands.OrganizationCommands;
public class SaveOrUpdateOrganizationPreValidator : AbstractValidator<SaveOrUpdateOrganizationRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public SaveOrUpdateOrganizationPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}