using FluentValidation;
using Tiveriad.Repositories;
using Tiveriad.Multitenancy.Core.Entities;
using System;

namespace Tiveriad.Multitenancy.Application.Commands.OrganizationCommands;
public class DeleteOrganizationByIdPreValidator : AbstractValidator<DeleteOrganizationByIdRequest>
{
    private IRepository<Organization, string> _organizationRepository;
    public DeleteOrganizationByIdPreValidator(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }
}