using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.Extensions.Options;
using Microsoft.FluentUI.AspNetCore.Components;


using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Identity.Client;
using SmartComponents;
using SmartComponents.Inference;
using SmartComponents.StaticAssets.Inference;

using Cosmos.Copilot.Options;
using Cosmos.Copilot.Services;

using AITGraph.Sdk;
using AITGraph.Sdk.Me.Profile;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.VectorData;

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureCosmosDBNoSQL;
using Microsoft.SemanticKernel.Data;

using AITrailblazer.net.Services;
using AITrailblazer.net.Models;
using aitrailblazer.Web;
using aitrailblazer.Web.Components;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.JSInterop;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Data;
using VectorStoreRAG;
using VectorStoreRAG.Options;
using Microsoft.Extensions.Hosting;


var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

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

string storageAccountName = secretClient.GetSecret("StorageAccountName").Value.Value;
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
string searchApiKey  = secretClient.GetSecret("searchApiKey").Value.Value;
string searchEndpoint  = secretClient.GetSecret("searchEndpoint").Value.Value;

string PhiEndpoint =secretClient.GetSecret("PhiEndpoint").Value.Value;
string PhiKey = secretClient.GetSecret("PhiKey").Value.Value;

string CodestralEndpoint =secretClient.GetSecret("CodestralEndpoint").Value.Value;
string CodestralKey = secretClient.GetSecret("CodestralKey").Value.Value;

string CohereCommandREndpoint =secretClient.GetSecret("CohereCommandREndpoint").Value.Value;
string CohereCommandRKey = secretClient.GetSecret("CohereCommandRKey").Value.Value;

// Fetch other configuration values from Key Vault or use Environment Variables as fallback
string azureOpenAIMaxCompletionTokens = secretClient.GetSecret("MaxCompletionTokens").Value.Value ?? Environment.GetEnvironmentVariable("MaxCompletionTokens") ?? string.Empty;
string azureEmbeddingsModelName03 = secretClient.GetSecret("AzureEmbeddingsModelName03").Value.Value ?? Environment.GetEnvironmentVariable("AzureEmbeddingsModelName03") ?? string.Empty;
string maxConversationTokens = secretClient.GetSecret("MaxConversationTokens").Value.Value ?? Environment.GetEnvironmentVariable("MaxConversationTokens") ?? string.Empty;
string maxEmbeddingTokens = secretClient.GetSecret("MaxEmbeddingTokens").Value.Value ?? Environment.GetEnvironmentVariable("MaxEmbeddingTokens") ?? string.Empty;
string azureOpenAIDALLEEndpoint01 = secretClient.GetSecret("AzureOpenAIDALLEEndpoint01").Value.Value ?? Environment.GetEnvironmentVariable("AzureOpenAIDALLEEndpoint01") ?? string.Empty;
string azureOpenAIDALLEKey01 = secretClient.GetSecret("AzureOpenAIDALLEKey01").Value.Value ?? Environment.GetEnvironmentVariable("AzureOpenAIDALLEKey01") ?? string.Empty;
string azureOpenAIDALLEModelName01 = secretClient.GetSecret("AzureOpenAIDALLEModelName01").Value.Value ?? Environment.GetEnvironmentVariable("AzureOpenAIDALLEModelName01") ?? string.Empty;
int azureEmbeddingsdDimensions  = 3072;

string azureCosmosDbEndpointUri ="https://aitrailblazer-asap.documents.azure.com:443/";
string AzureCosmosDBNoSQLConnectionString  = secretClient.GetSecret("AzureCosmosDBNoSQLConnectionString").Value.Value;
string AzureCosmosDBNoSQLDatabaseName  = "asapdb";

string chatContainerName ="chat";
string cacheContainerName ="cache";
string productContainerName= "products";
string organizerContainerName= "organizer";
string knowledgeBaseContainerName= "raghybrid"; // rag

string productDataSourceURI = "https://cosmosdbcosmicworks.blob.core.windows.net/cosmic-works-vectorized/product-text-3-large-1536.json";

string cacheSimilarityScore = "0.90";
string productMaxResults = "10";

