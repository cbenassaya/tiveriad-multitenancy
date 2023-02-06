using Microsoft.EntityFrameworkCore;
using MediatR;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Tiveriad.Multitenancy.Application.Commands.OrganizationCommands;
public class SaveOrUpdateOrganizationRequestHandler : IRequestHandler<SaveOrUpdateOrganizationRequest, Organization>
{
    private readonly IRepository<Organization, string> _organizationRepository;
    public SaveOrUpdateOrganizationRequestHandler(IRepository<Organization, string> organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public Task<Organization> Handle(SaveOrUpdateOrganizationRequest request, CancellationToken cancellationToken)
    {
        var query = _organizationRepository.Queryable.Where(x => x.Id == request.Organization.Id);
        return Task.Run(async () =>
        {
            //<-- START CUSTOM CODE-->
            var result = query.ToList().FirstOrDefault();
            if (result == null)
            {
                await _organizationRepository.AddOneAsync(request.Organization, cancellationToken);
                return request.Organization;
            }
            else
            {
                result.Name = request.Organization.Name;
                result.Description = request.Organization.Description;
                result.State = request.Organization.State;
                result.CreatedBy = request.Organization.CreatedBy;
                result.Created = request.Organization.Created;
                result.LastModifiedBy = request.Organization.LastModifiedBy;
                result.LastModified = request.Organization.LastModified;
                return result;
            }
        //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}