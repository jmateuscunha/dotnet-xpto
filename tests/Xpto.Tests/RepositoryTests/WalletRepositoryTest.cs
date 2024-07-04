using Bogus;
using Moq;
using Moq.AutoMock;
using Xpto.Core.Repositories;
using Xpto.Domain.Entities;
using Xpto.Infra.Database.Relational;
using Xpto.Repository;

namespace Xpto.Tests.RepositoryTests;

public class WalletRepositoryTest
{
    private readonly AutoMocker _mocker;
    private readonly XptoDbContext _context;
    private readonly IWalletRepository _walletRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    public WalletRepositoryTest()
    {

        _context = new XptoDbContextInMemory().CreateConxtext();
        _walletRepository = new WalletRepository(_context);
        _unitOfWork = new Mock<IUnitOfWork>();
    }

    [Fact]
    public async Task Should_CreateWallet_WithSuccess()
    {
        //Arrange
        var walletFaker = new Faker<Wallet>().CustomInstantiator(f => new Wallet(f.Random.AlphaNumeric(5), new Asset(f.Random.AlphaNumeric(5)))).Generate();
        await _context.Wallets.AddAsync(walletFaker);

        //Act
        await _walletRepository.CreateWallet(walletFaker);
        var wallet = await _walletRepository.GetWallets();

        //Assert
        Assert.NotEmpty(wallet);
        Assert.NotNull(wallet.FirstOrDefault().Assets);
    }
}