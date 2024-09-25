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

using aitrailblazer.Web;
using aitrailblazer.Web.Components;
using AITrailBlazer.Web.Services;

var builder = WebApplication.CreateBuilder(args);

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