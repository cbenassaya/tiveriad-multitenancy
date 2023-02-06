using Microsoft.AspNetCore.Mvc;
using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using Tiveriad.Multitenancy.Api.Contracts;
using System.Threading;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.EndPoints.UserEndPoints;
public class GetAllEndPoint : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public GetAllEndPoint(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("/api/users")]
    public async Task<ActionResult<UserReaderModel>> HandleAsync(CancellationToken cancellationToken)
    {
        //<-- START CUSTOM CODE-->
        var result = await _mediator.Send(new GetAllUsersRequest(), cancellationToken);
        var data = _mapper.Map<IEnumerable<User>, IEnumerable<UserReaderModel>>(result);
        //<-- END CUSTOM CODE-->
        return Ok(data);
    }
}