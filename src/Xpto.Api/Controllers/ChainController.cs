using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xpto.Application.Queries;

namespace Xpto.Api.Controllers;

[ApiController]
[Route("api/chain")]
public class ChainController : ControllerBase
{
    private readonly IMediator _mediator;
    public ChainController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetChains([FromQuery] string chainId, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetChainListQueries { ChainId = chainId}, ct);

        return Ok(result);
    }
}
