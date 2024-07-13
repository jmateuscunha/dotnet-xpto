using MediatR;
using Xpto.Core.IntegrationServices;
using Xpto.Shared;

namespace Xpto.Application.Queries.Handlers;

public class GetChainListQueriesHandler : IRequestHandler<GetChainListQueries, IEnumerable<ChainIdInfoDto>>
{
    private readonly IChainIdIntegrationService _chainIdIntegrationService;
    public GetChainListQueriesHandler(IChainIdIntegrationService chainIdIntegrationService)
    {

        _chainIdIntegrationService = chainIdIntegrationService;
    }

    public async Task<IEnumerable<ChainIdInfoDto>> Handle(GetChainListQueries request, CancellationToken cancellationToken)
    {
        var result = await _chainIdIntegrationService.GetBlockchainChainInfo(cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.ChainId)) return [result.FirstOrDefault(c => c.ChainId.ToString().Equals(request.ChainId, StringComparison.OrdinalIgnoreCase))];

        return result;
    }
}