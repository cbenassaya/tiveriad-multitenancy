using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using Tiveriad.Multitenancy.Api.Contracts;
using System;
using System.Threading;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.EndPoints.OrganizationEndPoints;
public class GetByIdEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public GetByIdEndPoint(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/api/organizations/{id}")]
    public async Task<ActionResult<OrganizationReaderModel>> HandleAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var result = await _mediator.Send(new GetOrganizationByIdRequest(id), cancellationToken);
        var data = _mapper.Map<Organization, OrganizationReaderModel>(result);
        //<-- END CUSTOM CODE-->
        return Ok(data);
    }
}