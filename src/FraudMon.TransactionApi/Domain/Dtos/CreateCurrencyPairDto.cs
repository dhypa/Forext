using NodaTime;

namespace Forext.CcyProvider.Domain.Dtos;

public sealed class CreateCurrencyPairDto
{
    public int BaseCurrencyId { get; set; }
    public int QuoteCurrencyId { get; set; }

    public bool IsEnabled { get; set; }
    public Instant? TradingOpenAt { get; set; }
    public Instant? TradingCloseAt { get; set; }
}
