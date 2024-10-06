// AITGraphService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using AITGraph.Sdk;
using AITGraph.Sdk.Models;
using AITGraph.Sdk.Models.ODataErrors;
using AITGraph.Sdk.Models;
using AITGraph.Sdk.Me;
using AITGraph.Sdk.Me.Contacts;
using AITGraph.Sdk.Me.Profile;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Identity.Client;
using System.Text.Json;
using System.IO; // For StreamReader
namespace AITrailBlazer.Web.Services
{

    public class AITGraphService
    {
        private readonly MicrosoftIdentityConsentAndConditionalAccessHandler _consentHandler;
        private readonly ILogger<AITGraphService> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private AITGraphApiClient _graphClient;
        private readonly string[] _graphScopes;
        private HttpClientRequestAdapter _requestAdapter;

        // Default scopes
        private static readonly string[] DefaultScopes = new[]
           {
            "User.Read",
            "Mail.Read",
            "Calendars.Read",
            "Files.Read",           // For reading files
            "Files.ReadWrite",      // For reading and writing files
            "Files.Read.All",       // For reading all files the user can access
            "Files.ReadWrite.All",   // For reading and writing all files the user can access
            "Contacts.Read",
            "Contacts.ReadWrite" // Added Contacts.ReadWrite scope for write operations
  
        };
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
                    // Assigning to _requestAdapter field
                    _requestAdapter = new HttpClientRequestAdapter(authProvider);

                    _graphClient = new AITGraphApiClient(_requestAdapter);
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
       public async Task<Stream> GetUserPhotoAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch user profile photo");

                // Ensure _requestAdapter is initialized
                if (_requestAdapter == null)
                {
                    // Initialize GraphClient to ensure _requestAdapter is set
                    var client = GraphClient;
                }

                var requestUrl = $"{_requestAdapter.BaseUrl}/me/photo/$value";

                var requestInfo = new RequestInformation
                {
                    HttpMethod = Method.GET,
                    UrlTemplate = requestUrl,
                    PathParameters = new Dictionary<string, object>()
                };

                requestInfo.Headers.Add("Accept", "image/*");

                var responseStream = await _requestAdapter.SendPrimitiveAsync<Stream>(
                    requestInfo,
                    errorMapping: null,
                    cancellationToken: CancellationToken.None
                );

