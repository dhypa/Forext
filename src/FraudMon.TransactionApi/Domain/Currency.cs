namespace Forext.CcyProvider.Domain;

using System.ComponentModel.DataAnnotations;

public sealed class Currency
{
    public int Id { get; set; }

    // ISO 4217 like "EUR"
    [MaxLength(3)]
    public required string Code { get; set; }

    [MaxLength(64)]
    public required string Name { get; set; }

    // Typical minor units: USD=2, JPY=0, etc.
    public short MinorUnits { get; set; } = 2;

    [MaxLength(8)]
    public string? Symbol { get; set; } // "€", "$"

    public bool IsActive { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}

