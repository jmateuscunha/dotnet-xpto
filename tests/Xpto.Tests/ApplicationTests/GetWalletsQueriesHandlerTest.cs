using Bogus;
using Moq;
using Moq.AutoMock;
using Xpto.Application.Queries;
using Xpto.Application.Queries.Handlers;
using Xpto.Core.Repositories;
using Xpto.Domain.Entities;

namespace Xpto.Tests.ApplicationTests;

public class GetWalletsQueriesHandlerTest
{
    private readonly AutoMocker _mocker;
    private readonly GetWalletsQueriesHandler _handler;

    public GetWalletsQueriesHandlerTest()
    {
        _mocker = new AutoMocker();
        _handler = _mocker.CreateInstance<GetWalletsQueriesHandler>();
    }

    [Fact]
    public async Task Should_GetWallets_WithSuccess()
    {
        //Arrange
        var faker = new Faker<GetWalletsQueries>().CustomInstantiator(f => new GetWalletsQueries { }).Generate();
        var walletFaker = new Faker<Wallet>().CustomInstantiator(f => new Wallet(f.Random.AlphaNumeric(5), new Asset(f.Random.AlphaNumeric(5)))).Generate(5);

        _mocker.GetMock<IWalletRepository>().Setup(c => c.GetWallets()).ReturnsAsync(walletFaker);

        //Act

        var result = await _handler.Handle(faker, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
    }
}
