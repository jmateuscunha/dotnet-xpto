using Xpto.Shared;

namespace Xpto.Core.IntegrationServices;

public interface IChainIdIntegrationService
{
    Task<IEnumerable<ChainIdInfoDto>> GetBlockchainChainInfo(CancellationToken ct);
}
