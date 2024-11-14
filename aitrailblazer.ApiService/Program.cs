using CosmosContainers.ApiService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.Identity.Web;

using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions;
using Swashbuckle.AspNetCore.SwaggerGen;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Cosmos.Copilot.Options;
using Cosmos.Copilot.Services;

var builder = WebApplication.CreateBuilder(args);
// Add Key Vault Configuration

var keyVaultUrl = "https://AITAzureKeyVault.vault.azure.net/";
var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
{
    ManagedIdentityClientId = "b86031a5-3a0e-4d84-ad1e-9f8f856e08bd"
});
var secretClient = new SecretClient(new Uri(keyVaultUrl), credential);

// Fetch secrets from Key Vault
builder.Configuration["AzureAd:Instance"] = secretClient.GetSecret("AzureAd-Instance").Value.Value;
builder.Configuration["AzureAd:Domain"] = secretClient.GetSecret("AzureAd-Domain").Value.Value;
builder.Configuration["AzureAd:TenantId"] = secretClient.GetSecret("AzureAd-TenantId").Value.Value;
builder.Configuration["AzureAd:ClientId"] = secretClient.GetSecret("AzureAd-ClientId").Value.Value;
builder.Configuration["AzureAd:ClientSecret"] = secretClient.GetSecret("AzureAd-ClientSecret").Value.Value;
builder.Configuration["AzureAd:CallbackPath"] = secretClient.GetSecret("AzureAd-CallbackPath").Value.Value;
builder.Configuration["AzureAd:SignedOutCallbackPath"] = secretClient.GetSecret("AzureAd-SignedOutCallbackPath").Value.Value;

var authority = "";
var clientId = builder.Configuration["AzureAd:ClientId"];
var clientSecret = builder.Configuration["AzureAd:ClientSecret"];
var tenantId = builder.Configuration["AzureAd:TenantId"];

string storageConnectionString = secretClient.GetSecret("StorageConnectionString").Value.Value;
string storageContainerName = secretClient.GetSecret("StorageContainerName").Value.Value;
string GTB_TOKEN = secretClient.GetSecret("GTB-TOKEN").Value.Value;

// Fetch other secrets as needed
string azureOpenAIModelName01 = secretClient.GetSecret("AzureOpenAIModelName01").Value.Value;
string azureOpenAIModelName02 = secretClient.GetSecret("AzureOpenAIModelName02").Value.Value;
string azureOpenAIModelName03 = secretClient.GetSecret("AzureOpenAIModelName03").Value.Value;
string azureOpenAIEndpoint03 = secretClient.GetSecret("AzureOpenAIEndpoint03").Value.Value;
string azureOpenAIKey03 = secretClient.GetSecret("AzureOpenAIKey03").Value.Value;
string bingSearchApiKey  = secretClient.GetSecret("BING-API-KEY").Value.Value;

// Fetch other configuration values from Key Vault or use Environment Variables as fallback
string azureOpenAIMaxCompletionTokens = secretClient.GetSecret("MaxCompletionTokens").Value.Value ?? Environment.GetEnvironmentVariable("MaxCompletionTokens") ?? string.Empty;
string azureEmbeddingsModelName03 = secretClient.GetSecret("AzureEmbeddingsModelName03").Value.Value ?? Environment.GetEnvironmentVariable("AzureEmbeddingsModelName03") ?? string.Empty;
string maxConversationTokens = secretClient.GetSecret("MaxConversationTokens").Value.Value ?? Environment.GetEnvironmentVariable("MaxConversationTokens") ?? string.Empty;
string maxEmbeddingTokens = secretClient.GetSecret("MaxEmbeddingTokens").Value.Value ?? Environment.GetEnvironmentVariable("MaxEmbeddingTokens") ?? string.Empty;
string azureOpenAIDALLEEndpoint01 = secretClient.GetSecret("AzureOpenAIDALLEEndpoint01").Value.Value ?? Environment.GetEnvironmentVariable("AzureOpenAIDALLEEndpoint01") ?? string.Empty;
string azureOpenAIDALLEKey01 = secretClient.GetSecret("AzureOpenAIDALLEKey01").Value.Value ?? Environment.GetEnvironmentVariable("AzureOpenAIDALLEKey01") ?? string.Empty;
string azureOpenAIDALLEModelName01 = secretClient.GetSecret("AzureOpenAIDALLEModelName01").Value.Value ?? Environment.GetEnvironmentVariable("AzureOpenAIDALLEModelName01") ?? string.Empty;

string azureCosmosDbEndpointUri ="https://aitrailblazer-asap.documents.azure.com:443/";
string databaseId = "asapdb";//"ToDoList";

string chatContainerName ="chat";
string cacheContainerName ="cache";
string productContainerName= "products";
string organizerContainerName= "organizer";

string productDataSourceURI = "https://cosmosdbcosmicworks.blob.core.windows.net/cosmic-works-vectorized/product-text-3-large-1536.json";

string cacheSimilarityScore = "0.90";
string productMaxResults = "10";

