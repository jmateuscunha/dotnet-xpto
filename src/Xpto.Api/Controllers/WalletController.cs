using Microsoft.AspNetCore.Mvc;
using Xpto.Core.Repositories;

namespace Xpto.Api.Controllers;

[ApiController]
[Route("api/wallet")]
public class WalletController : ControllerBase
{
    private readonly IWalletRepository _walletRepository;
    public WalletController(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet()
    {
        await _walletRepository.CreateWallet();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetWallets()
    => Ok(await _walletRepository.GetWallets());    
}
