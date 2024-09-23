using Microsoft.FluentUI.AspNetCore.Components;
using aitrailblazer.Web;
using aitrailblazer.Web.Components;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Azure.Identity;
using Microsoft.Graph;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// 1. Retrieve required permissions from appsettings
var initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ');

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAd", options); // Bind configuration from appsettings.json or environment variables
        //options.Prompt = "consent"; // Prompt the user for consent during authentication

        // Save tokens in the session for later use
        options.SaveTokens = true;

        // Handle events during the authentication lifecycle
        options.Events = new OpenIdConnectEvents
        {
            // This event is triggered when the token is validated
            OnTokenValidated = context =>
            {
                // Add custom logic after the token is validated (optional)
                return Task.CompletedTask;
            },
            // This event is triggered when authentication fails
            OnAuthenticationFailed = context =>
            {
                // Handle the failure and redirect to a custom error page
                context.HandleResponse();
                context.Response.Redirect("/Error?message=" + context.Exception.Message);
                return Task.CompletedTask;
            },
            
        };
    })
    .EnableTokenAcquisitionToCallDownstreamApi(builder.Configuration["DownstreamApi:Scopes"]?.Split(' '))
    .AddMicrosoftGraph(builder.Configuration.GetSection("DownstreamApi"))
    .AddInMemoryTokenCaches();


// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor
// 9. Add Microsoft Identity UI pages that provide user
// sign-in and sign-out support
builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();
// Add authorization services
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

builder.Services.AddOutputCache();

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapControllers();
//app.MapBlazorHub();
app.MapRazorPages();
app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
