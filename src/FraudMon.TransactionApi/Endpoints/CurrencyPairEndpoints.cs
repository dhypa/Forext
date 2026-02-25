using FluentValidation;
using FluentValidation.Results;
using Forext.CcyProvider.Database;
using Forext.CcyProvider.Domain;
using Forext.CcyProvider.Domain.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forext.CcyProvider.Endpoints;

public static class CurrencyPairEndpoints
{
    public static WebApplication MapCurrencyPairEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/ccy");

        group.MapGet("/", GetAllCurrencyPairs);

        group.MapGet("/{id:int}", GetCurrencyPair);

        return app;
    }

    public static async Task<Ok<IList<CurrencyPairDto>>> GetAllCurrencyPairs(
        CcyProviderDbContext dbContext,
        CancellationToken ct = default
    )
    {
        var ccyPairs = await dbContext.CurrencyPairs.Select(x => CurrencyPairDto.From(x)).ToListAsync(ct);
        return TypedResults.Ok<IList<CurrencyPairDto>>(ccyPairs);
    }

    public static async Task<Ok<CurrencyPairDto>> GetCurrencyPair(
        [FromRoute] int id,
        CcyProviderDbContext dbContext,
        CancellationToken ct = default
    )
    {
        var result = await dbContext.CurrencyPairs.Where(x => x.Id == id).Select(x => CurrencyPairDto.From(x)).FirstOrDefaultAsync(ct);
        return TypedResults.Ok(result);
    }

    public static async Task<Results<BadRequest<ProblemDetails>, Created>> CreateNewCurrencyPair(
        [FromBody] CreateCurrencyPairDto dto,
        CcyProviderDbContext dbContext,
        [FromServices] IValidator<CreateCurrencyPairDto> validator,
        CancellationToken ct = default
    )
    {
        var validationResult = validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            //return CreateValidationErrorResult(validationResult);
            return validationResult.ToBadRequest();
        }

        var baseCurrencyTask = dbContext.Currencies
            .Where(x => x.Id == dto.BaseCurrencyId)
            .SingleOrDefaultAsync(ct);
        var quoteCurrencyTask = dbContext.Currencies
            .Where(x => x.Id == dto.QuoteCurrencyId)
            .SingleOrDefaultAsync(ct);

        await Task.WhenAll(baseCurrencyTask, quoteCurrencyTask);

        var baseCurrency = baseCurrencyTask.Result;
        var quoteCurrency = quoteCurrencyTask.Result;

        if (baseCurrency is null || quoteCurrency is null)
        {
            return TypedResults.BadRequest(new ProblemDetails
            {
                Title = "Invalid currency",
                Detail = $"{(baseCurrency is null ? "Base" : "")}{(baseCurrency is null && quoteCurrency is null ? " and " : "")}{(quoteCurrency is null ? "Quote" : "")} currency not found.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        // Ensure pair doesn't already exist
        var pairAlreadyExists = await dbContext.CurrencyPairs.AnyAsync(
            p => p.BaseCurrencyId == dto.BaseCurrencyId && p.QuoteCurrencyId == dto.QuoteCurrencyId,
            ct);

        if (pairAlreadyExists)
        {
            return TypedResults.BadRequest(new ProblemDetails
            {
                Title = "Currency pair already exists",
                Detail = "A pair with the same base and quote already exists.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        dbContext.CurrencyPairs.Add(new CurrencyPair()
        {
            Symbol = baseCurrency.Symbol + '-' + quoteCurrency.Symbol,

            BaseCurrencyId = baseCurrency.Id,
            QuoteCurrencyId = quoteCurrency.Id,

            IsEnabled = dto.IsEnabled,

            TradingOpenAt = dto.TradingOpenAt,
            TradingCloseAt = dto.TradingCloseAt,
        });

        await dbContext.SaveChangesAsync(ct);

        return TypedResults.Created();
    }

    private static BadRequest<ProblemDetails> ToBadRequest(this ValidationResult validationResult)
    {
        return TypedResults.BadRequest(new ProblemDetails
        {
            Title = "Validation failed",
            Detail = string.Join("; ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")),
            Status = StatusCodes.Status400BadRequest 
        });
    }
}