// 1. Retrieve required permissions from Key Vault or fallback to configuration
var graphScopesString = secretClient.GetSecret("DownstreamApi-Scopes").Value.Value ?? builder.Configuration.GetSection("DownstreamApi:Scopes").Value;
var graphScopes = graphScopesString?.Split(' ') ?? new[] { "User.Read", "Mail.Read" };
// Fetch DownstreamApi configuration from Key Vault
string downstreamApiBaseUrl = secretClient.GetSecret("DownstreamApi-BaseUrl").Value.Value;
string downstreamApiScopes = secretClient.GetSecret("DownstreamApi-Scopes").Value.Value;

// 2. Add support for OpenId authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddMicrosoftIdentityWebApp(microsoftIdentityOptions =>
{
    microsoftIdentityOptions.Instance = builder.Configuration["AzureAd:Instance"];
    microsoftIdentityOptions.Domain = builder.Configuration["AzureAd:Domain"];
    microsoftIdentityOptions.TenantId = builder.Configuration["AzureAd:TenantId"];
    microsoftIdentityOptions.ClientId = builder.Configuration["AzureAd:ClientId"];
    microsoftIdentityOptions.ClientSecret = builder.Configuration["AzureAd:ClientSecret"];
    microsoftIdentityOptions.CallbackPath = builder.Configuration["AzureAd:CallbackPath"];
    microsoftIdentityOptions.SignedOutCallbackPath = builder.Configuration["AzureAd:SignedOutCallbackPath"];

    microsoftIdentityOptions.Events = new OpenIdConnectEvents
    {
        OnRedirectToIdentityProvider = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Redirecting to identity provider. Current path: {Path}", context.HttpContext.Request.Path);

            if (context.HttpContext.Request.Path.StartsWithSegments("/profile"))
            {
                context.ProtocolMessage.RedirectUri = $"{context.Request.Scheme}://{context.Request.Host}/profile";
                logger.LogInformation("Setting custom redirect URI: {Uri}", context.ProtocolMessage.RedirectUri);
            }

            return Task.CompletedTask;
        },
        OnAuthorizationCodeReceived = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Authorization code received");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Token validated");
            return Task.CompletedTask;
        },
        OnRemoteFailure = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError("Remote authentication failed: {Error}", context.Failure?.Message);
            context.Response.Redirect("/");
            context.HandleResponse();
            return Task.CompletedTask;
        }
    };
}, cookieScheme: CookieAuthenticationDefaults.AuthenticationScheme)
.EnableTokenAcquisitionToCallDownstreamApi(graphScopes)
.AddMicrosoftGraph(options =>
    {
        options.BaseUrl = downstreamApiBaseUrl;
        options.Scopes = graphScopes;
    }).AddInMemoryTokenCaches();

// 3. Require an authenticated user
builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();

builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});
builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

builder.Services.AddOutputCache();

// Initialize ParametersAzureService with the retrieved values
var parametersAzureService = new ParametersAzureService
{
    TenantId = tenantId,
    Authority = authority,
    ClientSecret = clientSecret,
    ClientId = clientId,

    //KernelMemoryServiceEndpoint = kernelMemoryServiceEndpoint,
    //KernelMemoryServiceApiKey = kernelMemoryServiceApiKey,
    
    StorageAccountName = storageAccountName,
    StorageConnectionString = storageConnectionString,
    StorageContainerName = storageContainerName,
    GITHUB_TOKEN = GTB_TOKEN,

    //GOOGLE_APPLICATION_CREDENTIALS_STR = GOOGLE_APPLICATION_CREDENTIALS_STR,
    //GOOGLE_API_ENDPOINT = GOOGLE_API_ENDPOINT,
    //GOOGLE_PROJECT_ID = GOOGLE_PROJECT_ID,
    //GOOGLE_LOCATION_ID = GOOGLE_LOCATION_ID,
    //GOOGLE_MODEL_ID = GOOGLE_MODEL_ID,
    //GOOGLE_API_KEY = GOOGLE_API_KEY,

    AzureOpenAIEndpoint03 = azureOpenAIEndpoint03,
    AzureOpenAIKey03 = azureOpenAIKey03,
    AzureEmbeddingsModelName03 = azureEmbeddingsModelName03,
    AzureEmbeddingsdDimensions = azureEmbeddingsdDimensions,

    AzureOpenAIModelName01 = azureOpenAIModelName01,
    AzureOpenAIModelName02 = azureOpenAIModelName02,
    AzureOpenAIModelName03 = azureOpenAIModelName03,

    AzureSearchApiKey = searchApiKey,
    AzureSearchEndpoint = searchEndpoint,

    PhiEndpoint = PhiEndpoint,
    PhiKey = PhiKey,

    CodestralEndpoint = CodestralEndpoint,
    CodestralKey = CodestralKey,

    CohereCommandREndpoint = CohereCommandREndpoint,
    CohereCommandRKey = CohereCommandRKey,

    AzureOpenAIMaxCompletionTokens = azureOpenAIMaxCompletionTokens,
    MaxConversationTokens = maxConversationTokens,
    MaxEmbeddingTokens = maxEmbeddingTokens,
    AzureOpenAIDALLEEndpoint01 = azureOpenAIDALLEEndpoint01,
    AzureOpenAIDALLEKey01 = azureOpenAIDALLEKey01,
    AzureOpenAIDALLEModelName01 = azureOpenAIDALLEModelName01,
    BingSearchApiKey = bingSearchApiKey,
    //ApplicationInsightsConnectionString = applicationInsightsConnectionString
};

