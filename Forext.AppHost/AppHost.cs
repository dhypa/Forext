var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FraudMon_TransactionApi>("fraudmon-transactionapi");

builder.AddAzureFunctionsProject<Projects.FraudMon_FraudProcessorFunction>("fraudmon-fraudprocessorfunction");

builder.Build().Run();
