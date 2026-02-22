namespace Forext.CcyProvider.Domain.Dtos;

public class CurrencyDto
{
    int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Symbol { get; set; } // "€", "$"
    public short MinorUnits { get; set; } = 2;

    public static CurrencyDto From(Currency currency)
    {
        return new CurrencyDto
        {
            Id = currency.Id,
            Code = currency.Code,
            Name = currency.Name,
            Symbol = currency.Symbol,
            MinorUnits = currency.MinorUnits,
        };
    }
}
