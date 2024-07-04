using MediatR;
using Xpto.Application.Dtos;

namespace Xpto.Application.Queries;

public class GetWalletsQueries : IRequest<IEnumerable<WalletsDto>>
{

}
