namespace Xpto.Domain.Entities;

public class Wallet
{    
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public virtual ICollection<Asset> Assets { get; set; } = [];
    public Wallet(string name, Asset asset = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        if (asset is not null)
        {
            asset.SetWalletId(Id);
            Assets.Add(asset);
        }
    }
    public Wallet() { }
}