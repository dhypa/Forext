using FluentValidation;
using Forext.CcyProvider.Domain.Dtos;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;

namespace Forext.CcyProvider.Domain.Validators;

public class CreateCurrencyDtoValidator : AbstractValidator<CurrencyDto>
{
    public CreateCurrencyDtoValidator()
    {
        RuleFor(x => x.Code).Length(3).NotNull().Must(BeUpperCase);
        RuleFor(x => x.Name).NotEmpty();
    }

    private bool BeUpperCase(string candidate)
    {
        for (int i = 0; i < candidate.Length; i++)
        {
            if (char.IsLower(candidate[i]))
            {
                return false;
            }
        }
        return true;
    }
}
