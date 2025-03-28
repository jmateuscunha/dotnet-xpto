using Bogus;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.AutoMock;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
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
        private readonly Mock<IValidator<CreateWalletCommand>> _validatorMock;
        private readonly Mock<IWalletRepository> _walletRepositoryMock;

        public CreateWalletCommandHandlerTest()
        {
            _mocker = new AutoMocker();
            _walletRepositoryMock = new Mock<IWalletRepository>();
            _validatorMock = new Mock<IValidator<CreateWalletCommand>>();
            _handler = new CreateWalletCommandHandler(_walletRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        [Trait("Handler", "CreateWallet")]
        public async Task Should_CreateWallet_WithSuccess()
        {
            //Arrange
            var faker = new Faker<CreateWalletCommand>().CustomInstantiator(f => new CreateWalletCommand { ChainId = f.Finance.AccountName(), Name = f.Finance.AccountName() }).Generate();

            var validationResult = new FluentValidation.Results.ValidationResult() { Errors = new List<ValidationFailure>()};

            _walletRepositoryMock.Setup(v => v.CreateWallet(It.IsAny<Wallet>()));
            _validatorMock.Setup(v => v.ValidateAsync(faker, It.IsAny<CancellationToken>())).ReturnsAsync(await Task.FromResult(validationResult));
            //Act

            await _handler.Handle(faker, CancellationToken.None);

            //Assert
            _walletRepositoryMock.Verify(w => w.CreateWallet(It.IsAny<Wallet>()), Times.Once);


        }

        [Fact]
        [Trait("Handler", "CreateWallet")]
        public async Task ShouldNot_CreateWallet_InvalidRequest()
        {
            //Arrange
            var faker = new Faker<CreateWalletCommand>().CustomInstantiator(f => new CreateWalletCommand { ChainId = f.Finance.AccountName(), Name = f.Finance.AccountName() }).Generate();

            var validationResult = new FluentValidation.Results.ValidationResult(new List<ValidationFailure> { new ValidationFailure("Name", "Invalid name") });

            _walletRepositoryMock.Setup(v => v.CreateWallet(It.IsAny<Wallet>()));
            _validatorMock.Setup(v => v.ValidateAsync(faker, It.IsAny<CancellationToken>())).ReturnsAsync(await Task.FromResult(validationResult));
            //Act

            await _handler.Handle(faker, CancellationToken.None);

            //Assert
            _walletRepositoryMock.Verify(w => w.CreateWallet(It.IsAny<Wallet>()), Times.Never);
        }
    }
}