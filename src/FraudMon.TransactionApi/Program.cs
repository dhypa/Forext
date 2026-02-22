
using Forext.CcyProvider.Database;
using Forext.CcyProvider.Domain;
using Forext.CcyProvider.Endpoints;
using Forext.CcyProvider.Services;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace FraudMon.TransactionApi;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        
        builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddSingleton<AuditTimestampInterceptor>();
        builder.Services.AddSingleton<IClock>(SystemClock.Instance);

        builder.Services.AddDbContext<CcyProviderDbContext>(options =>
        {
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("Database"),
                o=> o.UseNodaTime()
            );
        });

        builder.Services.AddServices(builder.Configuration);

        WebApplication app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapEndpoints();

        app.MapControllers();

        app.Run();
    }
}
