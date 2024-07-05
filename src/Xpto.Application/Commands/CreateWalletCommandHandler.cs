using FluentValidation;
using MediatR;
using Xpto.Core.Repositories;
using Xpto.Domain.Entities;

namespace Xpto.Application.Commands;

public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IValidator<CreateWalletCommand> _validator;
    public CreateWalletCommandHandler(IWalletRepository walletRepository, IValidator<CreateWalletCommand> validator)
    {
        _walletRepository = walletRepository;
        _validator = validator;
    }
    public async Task Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var validateCommand = await _validator.ValidateAsync(request, cancellationToken);

        if (validateCommand.IsValid == false) return; //show use proper middleware for error handling.

        var wallet  = new Wallet(request.Name, new Asset(request.ChainId));
        await _walletRepository.CreateWallet(wallet);
    }
}