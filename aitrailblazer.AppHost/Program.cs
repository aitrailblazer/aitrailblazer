var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.aitrailblazer_ApiService>("apiservice");
//
builder.AddProject<Projects.aitrailblazer_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);
//
builder.Build().Run();
//
//
//