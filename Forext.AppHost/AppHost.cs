using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// CcyProvider database
var postgres = builder.AddAzurePostgresFlexibleServer("postgres").RunAsContainer(container =>
{
    container.WithVolume("ccyprovider-db-data", "/var/lib/postgresql/data");
});

var ccyDb = postgres.AddDatabase("CurrencyPairsDb");

builder.AddProject<Projects.Forext_CcyProvider>("CcyProvider-API").WithReference(ccyDb);

builder.Build().Run();