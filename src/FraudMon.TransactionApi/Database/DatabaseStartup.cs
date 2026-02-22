using Microsoft.EntityFrameworkCore;

namespace Forext.CcyProvider.Database;

public static class DatabaseStartup
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddSingleton<AuditTimestampInterceptor>()
            .AddDbContext<CcyProviderDbContext>(options =>
            {
                options.UseNpgsql(
                    config.GetConnectionString("Database"),
                    o => o.UseNodaTime()
                );
            });
    }
}
