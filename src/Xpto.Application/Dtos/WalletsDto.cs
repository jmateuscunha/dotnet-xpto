namespace Xpto.Application.Dtos;

public class WalletsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IReadOnlyCollection<AssetsDto> Assets { get; set; }
}
