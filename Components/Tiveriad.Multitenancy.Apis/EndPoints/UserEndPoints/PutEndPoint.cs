﻿using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tiveriad.Multitenancy.Apis.Contracts;
using Tiveriad.Multitenancy.Apis.Filters;
using Tiveriad.Multitenancy.Applications.Commands.UserCommands;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Apis.EndPoints.UserEndPoints;

public class PutEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public PutEndPoint(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPut("/api/organizations/{organizationId}/users/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ValidateModel]
    public async Task<ActionResult<UserReaderModel>> HandleAsync([Required][FromRoute] string organizationId,[Required] [FromRoute] string userId,[FromBody] UserStateUpdaterModel model, CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        if (!Enum.TryParse<MembershipState>(model.State, true, out var state))
            return BadRequest("State is not valid");
        var result = await _mediator.Send(new UpdateMembershipStateRequest(organizationId,userId, state), cancellationToken);
        var data = _mapper.Map<User, UserReaderModel>(result);
        //<-- END CUSTOM CODE-->
        return Ok(data);
    }
}