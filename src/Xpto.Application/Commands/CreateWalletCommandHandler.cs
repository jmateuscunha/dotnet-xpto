using MediatR;
using Xpto.Core.Repositories;
using Xpto.Domain.Entities;

namespace Xpto.Application.Commands;

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand>
{
    private readonly IWalletRepository _walletRepository;
    public CreateWalletCommandHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    public async Task Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet  = new Wallet(request.Name, new Asset(request.ChainId));
        await _walletRepository.CreateWallet(wallet);
    }
}