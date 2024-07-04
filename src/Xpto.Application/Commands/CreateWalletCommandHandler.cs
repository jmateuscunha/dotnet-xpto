using MediatR;
using Xpto.Core.Repositories;

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
        await _walletRepository.CreateWallet();
    }
}