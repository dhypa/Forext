using FraudMon.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace FraudMon.Shared.Domain;

public class Transaction
{
    public long InternalId;
    
    public Guid PublicId;
    public Guid UserId;
    public Guid MerchantId;

    public Money Amount;

    public TransactionStatus Status { get; private set; } = TransactionStatus.Received;

    public int RiskScore;

    public required PaymentMethodType PaymentMethodType { get; init; } = PaymentMethodType.BankTransfer;

    // Store safe references only
    public byte[]? PaymentInstrumentHash { get; private set; }   // VARBINARY in SQL

    public string? IpAddress { get; private set; }
    public string? CountryCode { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }

    public DateTimeOffset OccurredAtUtc { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ProcessedAtUtc { get; private set; }

    public Guid CorrelationId { get; private set; } = Guid.NewGuid();
}

public enum TransactionStatus : byte
{
    Received = 1,
    Flagged = 2,
    Cleared = 3,
    Rejected = 4
}
public enum PaymentMethodType : byte
{
    Card = 1,
    BankTransfer = 2,
    Wallet = 3
}
