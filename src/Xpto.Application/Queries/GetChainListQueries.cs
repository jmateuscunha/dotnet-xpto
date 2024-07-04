using MediatR;
using Xpto.Shared;

namespace Xpto.Application.Queries;

public class GetChainListQueries : IRequest<IEnumerable<ChainIdInfoDto>>
{
    public string ChainId { get; set; }
}
