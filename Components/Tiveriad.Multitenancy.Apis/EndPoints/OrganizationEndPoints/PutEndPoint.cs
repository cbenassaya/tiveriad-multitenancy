﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tiveriad.Multitenancy.Apis.Contracts;
using Tiveriad.Multitenancy.Apis.Filters;
using Tiveriad.Multitenancy.Applications.Commands.OrganizationCommands;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Apis.EndPoints.OrganizationEndPoints;

public class PutEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public PutEndPoint(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPut("/api/organizations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ValidateModel]
    public async Task<ActionResult<OrganizationReaderModel>> HandleAsync([FromBody] OrganizationWriterModel model, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var entity = _mapper.Map<OrganizationWriterModel, Organization>(model);
        var result = await _mediator.Send(new UpdateOrganizationRequest(entity), cancellationToken);
        var data = _mapper.Map<Organization, OrganizationReaderModel>(result);
        //<-- END CUSTOM CODE-->
        return Ok(data);
    }
}