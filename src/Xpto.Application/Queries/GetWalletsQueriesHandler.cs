using MediatR;
using Xpto.Application.Commands;
using Xpto.Application.Dtos;
using Xpto.Core.Repositories;

namespace Xpto.Application.Queries;

public class GetWalletsQueriesHandler : IRequestHandler<GetWalletsQueries, IEnumerable<GetWalletsDto>>
{
    private readonly IWalletRepository _walletRepository;
    public GetWalletsQueriesHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }


    public async Task<IEnumerable<GetWalletsDto>> Handle(GetWalletsQueries request, CancellationToken cancellationToken)
    {
        var result = await _walletRepository.GetWallets();

        var resultParsed = result.Select(x => new GetWalletsDto
        {
            Id = x.Id,
            Name = x.Name,
            Assets = x.Assets.Select(y => new AssetsDto
            {
                Id = y.Id,
                Address = y.Address,
            }).ToList()
        }).ToList();

        return resultParsed;
    }
}