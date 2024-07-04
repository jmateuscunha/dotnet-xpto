using shared.api.Abstracts;
using System.Net;
using System.Net.Http.Json;
using Xpto.Core.IntegrationServices;
using Xpto.Shared;

namespace Xpto.Communication;

public class ChainIdIntegrationService : BaseClient, IChainIdIntegrationService
{
    public ChainIdIntegrationService(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<ChainIdInfoDto>> GetBlockchainChainInfo(CancellationToken ct)
    {
        var response = await _httpClient.GetAsync($"/chains.json",ct);

        if (response.StatusCode is not HttpStatusCode.OK) throw new Exception("FAILED.");

        return await response.Content.ReadFromJsonAsync<IEnumerable<ChainIdInfoDto>>(ct);
    }
}


