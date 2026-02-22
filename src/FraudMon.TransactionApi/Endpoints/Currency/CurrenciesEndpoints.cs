using Forext.CcyProvider.Database;
using Forext.CcyProvider.Domain;
using Forext.CcyProvider.Domain.Dtos;
using Forext.CcyProvider.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forext.CcyProvider.Endpoints.Currency;

public static class CurrenciesEndpoints
{
    public static WebApplication MapCurrenciesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/currency");
        group.MapPost("/", CreateCurrency);

        group.MapGet("/", GetCurrencies);

        return app;
    }

    public static async Task<NoContent> CreateCurrency(
                [FromBody]
                CreateCurrencyDto dto,
                ILoggerFactory loggerFactory,
                AuditTimestampUpdater audit,
                CcyProviderDbContext dbContext
            )
    {
        dbContext.Currencies.Add(new Domain.Currency
        {
            Code = dto.Code,
            Name = dto.Name,
            Symbol = dto.Symbol,
            IsActive = dto.IsActive,
            MinorUnits = dto.MinorUnits,
        });

        audit.Apply(dbContext.ChangeTracker);
        return TypedResults.NoContent();
    }

    public static async Task<Ok<IList<CurrencyDto>>> GetCurrencies(CcyProviderDbContext dbContext, CancellationToken ct = default)
    {
        var currencies = await dbContext.Currencies.Select(x=>CurrencyDto.From(x)).ToListAsync(ct);

        return TypedResults.Ok<IList<CurrencyDto>>(currencies);
    }
}
