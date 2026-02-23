using FluentValidation;
using FluentValidation.AspNetCore;
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

        group.MapGet("/id/{id:int}", GetCurrencyById);

        group.MapGet("/code/{id:string}", GetCurrencyByCode);

        return app;
    }

    public static async Task<NoContent> CreateCurrency(
                [FromBody]
                CreateCurrencyDto dto,
                ILoggerFactory loggerFactory,
                CcyProviderDbContext dbContext,
                IValidator<CreateCurrencyDto> validator,
                HttpContext httpContext
            )
    {
        var validationResult = validator.Validate(dto);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Validation failed",
                //Detail = string.Join("; ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")),
                Status = StatusCodes.Status400BadRequest
            });
        }


        dbContext.Currencies.Add(new Domain.Currency
        {
            Code = dto.Code,
            Name = dto.Name,
            Symbol = dto.Symbol,
            IsActive = dto.IsActive,
            MinorUnits = dto.MinorUnits,
        });

        return TypedResults.NoContent();

    }

    public static async Task<Ok<IList<CurrencyDto>>> GetCurrencies(CcyProviderDbContext dbContext, CancellationToken ct = default)
    {
        var currencies = await dbContext.Currencies.Select(x => CurrencyDto.From(x)).ToListAsync(ct);

        return TypedResults.Ok<IList<CurrencyDto>>(currencies);
    }

    public static async Task<Results<Ok<CurrencyDto>, NotFound<ProblemDetails>>> GetCurrencyById(CcyProviderDbContext dbContext, int id, CancellationToken ct = default)
    {
        var result = await dbContext.Currencies.Where(x => x.Id == id).Select(x => CurrencyDto.From(x)).SingleOrDefaultAsync(ct);
        if (result is null)
        {
            return TypedResults.NotFound(
            new ProblemDetails
            {
                Title = "Currency not found",
                Detail = $"No currency with id '{id}' exists.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return TypedResults.Ok(result);
    }

    public static async Task<Results<Ok<CurrencyDto>, NotFound<ProblemDetails>>> GetCurrencyByCode(CcyProviderDbContext dbContext, string code, CancellationToken ct = default)
    {
        var result = await dbContext.Currencies.Where(x => x.Code == code).Select(x => CurrencyDto.From(x)).SingleOrDefaultAsync(ct);
        if (result == null)
        {
            return TypedResults.NotFound(
            new ProblemDetails
            {
                Title = "Currency not found",
                Detail = $"No currency with code '{code}' exists.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return TypedResults.Ok(result);
    }
}
