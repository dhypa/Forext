var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Forext_CcyProvider>("CcyProvider-API");

var postgres = builder.AddPostgres("CcyProvider-DB");

//builder.AddAzureFunctionsProject<Projects.FraudMon_FraudProcessorFunction>("fraudmon-fraudprocessorfunction");

builder.Build().Run();