builder.Services.AddSingleton(parametersAzureService);

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
            databaseName: AzureCosmosDBNoSQLDatabaseName ?? string.Empty,
            chatContainerName: chatContainerName ?? string.Empty,
            cacheContainerName: cacheContainerName ?? string.Empty,
            productContainerName: productContainerName ?? string.Empty,
            organizerContainerName: organizerContainerName ?? string.Empty,
            knowledgeBaseContainerName: knowledgeBaseContainerName ?? string.Empty,
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
            endpoint: azureOpenAIEndpoint03,
            completionDeploymentName: azureOpenAIModelName02,
            embeddingDeploymentName: azureEmbeddingsModelName03,
            apiKey: azureOpenAIKey03,
            endpointPhi: PhiEndpoint,
            apiKeyPhi: PhiKey,
            dimensions: azureEmbeddingsdDimensions,
            logger: logger
    );
});
string azureOpenAIChatDeploymentName  = "gpt-4o";
string azureEmbeddingDeploymentName  = "text-embedding-3-large";
string RagCollectionName = "ragcontent"; 

// Create a cancellation token and source to pass to the application service to allow them
// to request a graceful application shutdown.
CancellationTokenSource appShutdownCancellationTokenSource = new();
CancellationToken appShutdownCancellationToken = appShutdownCancellationTokenSource.Token;
builder.Services.AddKeyedSingleton("AppShutdown", appShutdownCancellationTokenSource);

// Register the kernel with the dependency injection container
// and add Chat Completion and Text Embedding Generation services.
var kernelBuilder = builder.Services.AddKernel();
    kernelBuilder.AddAzureOpenAIChatCompletion(
        azureOpenAIChatDeploymentName,
        azureOpenAIEndpoint03,
        new AzureCliCredential());
        
    kernelBuilder.AddAzureOpenAITextEmbeddingGeneration(
        azureEmbeddingDeploymentName,            // Positional argument
        azureOpenAIEndpoint03,                   // Positional argument
        new AzureCliCredential(),                // Positional argument
        dimensions: azureEmbeddingsdDimensions   // Named argument
    );

    kernelBuilder.AddAzureCosmosDBNoSQLVectorStoreRecordCollection<TextSnippet<string>>(
        RagCollectionName,
        AzureCosmosDBNoSQLConnectionString,
        AzureCosmosDBNoSQLDatabaseName);

RegisterServices<string>(builder, kernelBuilder);

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

builder.Services.AddScoped<AzureOpenAIHandler>();


//builder.Services.AddSingleton<SmartPasteInference, MyFormSmartPasteInference>();

// Register the SmartPasteInferenceFactory as Scoped
//builder.Services.AddScoped<ISmartPasteInferenceFactory, SmartPasteInferenceFactory>();

builder.Services.AddSmartComponents()
    .WithInferenceBackend<MyCustomInferenceBackend>();


// Configure the plugin path
var pluginsPath = Path.Combine(builder.Environment.ContentRootPath, "Plugins");
// Register PluginService
builder.Services.AddSingleton(new PluginService(pluginsPath));
// Register PromptyTemplateService
builder.Services.AddTransient<PromptyTemplateService>();
// Register AgentConfigurationService
builder.Services.AddSingleton<AgentConfigurationService>();
// Add plugins that can be used by kernels
// The plugins are added as singletons so that they can be used by multiple kernels
//builder.Services.AddSingleton<TimeInformation>();


// Ensure app shutdown source is registered if used
builder.Services.AddSingleton<CancellationTokenSource>();

