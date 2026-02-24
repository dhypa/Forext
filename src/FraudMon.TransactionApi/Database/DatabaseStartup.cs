using Microsoft.EntityFrameworkCore;

namespace Forext.CcyProvider.Database;

public static class DatabaseStartup
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddScoped<AuditTimestampInterceptor>()
            .AddDbContext<CcyProviderDbContext>(options =>
            {
                options.UseNpgsql(
                    config.GetConnectionString("CurrencyPairsDb"),
                    o => o.UseNodaTime()
                );
            });
    }
}
