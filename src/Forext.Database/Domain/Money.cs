using System;
using System.Collections.Generic;
using System.Text;

namespace FraudMon.Common.Domain;

public struct Money
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money From(decimal amount, string currencyCode)
        => new Money(amount, Currency.From(currencyCode));

    public override string ToString() => $"{Amount} {Currency.Code}";
}
