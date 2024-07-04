using Microsoft.EntityFrameworkCore;
using Xpto.Core.Commands;
using Xpto.Core.Repositories;
using Xpto.Domain.Entities;
using Xpto.Infra.Database.Relational;

namespace Xpto.Repository;

public sealed class WalletRepository : IWalletRepository
{
    private readonly XptoDbContext _db;
    public WalletRepository(XptoDbContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<Wallet>> GetWallets()
    {
        var result = await _db.Wallets.AsNoTrackingWithIdentityResolution().Include(a => a.Assets).ToListAsync();

        return result;
    }

    public async Task CreateWallet(CreateWalletCommand command)
    {
        var wallet = new Wallet(command.Name, new Asset(command.ChainId));

        await _db.AddAsync(wallet);
        await _db.Commit();
    }
}