                if (responseStream != null)
                {
                    _logger.LogInformation($"User profile photo fetched successfully,{requestUrl}");
                    return responseStream;
                }
                else
                {
                    _logger.LogWarning("User profile photo not found");
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
                _logger.LogError(ex, "User challenge occurred while fetching user profile photo.");
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
                _logger.LogError(ex, "An error occurred while fetching user profile photo.");
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
        public async Task<List<Contact>> GetUserContactsAsync(int count = 10)
        {
            try
            {
                _logger.LogInformation($"Attempting to fetch {count} contacts");

                var contactsResponse = await GraphClient.Me.Contacts.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Top = count;
                    requestConfiguration.QueryParameters.Orderby = new[] { "displayName" };
                    requestConfiguration.QueryParameters.Select = new[]
                    {
                        "id",
                        "displayName",
                        "givenName",
                        "surname",
                        "emailAddresses",
                        "companyName",
                        "jobTitle",
                        "officeLocation",
                        "imAddresses",
                        "birthday",
                        "nickName",
                        "middleName",
                        "personalNotes",
                        "spouseName",
                        "department",
                        "assistantName",
                        "yomiGivenName",
                        "yomiSurname",
                        "yomiCompanyName",
                        "profession",
                        "title",
                        "children",
                        "phones",
                        "postalAddresses"
                    };
                });

                _logger.LogInformation($"Successfully fetched {contactsResponse.Value?.Count ?? 0} contacts");
                return contactsResponse.Value?.ToList() ?? new List<Contact>();
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred while fetching contacts.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while fetching contacts.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred while fetching contacts.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (ApiException ex)
            {
                // Check for claims challenge in the WWW-Authenticate header
                if (ex.ResponseHeaders != null && ex.ResponseHeaders.TryGetValue("WWW-Authenticate", out var wwwAuthenticateValues))
                {
                    var wwwAuthenticate = wwwAuthenticateValues.FirstOrDefault();
                    if (wwwAuthenticate != null && wwwAuthenticate.Contains("claims", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge while fetching contacts.");
                        _consentHandler.ChallengeUser(_graphScopes, wwwAuthenticate);
                        throw new AuthenticationRequiredException("Authentication challenge is required.");
                    }
                }

                _logger.LogError(ex, "API Exception occurred while fetching contacts.");

                // Log response headers if available
                if (ex.ResponseHeaders != null)
                {
                    var headers = string.Join(", ", ex.ResponseHeaders.Select(h => $"{h.Key}: {string.Join("; ", h.Value)}"));
                    _logger.LogError($"Response Headers: {headers}");
                }

                // Log inner exception if any
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner Exception:");
                }

            // Log additional data if available
            if (ex is Microsoft.Kiota.Abstractions.Serialization.IAdditionalDataHolder additionalDataHolder && additionalDataHolder.AdditionalData != null)
            {
                var additionalDataJson = System.Text.Json.JsonSerializer.Serialize(additionalDataHolder.AdditionalData);
                _logger.LogError($"Additional Data: {additionalDataJson}");
            }

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching contacts.");
                throw;
            }
        }
        public async Task<Contact> AddContactAsync(Contact newContact)
        {
            try
            {
                _logger.LogInformation($"Attempting to add a new contact: {newContact.DisplayName}");

                var contact = await GraphClient.Me.Contacts.PostAsync(newContact);

                _logger.LogInformation($"Successfully added contact: {contact.Id}");
                return contact;
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred while adding a contact.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while adding a contact.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred while adding a contact.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (ApiException ex)
            {
                if (ex.ResponseHeaders != null && ex.ResponseHeaders.TryGetValue("WWW-Authenticate", out var wwwAuthenticateValues))
                {
                    var wwwAuthenticate = wwwAuthenticateValues.FirstOrDefault();
                    if (wwwAuthenticate != null && wwwAuthenticate.Contains("claims", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge while adding a contact.");
                        _consentHandler.ChallengeUser(_graphScopes, wwwAuthenticate);
                        throw new AuthenticationRequiredException("Authentication challenge is required.");
                    }
                }

                _logger.LogError(ex, "API Exception occurred while adding a contact.");
                // Log additional details as needed
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a contact.");
                throw;
            }
        }

        public async Task DeleteContactAsync(string contactId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete contact: {contactId}");

                // Construct the request information
                var requestInfo = new RequestInformation
                {
                    HttpMethod = Method.DELETE,
                    UrlTemplate = "{+baseurl}/me/contacts/{contact%2Did}",
                    PathParameters = new Dictionary<string, object>
                    {
                        { "baseurl", _requestAdapter.BaseUrl },
                        { "contact%2Did", contactId }
                    }
                };

                // Send the delete request
                await _requestAdapter.SendNoContentAsync(requestInfo);

                _logger.LogInformation($"Successfully deleted contact: {contactId}");
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred while deleting a contact.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while deleting a contact.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred while deleting a contact.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (ApiException ex)
            {
                if (ex.ResponseHeaders != null && ex.ResponseHeaders.TryGetValue("WWW-Authenticate", out var wwwAuthenticateValues))
                {
                    var wwwAuthenticate = wwwAuthenticateValues.FirstOrDefault();
                    if (wwwAuthenticate != null && wwwAuthenticate.Contains("claims", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge while deleting a contact.");
                        _consentHandler.ChallengeUser(_graphScopes, wwwAuthenticate);
                        throw new AuthenticationRequiredException("Authentication challenge is required.");
                    }
                }

                _logger.LogError(ex, "API Exception occurred while deleting a contact.");
                // Log additional details as needed
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a contact.");
                throw;
            }
        }

public async Task<Contact> UpdateContactAsync(string contactId, Contact updatedContact)
{
    try
    {
        _logger.LogInformation($"Attempting to update contact: {contactId}");

        var requestInfo = new RequestInformation
        {
            HttpMethod = Method.PATCH,
            UrlTemplate = "{+baseurl}/me/contacts/{contact%2Did}",
            PathParameters = new Dictionary<string, object>
            {
                { "baseurl", _requestAdapter.BaseUrl },
                { "contact%2Did", contactId }
            }
        };

        _logger.LogInformation($"UpdateContactAsync Request URL: {requestInfo.UrlTemplate}");

        // Log the updated fields
        var updatedFieldsJson = System.Text.Json.JsonSerializer.Serialize(updatedContact);
        _logger.LogInformation($"UpdateContactAsync Request Body: {updatedFieldsJson}");

        // Set the content of the request using the Contact object
        requestInfo.SetContentFromParsable(_requestAdapter, "application/json", updatedContact);

        var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
        {
            {"4XX", ODataError.CreateFromDiscriminatorValue},
            {"5XX", ODataError.CreateFromDiscriminatorValue},
        };

        // Send the PATCH request to update the contact
        var updatedContactResponse = await _requestAdapter.SendAsync<Contact>(
            requestInfo,
            Contact.CreateFromDiscriminatorValue,
            errorMapping
        );

        _logger.LogInformation($"Successfully updated contact: {contactId}");
        return updatedContactResponse;
    }
    catch (Exception ex)
    {
        // Error handling (keep your existing catch blocks)
        _logger.LogError(ex, "An error occurred while updating a contact.");
        throw;
    }
}
        public async Task<CalendarEventsResult> GetCalendarEventsAsync(int count = 10)
        {
            var result = new CalendarEventsResult();

            try
            {
                _logger.LogInformation($"Attempting to fetch {count} calendar events");

                var eventsResponse = await _graphClient.Me.Calendar.Events.GetAsync(requestConfiguration =>
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
                result.Events = eventsResponse.Value?.ToList() ?? new List<Event>();
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "API Exception occurred while fetching calendar events.");
                _logger.LogError($"Error Message: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
                }

                // Optionally, set an error message in the result
                result.ErrorMessage = $"An error occurred while fetching calendar events: {ex.Message}";
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred.");
                result.ErrorMessage = "Authentication is required.";
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while fetching calendar events.");
                result.ErrorMessage = "Authentication challenge is required.";
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred.");
                result.ErrorMessage = "Unauthorized access.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching calendar events.");
                result.ErrorMessage = "An unexpected error occurred.";
            }

            return result;
        }

        public async Task<List<Event>> GetCalendarViewAsync(DateTime startDateTime, DateTime endDateTime, int maxEvents = 100)
        {
            try
            {
                _logger.LogInformation($"Attempting to fetch calendar events from {startDateTime} to {endDateTime}");

                var eventsResponse = await GraphClient.Me.Calendar.CalendarView.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.StartDateTime = startDateTime.ToString("o");
                    requestConfiguration.QueryParameters.EndDateTime = endDateTime.ToString("o");
                    requestConfiguration.QueryParameters.Top = maxEvents;
                    requestConfiguration.QueryParameters.Orderby = new[] { "start/dateTime" };
                    requestConfiguration.QueryParameters.Select = new[] {
                        "subject",
                        "organizer",
                        "start",
                        "end",
                        "location",
                        "body"
                    };
                });

                _logger.LogInformation($"Successfully fetched {eventsResponse.Value?.Count ?? 0} calendar events");
                return eventsResponse.Value?.ToList() ?? new List<Event>();
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
                        "receivedDateTime",
                        "body",           // Added to fetch the body content
                        "toRecipients"    // Added to fetch the recipients
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


        public async Task<DriveItem> GetFileByIdAsync(string fileId)
        {
            try
            {
                _logger.LogInformation($"Attempting to fetch file with ID: {fileId}");
                var drive = await GraphClient.Me.Drive.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Expand = new[] { $"items(${fileId})" };
                    requestConfiguration.QueryParameters.Select = new[] { $"items(${fileId})" };
                });

                var file = drive?.Items?.FirstOrDefault(item => item.Id == fileId);

                if (file == null)
                {
                    throw new FileNotFoundException($"File with ID {fileId} not found.");
                }

                _logger.LogInformation($"Successfully fetched file: {file.Name}");
                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching file with ID: {fileId}");
                throw;
            }
        }

        public async Task<Stream> DownloadFileContentAsync(string fileId)
        {
            try
            {
                _logger.LogInformation($"Attempting to download content for file with ID: {fileId}");
                var file = await GetFileByIdAsync(fileId);

                if (file.Content == null)
                {
                    throw new InvalidOperationException($"Content for file with ID {fileId} is not available.");
                }

                var contentStream = new MemoryStream(file.Content);

                _logger.LogInformation($"Successfully downloaded content for file with ID: {fileId}");
                return contentStream;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while downloading content for file with ID: {fileId}");
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
    public class AuthenticationRequiredException : Exception
    {
        public AuthenticationRequiredException(string message) : base(message) { }
        public AuthenticationRequiredException(string message, Exception innerException) : base(message, innerException) { }
    }


    // Define a result class to encapsulate events and error messages
    public class CalendarEventsResult
    {
        public List<Event> Events { get; set; } = new List<Event>();
        public string ErrorMessage { get; set; }
    }

}
