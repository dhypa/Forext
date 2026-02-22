namespace Forext.CcyProvider.Domain.Dtos;

public sealed class CreateCurrencyPairRequest
{
    public string Symbol { get; set; }
    public int BaseCurrencyId { get; set; }
    public int QuoteCurrencyId { get; set; }
    public short DisplayPrecision { get; set; }
    public DateTimeOffset? TradingOpenAt { get; set; }
    public DateTimeOffset? TradingCloseAt { get; set; }
}
