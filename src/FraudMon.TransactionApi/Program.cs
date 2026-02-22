
using Forext.CcyProvider.Database;
using Forext.CcyProvider.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;

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


        builder.Services.AddDbContext<CcyProviderDbContext>(options =>
        {
            options.UseSqlServer(DatabaseConfig.GetConnectionString(builder.Configuration));
        });


        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
