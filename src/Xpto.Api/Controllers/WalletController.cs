using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xpto.Application.Commands;
using Xpto.Application.Dtos;
using Xpto.Application.Queries;

namespace Xpto.Api.Controllers;

[ApiController]
[Route("api/wallet")]
public class WalletController : ControllerBase
{
    private readonly IMediator _mediator;
    public WalletController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDto dto, CancellationToken ct)
    {
        var command = new CreateWalletCommand() { Name = dto.Name, ChainId = dto.ChainId };

        await _mediator.Send(command, ct);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetWallets(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetWalletsQueries(), ct);

        return Ok(result);
    } 
}