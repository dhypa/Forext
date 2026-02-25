namespace Forext.CcyProvider.Endpoints;

public static class EndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapCurrenciesEndpoints();
        app.MapCurrencyPairEndpoints();
        return app;
    }

}