// 1. Retrieve required permissions from Key Vault or fallback to configuration
var graphScopesString = secretClient.GetSecret("DownstreamApi-Scopes").Value.Value ?? builder.Configuration.GetSection("DownstreamApi:Scopes").Value;
var graphScopes = graphScopesString?.Split(' ') ?? new[] { "User.Read", "Mail.Read" };
// Fetch DownstreamApi configuration from Key Vault
string downstreamApiBaseUrl = secretClient.GetSecret("DownstreamApi-BaseUrl").Value.Value;
string downstreamApiScopes = secretClient.GetSecret("DownstreamApi-Scopes").Value.Value;

// Add service defaults & Aspire components.
// Assuming 'AddServiceDefaults' is an extension method you have.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddSingleton<CosmosDbService>((provider) =>
{
    var cosmosDbOptions = provider.GetRequiredService<IOptions<CosmosDb>>();
    if (cosmosDbOptions is null)
    {
        throw new ArgumentException($"{nameof(IOptions<CosmosDb>)} was not resolved through dependency injection.");
    }
    else
    {
        var logger = provider.GetRequiredService<ILogger<CosmosDbService>>(); // Retrieve the logger

        return new CosmosDbService(
            endpoint: azureCosmosDbEndpointUri ?? string.Empty,
            databaseName: databaseId ?? string.Empty,
            chatContainerName: chatContainerName ?? string.Empty,
            cacheContainerName: cacheContainerName ?? string.Empty,
            productContainerName: productContainerName ?? string.Empty,
            organizerContainerName: organizerContainerName ?? string.Empty,
            productDataSourceURI: productDataSourceURI ?? string.Empty,
            logger: logger // Pass the logger to the constructor
        );
    }
});


builder.Services.AddScoped<SemanticKernelService>((provider) =>
{
    var logger = provider.GetRequiredService<ILogger<SemanticKernelService>>(); // Resolve logger

    var semanticKernelOptions = provider.GetRequiredService<IOptions<OpenAi>>().Value;
    if (semanticKernelOptions == null)
    {
        throw new ArgumentException($"{nameof(IOptions<OpenAi>)} was not resolved through dependency injection.");
    }

    return new SemanticKernelService(
            endpoint: azureOpenAIEndpoint03 ?? String.Empty,
            completionDeploymentName: azureOpenAIModelName02 ?? String.Empty,
            embeddingDeploymentName: azureEmbeddingsModelName03 ?? String.Empty,
            apiKey: azureOpenAIKey03 ?? String.Empty,
            logger: logger
    );
});

builder.Services.AddScoped<ChatService>((provider) =>
{
    var chatOptions = provider.GetRequiredService<IOptions<Chat>>()?.Value 
                      ?? throw new ArgumentException($"{nameof(IOptions<Chat>)} was not resolved through dependency injection.");
    var cosmosDbService = provider.GetRequiredService<CosmosDbService>();
    var semanticKernelService = provider.GetRequiredService<SemanticKernelService>();
    var logger = provider.GetRequiredService<ILogger<ChatService>>();

    return new ChatService(
        cosmosDbService: cosmosDbService,
        semanticKernelService: semanticKernelService,
        maxConversationTokens: chatOptions.MaxConversationTokens,
        cacheSimilarityScore: chatOptions.CacheSimilarityScore,
        productMaxResults: chatOptions.ProductMaxResults,
        logger: logger
    );
});
builder.Services.AddProblemDetails();
// Configure the API versioning properties of the project.
builder.Services.AddApiVersioning(
                    options =>
                    {
                        // reporting api versions will return the headers
                        // "api-supported-versions" and "api-deprecated-versions"
                        options.ReportApiVersions = true;

                        //options.Policies.Sunset( 0.9 )
                        //                .Effective( DateTimeOffset.Now.AddDays( 60 ) )
                        //                .Link( "policy.html" )
                        //                    .Title( "Versioning Policy" )
                        //                    .Type( "text/html" );
                    } );
                //.AddMvc()
                //.AddApiExplorer(
                //    options =>
                //    {
                        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                //        options.GroupNameFormat = "'v'VVV";

                        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                        // can also be used to control the format of the API version in route templates
                        //options.SubstituteApiVersionInUrl = true;
                //    } );
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.EnableAnnotations();
  options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ASAP Knowledge Navigator OpenAPI",
        Version = "v1",
        Description = "A robust API providing Cosmos DB-backed data management and retrieval for the AITrailblazer ASAP Knowledge Navigator, supporting advanced chat sessions and knowledge organization.",
         Contact = new OpenApiContact
        {
            Name = "Developer Support",
            Email = "support@aitrailblazer.com",
            Url = new Uri("https://aitrailblazer.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "ASAP Knowledge Navigator OpenAPI v1");
        options.RoutePrefix = "swagger";
    });
    
}

// Map API endpoints
app.MapItemsApi();

// Map default endpoints if necessary
// app.MapDefaultEndpoints(); // Uncomment if you have default endpoints to map.

app.Run();
