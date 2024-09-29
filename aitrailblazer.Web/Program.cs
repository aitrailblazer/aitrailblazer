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
using AITGraph.Sdk;
using AITGraph.Sdk.Me.Profile;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server;
using aitrailblazer.net.Services;
using aitrailblazer.net.Models;
using aitrailblazer.Web;
using aitrailblazer.Web.Components;
using AITrailBlazer.Web.Services;

var builder = WebApplication.CreateBuilder(args);
var authority = "";//Environment.GetEnvironmentVariable("authority") ?? string.Empty;

var clientId = Environment.GetEnvironmentVariable("ClientId") ?? string.Empty;
var clientSecret = Environment.GetEnvironmentVariable("ClientSecret") ?? string.Empty;
var tenantId = Environment.GetEnvironmentVariable("tenantId") ?? string.Empty;

string storageConnectionString = Environment.GetEnvironmentVariable("StorageConnectionString") ?? string.Empty;
string storageContainerName = Environment.GetEnvironmentVariable("StorageContainerName") ?? string.Empty;

string GITHUB_TOKEN = Environment.GetEnvironmentVariable("GITHUB_TOKEN") ?? string.Empty;

// gpt-4
string azureOpenAIModelName01 = Environment.GetEnvironmentVariable("AzureOpenAIModelName01") ?? string.Empty;

// gpt-4o-mini string modelId = "gpt-4o-mini";
string azureOpenAIModelName02 = Environment.GetEnvironmentVariable("AzureOpenAIModelName02") ?? string.Empty;

// gpt-4o string modelId = "gpt-4o";
string azureOpenAIModelName03 = Environment.GetEnvironmentVariable("AzureOpenAIModelName03") ?? string.Empty;

string azureOpenAIEndpoint03 = Environment.GetEnvironmentVariable("AzureOpenAIEndpoint03") ?? string.Empty;
string azureOpenAIKey03 = Environment.GetEnvironmentVariable("AzureOpenAIKey03") ?? string.Empty;

string azureOpenAIMaxCompletionTokens = Environment.GetEnvironmentVariable("MaxCompletionTokens") ?? string.Empty;
string azureEmbeddingsModelName03 = Environment.GetEnvironmentVariable("AzureEmbeddingsModelName03") ?? string.Empty;
string maxConversationTokens = Environment.GetEnvironmentVariable("MaxConversationTokens") ?? string.Empty;
string maxEmbeddingTokens = Environment.GetEnvironmentVariable("MaxEmbeddingTokens") ?? string.Empty;
string azureOpenAIDALLEEndpoint01 = Environment.GetEnvironmentVariable("AzureOpenAIDALLEEndpoint01") ?? string.Empty;
string azureOpenAIDALLEKey01 = Environment.GetEnvironmentVariable("AzureOpenAIDALLEKey01") ?? string.Empty;
string azureOpenAIDALLEModelName01 = Environment.GetEnvironmentVariable("AzureOpenAIDALLEModelName01") ?? string.Empty;
string bingSearchKey = Environment.GetEnvironmentVariable("BingSearchKey") ?? string.Empty;

// 1. Retrieve required permissions from appsettings
var graphScopes = builder.Configuration.GetSection("DownstreamApi:Scopes").Get<string[]>();

if (graphScopes == null || graphScopes.Length == 0)
{
    Console.WriteLine("Warning: Graph scopes are not configured properly in appsettings.json. Using default scopes.");
    graphScopes = new[] { "User.Read", "Mail.Read" };
}

// 2. Add support for OpenId authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddMicrosoftIdentityWebApp(microsoftIdentityOptions =>
{
    builder.Configuration.Bind("AzureAd", microsoftIdentityOptions);
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
.AddMicrosoftGraph(builder.Configuration.GetSection("DownstreamApi"))
.AddInMemoryTokenCaches();

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

    StorageConnectionString = storageConnectionString,
    StorageContainerName = storageContainerName,
    GITHUB_TOKEN = GITHUB_TOKEN,

    //GOOGLE_APPLICATION_CREDENTIALS_STR = GOOGLE_APPLICATION_CREDENTIALS_STR,
    //GOOGLE_API_ENDPOINT = GOOGLE_API_ENDPOINT,
    //GOOGLE_PROJECT_ID = GOOGLE_PROJECT_ID,
    //GOOGLE_LOCATION_ID = GOOGLE_LOCATION_ID,
    //GOOGLE_MODEL_ID = GOOGLE_MODEL_ID,
    //GOOGLE_API_KEY = GOOGLE_API_KEY,

    AzureOpenAIEndpoint03 = azureOpenAIEndpoint03,
    AzureOpenAIKey03 = azureOpenAIKey03,
    AzureEmbeddingsModelName03 = azureEmbeddingsModelName03,

    AzureOpenAIModelName01 = azureOpenAIModelName01,
    AzureOpenAIModelName02 = azureOpenAIModelName02,
    AzureOpenAIModelName03 = azureOpenAIModelName03,

    AzureOpenAIMaxCompletionTokens = azureOpenAIMaxCompletionTokens,
    MaxConversationTokens = maxConversationTokens,
    MaxEmbeddingTokens = maxEmbeddingTokens,
    AzureOpenAIDALLEEndpoint01 = azureOpenAIDALLEEndpoint01,
    AzureOpenAIDALLEKey01 = azureOpenAIDALLEKey01,
    AzureOpenAIDALLEModelName01 = azureOpenAIDALLEModelName01,
    BingSearchKey = bingSearchKey,
    //ApplicationInsightsConnectionString = applicationInsightsConnectionString
};

builder.Services.AddSingleton(parametersAzureService);

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
builder.Services.AddScoped<AzureOpenAIHandler>();

builder.Services.AddScoped<MasterTextSettingsService>();
builder.Services.AddScoped<CreativitySettingsService>();
builder.Services.AddScoped<WritingStyleService>();
builder.Services.AddScoped<ReadingLevelService>();
builder.Services.AddScoped<RelationSettingsService>();
builder.Services.AddScoped<TokenLabelService>();

// Register Services
builder.Services.AddScoped<TimeZoneService>();
builder.Services.AddScoped<UserTimeZoneService>();
builder.Services.AddScoped<TimeFunctions>();
builder.Services.AddScoped<KernelSetup>();

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