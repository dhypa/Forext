using System;
using System.Collections.Generic;
using System.Text;

namespace FraudMon.Common.Domain;

public struct Currency
{
    public string Code { get; set; }

    private Currency(string code)
    {
        Code = code;
    }
    public static Currency From(string code)
    {
        if (string.IsNullOrEmpty(code) || code.Length != 3)
        {
            throw new ArgumentException("Invalid country code");
        }

        return new Currency(code.ToUpperInvariant());
    }

    public override string ToString() => Code;
}
