var builder = DistributedApplication.CreateBuilder(args);

//        <ProjectReference Include="..\..\Shared\StatefulReconnection\StatefulReconnection.csproj" IsAspireProjectResource="false" />

//var statefulReconnection = builder.AddProject<Projects.StatefulReconnection>("statefulreconnection");

//   <ProjectReference Include="..\aitrailblazer.ApiService\aitrailblazer.ApiService.csproj" />

var apiService = builder.AddProject<Projects.aitrailblazer_ApiService>("apiservice");

builder.AddProject<Projects.aitrailblazer_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
