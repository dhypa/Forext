using FluentValidation;
using NodaTime;
using System.Reflection;

namespace Forext.CcyProvider.Services;

public static class ServicesStartup
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
