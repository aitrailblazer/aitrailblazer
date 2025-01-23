var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.aitrailblazer_ApiService>("apiservice");
//
// Add the Golang app as a Dockerfile
/*
var gotenbergapp = builder.AddDockerfile("gotenberg", "./gotenberg")
    .WithHttpEndpoint(targetPort: 3000, port: 3000) // Bind host port 8000 to container port 8000
    .WithExternalHttpEndpoints()  // Make it accessible externally
    .WithOtlpExporter();          // Enable OpenTelemetry for observability
*/
// Add the Python app as a Dockerfile
/* 
var secedgarwsapp = builder.AddDockerfile("secedgarwsapp", "./sec-edgar-ws")
    .WithHttpEndpoint(targetPort: 8000, port: 8000) // Bind host port 8000 to container port 8000
    .WithExternalHttpEndpoints()  // Make it accessible externally
    .WithOtlpExporter();
   
*/
// Backend service from Dockerfile
var secedgarwsapp = builder.AddDockerfile(
    "secedgarwsapp", 
    "./sec-edgar-ws", // The directory where your backend project and Dockerfile are located
    "Dockerfile"
).WithHttpEndpoint(8000, 8000); // Map container's port 80 to host's port 5000

var gosecedgarwsapp = builder.AddDockerfile(
    "gosecedgarwsapp", 
    "./go-sec-edgar-ws", // The directory where your backend project and Dockerfile are located
    "Dockerfile"
).WithHttpEndpoint(8001, 8001); // Map container's port 80 to host's port 5000

builder.AddProject<Projects.aitrailblazer_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    //.WithReference(secedgarwsapp);
    //.WaitFor(gotenbergapp)
    //.WaitFor(gosecedgarwsapp)
    .WithReference(gosecedgarwsapp.GetEndpoint("http"))
    .WithReference(secedgarwsapp.GetEndpoint("http"));
    //.WaitFor(secedgarwsapp);      // Ensure webfrontend waits for uvicornapp

//
builder.Build().Run();
//
