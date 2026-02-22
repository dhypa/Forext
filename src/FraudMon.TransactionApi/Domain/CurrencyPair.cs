using System.ComponentModel.DataAnnotations;

namespace Forext.CcyProvider.Domain;

public sealed class CurrencyPair
{
    public int Id { get; set; }

    // e.g "EUR-USD"
    [MaxLength(7)]
    public required string Symbol { get; set; }

    public int BaseCurrencyId { get; set; }
    public required Currency BaseCurrency { get; set; }

    public int QuoteCurrencyId { get; set; }
    public required Currency QuoteCurrency { get; set; }

    // Frontend formatting hint
    public short DisplayPrecision { get; set; } = 5;

    // Optional lifecycle/trading window for the pair
    public DateTimeOffset? TradingOpenAt { get; set; }
    public DateTimeOffset? TradingCloseAt { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}