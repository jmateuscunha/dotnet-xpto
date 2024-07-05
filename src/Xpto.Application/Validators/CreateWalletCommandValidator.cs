using FluentValidation;
using Xpto.Application.Commands;

namespace Xpto.Application.Validators;

public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
{
    private readonly int _minimum = 3;
    private readonly int _maximum = 12;
    public CreateWalletCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .Length(_minimum, _maximum)
            .WithMessage($"Name field must have min length of {_minimum} and maximum of {_maximum}");
    }
}