builder.Services.AddTransient<KernelFunctionStrategies>(); 
builder.Services.AddTransient<KernelFunctionStrategyService>();
builder.Services.AddSingleton<KernelService>(provider =>
{
    var parametersAzureService = provider.GetRequiredService<ParametersAzureService>();
    return new KernelService(parametersAzureService);
});
// Register the IRequestAdapter
builder.Services.AddScoped<IRequestAdapter>(sp =>
{
    var tokenProvider = sp.GetRequiredService<ITokenAcquisition>();
    var accessTokenProvider = new TokenProviderAuthProvider(tokenProvider);
    var authenticationProvider = new KiotaAuthenticationProvider(accessTokenProvider);
    return new HttpClientRequestAdapter(authenticationProvider);
});

builder.Services.AddScoped<AITGraphApiClient>(sp =>
{
    var requestAdapter = sp.GetRequiredService<IRequestAdapter>();
    return new AITGraphApiClient(requestAdapter);
});

// Register AITGraphService with the correct dependencies
builder.Services.AddScoped<AITGraphService>(sp =>
{
    var consentHandler = sp.GetRequiredService<MicrosoftIdentityConsentAndConditionalAccessHandler>();
    var logger = sp.GetRequiredService<ILogger<AITGraphService>>();
    var tokenAcquisition = sp.GetRequiredService<ITokenAcquisition>();
    return new AITGraphService(consentHandler, logger, tokenAcquisition, graphScopes);
});

builder.Services.AddScoped<MasterTextSettingsService>();
builder.Services.AddScoped<CreativitySettingsService>();
builder.Services.AddScoped<WritingStyleService>();
builder.Services.AddScoped<ReadingLevelService>();
builder.Services.AddScoped<RelationSettingsService>();
builder.Services.AddScoped<TokenLabelService>();

// Register Services
builder.Services.AddScoped<UserIDsService>();
builder.Services.AddScoped<TimeFunctions>();
builder.Services.AddScoped<KQLFunctions>();
builder.Services.AddScoped<SKHandler>();


builder.Services.AddScoped<MasterTextSettingsService>();
builder.Services.AddScoped<CreativitySettingsService>();
builder.Services.AddScoped<WritingStyleService>();
builder.Services.AddScoped<ReadingLevelService>();
builder.Services.AddScoped<RelationSettingsService>();
builder.Services.AddScoped<TokenLabelService>();
builder.Services.AddScoped<AICopilotDescriptionService>();
builder.Services.AddScoped<AICopilotSettingsService>();
//builder.Services.AddScoped<WebSearchService>();
// Register BingNewsService as a singleton or scoped service
builder.Services.AddSingleton<BingNewsService>();
/*
builder.Services.AddHttpClient<GotenbergWSAppService>(client =>
{
    client.BaseAddress = new Uri("http://0.0.0.0:3000");
    //client.BaseAddress = new("http://apiservice");

    client.Timeout = TimeSpan.FromMinutes(5); // Total timeout for all retries combined
})
.AddStandardResilienceHandler(options =>
{
    // Retry policy configuration
    options.Retry.MaxRetryAttempts = 5; // Retry up to 5 times
    options.Retry.Delay = TimeSpan.FromSeconds(2); // Fixed delay between retries
    options.Retry.UseJitter = true; // Add jitter to avoid retry storms

    // Timeout policy configuration
    options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(60); // Timeout for individual attempts

    // Total request timeout (all retries combined)
    options.TotalRequestTimeout.Timeout = TimeSpan.FromMinutes(5);

    // Circuit breaker configuration
    options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(120); // At least double the attempt timeout
});
*/
builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });
// Register HttpClient for calling the Go API
builder.Services.AddHttpClient("GoApi", client =>
{
    client.BaseAddress = new Uri("http://ginapp"); // Use service discovery name 'ginapp'
    client.Timeout = TimeSpan.FromSeconds(30);
});


