namespace Forext.CcyProvider.Domain.Dtos;

public class CreateCurrencyDto
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Symbol { get; set; } // "€", "$"
    public short MinorUnits { get; set; }
    public bool IsActive { get; set; }
}
