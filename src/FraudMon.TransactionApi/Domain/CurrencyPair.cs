using NodaTime;
using System.ComponentModel.DataAnnotations;

namespace Forext.CcyProvider.Domain;

public sealed class CurrencyPair : IAuditableEntity
{
    public int Id { get; set; }

    // e.g "EUR-USD"
    [MaxLength(7)]
    public required string Symbol { get; set; }

    public int BaseCurrencyId { get; set; }
    public Currency BaseCurrency { get; set; } = null!;

    public int QuoteCurrencyId { get; set; }
    public Currency QuoteCurrency { get; set; } = null!;

    // Optional lifecycle/trading window for the pair
    public Instant? TradingOpenAt { get; set; }
    public Instant? TradingCloseAt { get; set; }

    public bool IsEnabled { get; set; } = true;

    public Instant CreatedAt { get; set; } 
    public Instant UpdatedAt { get; set; }
}