builder.Services.AddHttpClient<SECEdgarWSAppService>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://secedgarwsapp");
    });
    /*

builder.Services.AddHttpClient<SECEdgarWSAppService>(client =>
{
    client.BaseAddress = new("http://secedgarwsapp");

    client.Timeout = TimeSpan.FromMinutes(5); // Total timeout for all retries combined
})
.AddStandardResilienceHandler(options =>
{
    // Retry policy configuration
    options.Retry.MaxRetryAttempts = 5; // Retry up to 5 times
    options.Retry.Delay = TimeSpan.FromSeconds(2); // Fixed delay between retries
    options.Retry.UseJitter = true; // Add jitter to avoid retry storms

    // Timeout policy configuration
    options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(60); // Timeout for individual attempts

    // Total request timeout (all retries combined)
    options.TotalRequestTimeout.Timeout = TimeSpan.FromMinutes(5);

    // Circuit breaker configuration
    options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(120); // At least double the attempt timeout
});
*/
builder.Services.AddHttpClient<SECEdgarWSAppService>(client =>
{
     client.BaseAddress = new Uri("http://secedgarwsapp");
    //client.BaseAddress = new("http://apiservice");

    client.Timeout = TimeSpan.FromMinutes(5); // Total timeout for all retries combined
})
.AddStandardResilienceHandler(options =>
{
    // Retry policy configuration
    options.Retry.MaxRetryAttempts = 5; // Retry up to 5 times
    options.Retry.Delay = TimeSpan.FromSeconds(2); // Fixed delay between retries
    options.Retry.UseJitter = true; // Add jitter to avoid retry storms

    // Timeout policy configuration
    options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(60); // Timeout for individual attempts

    // Total request timeout (all retries combined)
    options.TotalRequestTimeout.Timeout = TimeSpan.FromMinutes(5);

    // Circuit breaker configuration
    options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(120); // At least double the attempt timeout
});

builder.Services.AddHttpClient<GoSECEdgarWSAppService>(client =>
{
    client.BaseAddress = new Uri("http://gosecedgarwsapp");
    //client.BaseAddress = new("http://apiservice");

    client.Timeout = TimeSpan.FromMinutes(5); // Total timeout for all retries combined
})
.AddStandardResilienceHandler(options =>
{
    // Retry policy configuration
    options.Retry.MaxRetryAttempts = 5; // Retry up to 5 times
    options.Retry.Delay = TimeSpan.FromSeconds(2); // Fixed delay between retries
    options.Retry.UseJitter = true; // Add jitter to avoid retry storms

    // Timeout policy configuration
    options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(60); // Timeout for individual attempts

    // Total request timeout (all retries combined)
    options.TotalRequestTimeout.Timeout = TimeSpan.FromMinutes(5);

    // Circuit breaker configuration
    options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(120); // At least double the attempt timeout
});

//builder.Services.AddScoped<SearchUrlPlugin>();
builder.Services.AddScoped<NewsFunctions>();

builder.Services.AddScoped<KernelAddPLugin>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

// Add logging
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

// Log the scopes being used
app.Lifetime.ApplicationStarted.Register(() =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Application started with graph scopes: {Scopes}", string.Join(", ", graphScopes));
});

app.Run();

static void RegisterServices<TKey>(WebApplicationBuilder builder, IKernelBuilder kernelBuilder)
    where TKey : notnull
{
    // Add a text search implementation that uses the registered vector store record collection for search.
    kernelBuilder.AddVectorStoreTextSearch<TextSnippet<TKey>>(
        new TextSearchStringMapper((result) =>
        {
            // Safely cast result and return the text
            if (result is TextSnippet<TKey> castResult)
            {
                return castResult.Text ?? string.Empty; // Fallback to empty string if Text is null
            }
            throw new InvalidCastException("Result is not of type TextSnippet<TKey>.");
        }),
        new TextSearchResultMapper((result) =>
        {
            // Safely cast result and create a TextSearchResult
            if (result is TextSnippet<TKey> castResult)
            {
                return new TextSearchResult(value: castResult.Text ?? string.Empty) // Fallback to empty string
                {
                    Name = castResult.ReferenceDescription ?? "No Description",
                    Link = castResult.ReferenceLink ?? "No Link"
                };
            }
            throw new InvalidCastException("Result is not of type TextSnippet<TKey>.");
        }));

    // Add the key generator and data loader to the dependency injection container.
    // Uncomment and use the appropriate key generator for your needs
    // builder.Services.AddSingleton<UniqueKeyGenerator<Guid>>(new UniqueKeyGenerator<Guid>(() => Guid.NewGuid()));
    // builder.Services.AddSingleton<UniqueKeyGenerator<string>>(new UniqueKeyGenerator<string>(() => Guid.NewGuid().ToString()));
    builder.Services.AddSingleton<IDataLoader, DataLoader<TKey>>();

    // Add the main service for this application.
    builder.Services.AddScoped<RAGChatService<TKey>>();
}
