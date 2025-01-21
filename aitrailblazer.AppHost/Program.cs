var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.aitrailblazer_ApiService>("apiservice");
//
// Add the Golang app as a Dockerfile
var gotenbergapp = builder.AddDockerfile("gotenberg", "./gotenberg")
    .WithHttpEndpoint(targetPort: 3000, port: 3000) // Bind host port 8000 to container port 8000
    .WithExternalHttpEndpoints()  // Make it accessible externally
    .WithOtlpExporter();          // Enable OpenTelemetry for observability

// Add the Python app as a Dockerfile
var secedgarwsapp = builder.AddDockerfile("secedgarwsapp", "./sec-edgar-ws")
    //.WithHttpEndpoint(targetPort: 8000, env: "PORT") // Expose HTTP endpoint based on PORT environment variable
    .WithHttpEndpoint(targetPort: 8000, port: 8000) // Bind host port 8000 to container port 8000
    .WithExternalHttpEndpoints()  // Make it accessible externally
    .WithOtlpExporter();          // Enable OpenTelemetry for observability
// Add the Golang app as a Dockerfile
var gosecedgarwsapp = builder.AddDockerfile("gosecedgarwsapp", "./go-sec-edgar-ws")
    .WithHttpEndpoint(targetPort: 8001, port: 8001) // Bind host port 8000 to container port 8000
    .WithExternalHttpEndpoints()  // Make it accessible externally
    .WithOtlpExporter();          // Enable OpenTelemetry for observability

builder.AddProject<Projects.aitrailblazer_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(gotenbergapp)
    .WaitFor(gosecedgarwsapp)
    .WaitFor(secedgarwsapp);      // Ensure webfrontend waits for uvicornapp

//
builder.Build().Run();
//
//
//
//