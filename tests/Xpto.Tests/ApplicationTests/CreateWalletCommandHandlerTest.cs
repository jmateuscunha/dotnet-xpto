using Bogus;
using Moq;
using Moq.AutoMock;
using Xpto.Application.Commands;
using Xpto.Application.Commands.Handlers;
using Xpto.Core.Repositories;
using Xpto.Domain.Entities;

namespace Xpto.Tests.ApplicationTests
{
    public class CreateWalletCommandHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly CreateWalletCommandHandler _handler;

        public CreateWalletCommandHandlerTest()
        {
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<CreateWalletCommandHandler>();
        }

        [Fact]
        public async Task Should_CreateWallet_WithSuccess()
        {
            //Arrange
            var faker = new Faker<CreateWalletCommand>().CustomInstantiator(f => new CreateWalletCommand { ChainId = f.Finance.AccountName(), Name = f.Finance.AccountName() }).Generate();
            _mocker.GetMock<IWalletRepository>().Setup(c => c.CreateWallet(It.IsAny<Wallet>()));

            //Act

            await _handler.Handle(faker, CancellationToken.None);

            //Assert
            _mocker.GetMock<IWalletRepository>().Verify(v => v.CreateWallet(It.IsAny<Wallet>()), Times.Once);

        }
    }
}