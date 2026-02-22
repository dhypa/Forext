using Forext.CcyProvider.Domain.Dtos;
using Forext.CcyProvider.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Forext.CcyProvider.Endpoints.Currency;

public static class CreateCurrencyEndpoints
{
    public static IEndpointRouteBuilder MapCreateCurrencyEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/currency",
            CreateCurrency);

        return app;
    }

    public static async Task<NoContent> CreateCurrency(
                [FromBody]
                CreateCurrencyDto dto,
                ILoggerFactory loggerFactory,
                AuditTimestampUpdater audit
            )
    {
        await Task.CompletedTask;

        return TypedResults.NoContent();
    }
}
