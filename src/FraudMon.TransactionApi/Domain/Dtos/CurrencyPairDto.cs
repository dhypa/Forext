using NodaTime;
using System.ComponentModel.DataAnnotations;

namespace Forext.CcyProvider.Domain.Dtos;

public class CurrencyPairDto
{
    public int Id { get; set; }

    // e.g "EUR-USD"
    [MaxLength(7)]
    public required string Symbol { get; set; }

    public int BaseCurrencyId { get; set; }
    public required Currency BaseCurrency { get; set; }

    public int QuoteCurrencyId { get; set; }
    public required Currency QuoteCurrency { get; set; }

    // Optional lifecycle/trading window for the pair
    public Instant? TradingOpenAt { get; set; }
    public Instant? TradingCloseAt { get; set; }

    public bool IsEnabled { get; set; } = true;

    public static CurrencyPairDto From(CurrencyPair source)
    {
        return new CurrencyPairDto()
        {
            Id = source.Id,
            Symbol = source.Symbol,

            BaseCurrencyId = source.BaseCurrencyId,
            BaseCurrency = source.BaseCurrency,
            
            QuoteCurrencyId = source.QuoteCurrencyId,
            QuoteCurrency = source.QuoteCurrency,
            
            TradingCloseAt = source.TradingCloseAt,
            TradingOpenAt = source.TradingOpenAt
        };
    }
}
