using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// CcyProvider database
var postgres = builder.AddAzurePostgresFlexibleServer("postgres");
var ccyDb = postgres.AddDatabase("CurrencyPairsDb");
if (builder.Environment.IsDevelopment())
{
    postgres.RunAsContainer(container =>
    {
        container.WithVolume("ccyprovider-db-data", "/var/lib/postgresql/data");
    });
}

builder.AddProject<Projects.Forext_CcyProvider>("CcyProvider-API").WithReference(ccyDb);

//builder.AddAzureFunctionsProject<Projects.FraudMon_FraudProcessorFunction>("fraudmon-fraudprocessorfunction");

builder.Build().Run();
