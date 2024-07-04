﻿using MediatR;
using Xpto.Application.Dtos;
using Xpto.Core.IntegrationServices;
using Xpto.Core.Repositories;
using Xpto.Shared;

namespace Xpto.Application.Queries;

public class GetWalletsQueriesHandler : IRequestHandler<GetWalletsQueries, IEnumerable<WalletsDto>>
{
    private readonly IWalletRepository _walletRepository;
    public GetWalletsQueriesHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<IEnumerable<WalletsDto>> Handle(GetWalletsQueries request, CancellationToken cancellationToken)
    {
        var result = await _walletRepository.GetWallets();

        var resultParsed = result.Select(x => new WalletsDto
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