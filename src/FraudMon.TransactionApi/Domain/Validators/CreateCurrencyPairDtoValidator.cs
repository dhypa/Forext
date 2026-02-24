using FluentValidation;
using Forext.CcyProvider.Domain.Dtos;

namespace Forext.CcyProvider.Domain.Validators;

public class CreateCurrencyPairDtoValidator : AbstractValidator<CreateCurrencyPairDto>
{
    public CreateCurrencyPairDtoValidator()
    {
        RuleFor(x => x.BaseCurrencyId)
            .NotNull();

        RuleFor(x => x.QuoteCurrencyId)
            .NotNull();

        RuleFor(x => x)
            .Must(x => x.BaseCurrencyId != x.QuoteCurrencyId)
            .WithMessage("BaseCurrencyId and QuoteCurrencyId must be different.");
    }
}
