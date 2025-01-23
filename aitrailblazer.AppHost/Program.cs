var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.aitrailblazer_ApiService>("apiservice");
//
// Backend service from Dockerfile
var gotenberg = builder.AddDockerfile(
    "gotenberg", 
    "./gotenberg", // The directory where your backend project and Dockerfile are located
    "Dockerfile"
).WithHttpEndpoint(3000, 3000); // Map container's port 80 to host's port 5000

// Backend service from Dockerfile
var secedgarwsapp = builder.AddDockerfile(
    "secedgarwsapp", 
    "./sec-edgar-ws", // The directory where your backend project and Dockerfile are located
    "Dockerfile"
).WithHttpEndpoint(8000, 8000); // Map container's port 80 to host's port 5000

//var gosecedgarwsapp = builder.AddDockerfile(
//    "gosecedgarwsapp", 
//    "./go-sec-edgar-ws", // The directory where your backend project and Dockerfile are located
//    "Dockerfile"
//).WithHttpEndpoint(8001, 8001); // Map container's port 80 to host's port 5000

builder.AddProject<Projects.aitrailblazer_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WithReference(gotenberg.GetEndpoint("http"))
    //.WithReference(gosecedgarwsapp.GetEndpoint("http"))
    .WithReference(secedgarwsapp.GetEndpoint("http"));

//
builder.Build().Run();
//
