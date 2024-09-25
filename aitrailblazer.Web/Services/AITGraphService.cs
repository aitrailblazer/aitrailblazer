using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using AITGraph.Sdk;
using AITGraph.Sdk.Models;
using AITGraph.Sdk.Me;
using AITGraph.Sdk.Me.Profile;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Identity.Client;

namespace AITrailBlazer.Web.Services
{
    public class AuthenticationRequiredException : Exception
    {
        public AuthenticationRequiredException(string message) : base(message) { }
    }

    public class AITGraphService
    {
        private readonly MicrosoftIdentityConsentAndConditionalAccessHandler _consentHandler;
        private readonly ILogger<AITGraphService> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private AITGraphApiClient _graphClient;
        private readonly string[] _graphScopes;

        // Default scopes
        private static readonly string[] DefaultScopes = new[] { "User.Read", "Mail.Read", "Calendars.Read" };

        public AITGraphService(
            MicrosoftIdentityConsentAndConditionalAccessHandler consentHandler,
            ILogger<AITGraphService> logger,
            ITokenAcquisition tokenAcquisition,
            string[] graphScopes = null)
        {
            _consentHandler = consentHandler ?? throw new ArgumentNullException(nameof(consentHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tokenAcquisition = tokenAcquisition ?? throw new ArgumentNullException(nameof(tokenAcquisition));
            
            _graphScopes = (graphScopes != null && graphScopes.Length > 0) ? graphScopes : DefaultScopes;
            
            _logger.LogInformation($"AITGraphService initialized with scopes: {string.Join(", ", _graphScopes)}");
        }

        private AITGraphApiClient GraphClient
        {
            get
            {
                if (_graphClient == null)
                {
                    var authProvider = new TokenAcquisitionAuthenticationProvider(_tokenAcquisition, _graphScopes, _logger);
                    var requestAdapter = new HttpClientRequestAdapter(authProvider);

                    _graphClient = new AITGraphApiClient(requestAdapter);
                }
                return _graphClient;
            }
        }

        public async Task<Profile> GetCurrentUserProfileAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch user profile");
                var profile = await GraphClient.Me.Profile.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Expand = new[] { 
                        "names",
                        "emails",
                        "skills",
                        "educationalActivities"
                    };
                });

                _logger.LogInformation("User profile fetched successfully");
                return profile ?? new Profile();
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while fetching user profile.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (ApiException ex) when (ex.Message.Contains("Continuous access evaluation resulted in claims challenge"))
            {
                _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge.");
                string claimChallenge = ex.ResponseHeaders["WWW-Authenticate"].FirstOrDefault();
                _consentHandler.ChallengeUser(_graphScopes, claimChallenge);
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user profile.");
                throw;
            }
        }

        public async Task<UserAccountInformation?> GetUserAccountInformationAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch user account information");
                var accountInfo = await GraphClient.Me.Profile.Account.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Select = new[] { 
                        "ageGroup",
                        "countryCode",
                        "preferredLanguageTag",
                        "userPrincipalName",
                        "allowedAudiences",
                        "createdDateTime",
                        "lastModifiedDateTime",
                        "id"
                    };
                });

                if (accountInfo?.Value != null && accountInfo.Value.Count > 0)
                {
                    _logger.LogInformation("User account information fetched successfully");
                    return accountInfo.Value[0];
                }
                else
                {
                    _logger.LogWarning("No user account information found");
                    return null;
                }
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while fetching user account information.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (ApiException ex) when (ex.Message.Contains("Continuous access evaluation resulted in claims challenge"))
            {
                _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge.");
                string claimChallenge = ex.ResponseHeaders["WWW-Authenticate"].FirstOrDefault();
                _consentHandler.ChallengeUser(_graphScopes, claimChallenge);
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user account information.");
                throw;
            }
        }

        public async Task<List<Event>> GetCalendarEventsAsync(int count = 10)
        {
            try
            {
                _logger.LogInformation($"Attempting to fetch {count} calendar events");
                var eventsResponse = await GraphClient.Me.Calendar.Events.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Top = count;
                    requestConfiguration.QueryParameters.Orderby = new[] { "start/dateTime" };
                    requestConfiguration.QueryParameters.Select = new[] { 
                        "subject",
                        "organizer",
                        "start",
                        "end"
                    };
                });

                _logger.LogInformation($"Successfully fetched {eventsResponse.Value?.Count ?? 0} calendar events");
                return eventsResponse.Value?.ToList() ?? new List<Event>();
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while fetching calendar events.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (ApiException ex) when (ex.Message.Contains("Continuous access evaluation resulted in claims challenge"))
            {
                _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge.");
                string claimChallenge = ex.ResponseHeaders["WWW-Authenticate"].FirstOrDefault();
                _consentHandler.ChallengeUser(_graphScopes, claimChallenge);
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching calendar events.");
                throw;
            }
        }

        public async Task<List<Message>> GetRecentMessagesAsync(int count = 10)
        {
            try
            {
                _logger.LogInformation($"Attempting to fetch {count} recent messages");
                var messagesResponse = await GraphClient.Me.Messages.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Top = count;
                    requestConfiguration.QueryParameters.Orderby = new[] { "receivedDateTime desc" };
                    requestConfiguration.QueryParameters.Select = new[] { 
                        "subject",
                        "from",
                        "receivedDateTime"
                    };
                });

                _logger.LogInformation($"Successfully fetched {messagesResponse.Value?.Count ?? 0} messages");
                return messagesResponse.Value?.ToList() ?? new List<Message>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching recent messages.");
                throw;
            }
        }
    }

    public class TokenAcquisitionAuthenticationProvider : IAuthenticationProvider
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly string[] _scopes;
        private readonly ILogger _logger;
        private const string OpenIdConnectAuthenticationScheme = "OpenIdConnect";

        public TokenAcquisitionAuthenticationProvider(ITokenAcquisition tokenAcquisition, string[] scopes, ILogger logger)
        {
            _tokenAcquisition = tokenAcquisition ?? throw new ArgumentNullException(nameof(tokenAcquisition));
            _scopes = scopes ?? throw new ArgumentNullException(nameof(scopes));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (_scopes.Length == 0)
            {
                throw new ArgumentException("Scopes array cannot be empty", nameof(scopes));
            }
        }

        public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation($"Attempting to acquire token for scopes: {string.Join(", ", _scopes)}");
                var token = await _tokenAcquisition.GetAccessTokenForUserAsync(_scopes, authenticationScheme: OpenIdConnectAuthenticationScheme);
                request.Headers.Add("Authorization", $"Bearer {token}");
                _logger.LogInformation("Token acquired and added to request headers");
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred during token acquisition.");
                throw new AuthenticationRequiredException("User authentication is required.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during token acquisition.");
                throw;
            }
        }
    }
}
