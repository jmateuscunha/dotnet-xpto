using Xpto.Application.Commands;

namespace Xpto.Application.Dtos;

public class GetWalletsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<AssetsDto> Assets { get; set; }
}
