using MediatR;

namespace Xpto.Application.Commands;

public class CreateWalletCommand : IRequest
{
    public string Name { get; set; }
    public string ChainId { get; set; }
}