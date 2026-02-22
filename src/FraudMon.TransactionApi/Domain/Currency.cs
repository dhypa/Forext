namespace Forext.CcyProvider.Domain;

using Forext.CcyProvider.Domain.Dtos;
using NodaTime;
using System.ComponentModel.DataAnnotations;

public sealed class Currency : IAuditableEntity
{
    public int Id { get; set; }

    [MaxLength(64)]
    public required string Name { get; set; }

    // ISO 4217 like "EUR"
    [MaxLength(3)]
    public required string Code { get; set; }
    // Typical minor units: USD=2, JPY=0, etc.
    public short MinorUnits { get; set; }

    [MaxLength(8)]
    public string? Symbol { get; set; } // "€", "$"

    public bool IsActive { get; set; }

    public Instant CreatedAt { get; set; }
    public Instant UpdatedAt { get; set; }
}

