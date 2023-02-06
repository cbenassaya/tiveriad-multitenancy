using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using Tiveriad.Multitenancy.Api.Contracts;
using System.Threading;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.EndPoints.OrganizationEndPoints;
public class SaveOrUpdateEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public SaveOrUpdateEndPoint(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("/api/organizations")]
    public async Task<ActionResult<OrganizationReaderModel>> HandleAsync([FromBody] OrganizationWriterModel model, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var entity = _mapper.Map<OrganizationWriterModel, Organization>(model);
        var result = await _mediator.Send(new SaveOrUpdateOrganizationRequest(entity), cancellationToken);
        var data = _mapper.Map<Organization, OrganizationReaderModel>(result);
        //<-- END CUSTOM CODE-->
        return Ok(data);
    }
}