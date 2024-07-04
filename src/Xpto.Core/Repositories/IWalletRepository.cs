using Xpto.Domain.Entities;

namespace Xpto.Core.Repositories;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet>> GetWallets();
    Task CreateWallet();
}
