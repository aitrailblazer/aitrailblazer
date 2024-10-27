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
using Newtonsoft.Json;
using AITGraph.Sdk.Models.ODataErrors;
using AITGraph.Sdk.Me;
using AITGraph.Sdk.Me.Contacts;
using AITGraph.Sdk.Me.Profile;
using AITGraph.Sdk.Me.Calendar.Events;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Abstractions.Serialization;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Identity.Client;
using System.IO; // For StreamReader
using AITrailblazer.net.Models;
using System.Globalization;
namespace AITrailblazer.net.Services
{

    public class AITGraphService
    {
        private readonly MicrosoftIdentityConsentAndConditionalAccessHandler _consentHandler;
        private readonly ILogger<AITGraphService> _logger;
        private readonly ITokenAcquisition _tokenAcquisition;
        private AITGraphApiClient _graphClient;
        private readonly string[] _graphScopes;
        private HttpClientRequestAdapter _requestAdapter;
        private EventsRequestBuilder _eventsRequestBuilder;

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
                    _eventsRequestBuilder = new EventsRequestBuilder(new Dictionary<string, object>(), _requestAdapter);

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
                    requestConfiguration.QueryParameters.Expand = new[]
                    {
                GetExpandQueryParameterType.Names,
                GetExpandQueryParameterType.Addresses,
                GetExpandQueryParameterType.Emails,
                GetExpandQueryParameterType.Phones,
                GetExpandQueryParameterType.Languages,
                GetExpandQueryParameterType.Positions,
                GetExpandQueryParameterType.Skills,
                GetExpandQueryParameterType.EducationalActivities
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

                if (_requestAdapter == null)
                {
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
                    _logger.LogInformation($"User profile photo fetched successfully, {requestUrl}");
                    return responseStream;
                }
                else
                {
                    _logger.LogWarning("User profile photo not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching user profile photo.");
                // Instead of throwing the error, return null to indicate no photo was found
                return null;
            }
        }
        public async Task<MailboxSettings> GetMailboxSettingsAsync()
        {
            try
            {
                _logger.LogInformation("Attempting to fetch mailbox settings");

                if (_requestAdapter == null)
                {
                    var client = GraphClient;
                }

                var requestUrl = $"{_requestAdapter.BaseUrl}/me/mailboxSettings";

                var requestInfo = new RequestInformation
                {
                    HttpMethod = Method.GET,
                    UrlTemplate = requestUrl,
                    PathParameters = new Dictionary<string, object>()
                };

                requestInfo.Headers.Add("Accept", "application/json");

                var response = await _requestAdapter.SendAsync<MailboxSettings>(
                    requestInfo,
                    MailboxSettings.CreateFromDiscriminatorValue,
                    errorMapping: null,
                    cancellationToken: CancellationToken.None
                );

                if (response != null)
                {
                    _logger.LogInformation($"Mailbox settings fetched successfully, {requestUrl}");
                    return response;
                }
                else
                {
                    _logger.LogWarning("Mailbox settings not found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching mailbox settings.");
                return null;
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
                AITGraph.Sdk.Me.Profile.Account.GetSelectQueryParameterType.AgeGroup,
                AITGraph.Sdk.Me.Profile.Account.GetSelectQueryParameterType.CountryCode,
                AITGraph.Sdk.Me.Profile.Account.GetSelectQueryParameterType.PreferredLanguageTag,
                AITGraph.Sdk.Me.Profile.Account.GetSelectQueryParameterType.UserPrincipalName,
                AITGraph.Sdk.Me.Profile.Account.GetSelectQueryParameterType.AllowedAudiences,
                AITGraph.Sdk.Me.Profile.Account.GetSelectQueryParameterType.CreatedDateTime,
                AITGraph.Sdk.Me.Profile.Account.GetSelectQueryParameterType.LastModifiedDateTime,
                AITGraph.Sdk.Me.Profile.Account.GetSelectQueryParameterType.Id
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
                    requestConfiguration.QueryParameters.Orderby = new[] {
                    AITGraph.Sdk.Me.Contacts.GetOrderbyQueryParameterType.DisplayName
                        // Add other enum members if needed, e.g., GetOrderbyQueryParameterType.GivenNameDesc
                    };
                    requestConfiguration.QueryParameters.Select = new[]
                    {
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.Id,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.DisplayName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.GivenName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.Surname,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.EmailAddresses,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.CompanyName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.JobTitle,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.OfficeLocation,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.ImAddresses,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.Birthday,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.NickName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.MiddleName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.PersonalNotes,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.SpouseName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.Department,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.AssistantName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.YomiGivenName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.YomiSurname,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.YomiCompanyName,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.Profession,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.Title,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.Children,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.Phones,
                AITGraph.Sdk.Me.Contacts.GetSelectQueryParameterType.PostalAddresses
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
                if (ex is IAdditionalDataHolder additionalDataHolder && additionalDataHolder.AdditionalData != null)
                {
                    var additionalDataJson = JsonConvert.SerializeObject(additionalDataHolder.AdditionalData, Formatting.Indented);
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

                // Log the updated fields using Newtonsoft.Json
                var updatedFieldsJson = JsonConvert.SerializeObject(updatedContact, Formatting.Indented);
                _logger.LogInformation($"UpdateContactAsync Request Body: {updatedFieldsJson}");

                // Set the content of the request using the Contact object
                requestInfo.SetContentFromParsable(_requestAdapter, "application/json", updatedContact);

                var errorMapping = new Dictionary<string, ParsableFactory<IParsable>>
        {
            { "4XX", ODataError.CreateFromDiscriminatorValue },
            { "5XX", ODataError.CreateFromDiscriminatorValue },
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
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred while updating contact.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while updating contact.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred while updating contact.");
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
                        _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge while updating contact.");
                        _consentHandler.ChallengeUser(_graphScopes, wwwAuthenticate);
                        throw new AuthenticationRequiredException("Authentication challenge is required.");
                    }
                }

                _logger.LogError(ex, "API Exception occurred while updating contact.");

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
                    var additionalDataJson = JsonConvert.SerializeObject(additionalDataHolder.AdditionalData, Formatting.Indented);
                    _logger.LogError($"Additional Data: {additionalDataJson}");
                }

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a contact.");
                throw;
            }
        }
        public async Task<CalendarEventsResult> GetCalendarEventsAsync2(int count = 10, DateTime? startDate = null, DateTime? endDate = null)
        {
            var result = new CalendarEventsResult();

            try
            {
                _logger.LogInformation($"Attempting to fetch {count} calendar events");

                var eventsResponse = await _graphClient.Me.Calendar.Events.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Top = count;
                    // Set the order by parameter using enum
                    requestConfiguration.QueryParameters.Orderby = new[] 
                    {
                        AITGraph.Sdk.Me.Calendar.Events.GetOrderbyQueryParameterType.Start
                        // Add other enum members if needed, e.g., GetOrderbyQueryParameterType.StartDesc
                    };

                    // Select specific fields using enum
                    requestConfiguration.QueryParameters.Select = new[]
                    {
                        AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Subject,
                        AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Organizer,
                        AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Start,
                        AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.End,
                        AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Attendees,
                        AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Body,
                        AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Importance,
                        AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Location
                    };

                    if (startDate.HasValue && endDate.HasValue)
                    {
                        
                        requestConfiguration.QueryParameters.Filter = $"start/dateTime ge '{startDate.Value:yyyy-MM-ddTHH:mm:ssZ}' and end/dateTime le '{endDate.Value:yyyy-MM-ddTHH:mm:ssZ}'";
                    }
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

public async Task<List<EventViewModel>> GetCalendarEventsAsync(int count = 10, DateTime? startDate = null, DateTime? endDate = null)
{
    try
    {
        _logger.LogInformation($"GetCalendarEventsAsync: Starting to fetch up to {count} calendar events");

        startDate ??= DateTime.UtcNow;
        endDate ??= startDate.Value.AddDays(30);

        _logger.LogInformation($"GetCalendarEventsAsync: Date range - Start: {startDate.Value:o}, End: {endDate.Value:o}");

        var eventsResponse = await GraphClient.Me.Calendar.Events.GetAsync(requestConfiguration =>
        {
            requestConfiguration.QueryParameters.Top = count;
            requestConfiguration.QueryParameters.Filter = $"start/dateTime ge '{startDate.Value:o}' and end/dateTime le '{endDate.Value:o}'";
            requestConfiguration.QueryParameters.Orderby = new[] { AITGraph.Sdk.Me.Calendar.Events.GetOrderbyQueryParameterType.CreatedDateTime };
            requestConfiguration.QueryParameters.Select = new[]
            {
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Id,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Subject,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Organizer,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Attendees,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Start,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.End,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Location,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.BodyPreview,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.IsAllDay,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.IsCancelled,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Importance,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.Categories,
                AITGraph.Sdk.Me.Calendar.Events.GetSelectQueryParameterType.WebLink
            };
        });

        if (eventsResponse?.Value == null || !eventsResponse.Value.Any())
        {
            _logger.LogInformation("GetCalendarEventsAsync: No events found for the specified date range.");
            return new List<EventViewModel>(); // Return an empty list instead of JSON
        }

        _logger.LogInformation($"GetCalendarEventsAsync: Successfully fetched {eventsResponse.Value.Count} events");

        var eventList = eventsResponse.Value.ToList();

        var rawEventsJson = JsonConvert.SerializeObject(
            eventsResponse.Value,
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            }
        );

        _logger.LogInformation($"GetCalendarEventsAsync: Raw Events Response: {rawEventsJson}");

        // Serialize events to JSON
        var basicModelList = eventList.Select(evt => new EventViewModel
        {
            Id = evt.Id,
            Subject = evt.Subject,
            BodyPreview = evt.BodyPreview,
            WebLink = evt.WebLink,
            Importance = evt.Importance.HasValue ? (EventImportance)evt.Importance.Value : EventImportance.Normal,
            IsAllDay = evt.IsAllDay ?? false,
            IsCancelled = evt.IsCancelled ?? false,
            Categories = evt.Categories?.ToList() ?? new List<string>(),
            Location = evt.Location ?? new Location
            {
                DisplayName = "No Location"
                // Initialize other properties as needed
            },
            StartDateTime = evt.Start?.DateTime != null ? DateTimeOffset.Parse(evt.Start.DateTime) : (DateTimeOffset?)null,
            EndDateTime = evt.End?.DateTime != null ? DateTimeOffset.Parse(evt.End.DateTime) : (DateTimeOffset?)null,
            StartDateTimeFormatted = evt.Start?.DateTime != null ? DateTimeOffset.Parse(evt.Start.DateTime).ToLocalTime().ToString("g", CultureInfo.CurrentCulture) : "N/A",
            EndDateTimeFormatted = evt.End?.DateTime != null ? DateTimeOffset.Parse(evt.End.DateTime).ToLocalTime().ToString("g", CultureInfo.CurrentCulture) : "N/A",
            Attendees = evt.Attendees?.Where(a => a?.EmailAddress != null).ToList() ?? new List<Attendee>()
        }).ToList();

        var jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        var jsonResponse = JsonConvert.SerializeObject(basicModelList, jsonSettings);
        _logger.LogInformation($"GetCalendarEventsAsync: Processed JSON Response: {jsonResponse}");

        return basicModelList; // Return the list instead of JSON
    }
    catch (MsalUiRequiredException ex)
    {
        _logger.LogError(ex, "MSAL UI Required Exception occurred while fetching events.");
        throw new AuthenticationRequiredException("Authentication is required.");
    }
    catch (MicrosoftIdentityWebChallengeUserException ex)
    {
        _logger.LogError(ex, "User challenge occurred while fetching events.");
        throw new AuthenticationRequiredException("Authentication challenge is required.");
    }
    catch (UnauthorizedAccessException ex)
    {
        _logger.LogError(ex, "Unauthorized access exception occurred while fetching events.");
        throw new AuthenticationRequiredException("Unauthorized access.");
    }
    catch (AITGraph.Sdk.Models.ODataErrors.ODataError odataError)
    {
        _logger.LogError(odataError, $"GetCalendarEventsAsync: OData error occurred. Error Code: {odataError.Error?.Code}, Message: {odataError.Error?.Message}");
        _logger.LogError($"GetCalendarEventsAsync: Full OData Error: {JsonConvert.SerializeObject(odataError, Formatting.Indented)}");
        throw new InvalidOperationException($"Failed to fetch calendar events. Error: {odataError.Error?.Message}", odataError);
    }
    catch (ApiException ex)
    {
        // Check for claims challenge in the WWW-Authenticate header
        if (ex.ResponseHeaders != null && ex.ResponseHeaders.TryGetValue("WWW-Authenticate", out var wwwAuthenticateValues))
        {
            var wwwAuthenticate = wwwAuthenticateValues.FirstOrDefault();
            if (wwwAuthenticate != null && wwwAuthenticate.Contains("claims", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge while fetching events.");
                _consentHandler.ChallengeUser(_graphScopes, wwwAuthenticate);
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
        }

        _logger.LogError(ex, "API Exception occurred while fetching events.");

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
        if (ex is IAdditionalDataHolder additionalDataHolder && additionalDataHolder.AdditionalData != null)
        {
            var additionalDataJson = JsonConvert.SerializeObject(additionalDataHolder.AdditionalData, Formatting.Indented);
            _logger.LogError($"Additional Data: {additionalDataJson}");
        }

        throw;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "GetCalendarEventsAsync: An unexpected error occurred while fetching calendar events.");
        throw;
    }
}


        /// <summary>
        /// Creates a new calendar event for the user.
        /// </summary>
        /// <param name="newEvent">The Event object containing event details.</param>
        /// <returns>A <see cref="CreateCalendarEventResult"/> containing the created event and any error messages.</returns>
        public async Task<CreateCalendarEventResult> CreateCalendarEventAsync(Event newEvent)
        {
            var result = new CreateCalendarEventResult();

            if (newEvent == null)
            {
                _logger.LogError("CreateCalendarEventAsync called with null newEvent.");
                result.ErrorMessage = "Event details cannot be null.";
                return result;
            }

            // Additional Validation: Ensure essential properties are set
            var validationErrors = ValidateEvent(newEvent);
            if (validationErrors.Count > 0)
            {
                _logger.LogError("Validation failed for CreateCalendarEventAsync: {Errors}", string.Join("; ", validationErrors));
                result.ErrorMessage = $"Validation failed: {string.Join("; ", validationErrors)}";
                return result;
            }

            try
            {
                _logger.LogInformation("Attempting to create a new calendar event: {Subject}", newEvent.Subject);

                // Call the PostAsync method from EventsRequestBuilder
                var createdEvent = await _eventsRequestBuilder.PostAsync(newEvent, null, default);

                if (createdEvent != null)
                {
                    _logger.LogInformation("Successfully created calendar event with ID: {EventId}", createdEvent.Id);
                    result.CreatedEvent = createdEvent;
                }
                else
                {
                    _logger.LogWarning("CreateCalendarEventAsync returned null.");
                    result.ErrorMessage = "Failed to create the calendar event.";
                }
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "API Exception occurred while creating calendar event: {Message}", ex.Message);
                result.ErrorMessage = $"An error occurred while creating the calendar event: {ex.Message}";
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred while creating calendar event.");
                result.ErrorMessage = "Authentication is required.";
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while creating calendar event.");
                result.ErrorMessage = "Authentication challenge is required.";
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred while creating calendar event.");
                result.ErrorMessage = "Unauthorized access.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating calendar event.");
                result.ErrorMessage = "An unexpected error occurred.";
            }

            return result;
        }

        /// <summary>
        /// Validates essential properties of the Event object.
        /// </summary>
        /// <param name="eventToValidate">The Event object to validate.</param>
        /// <returns>A list of validation error messages.</returns>
        private List<string> ValidateEvent(Event eventToValidate)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(eventToValidate.Subject))
                errors.Add("Subject is required.");

            if (eventToValidate.Start == null || string.IsNullOrWhiteSpace(eventToValidate.Start.DateTime))
                errors.Add("Start date and time are required.");

            if (eventToValidate.End == null || string.IsNullOrWhiteSpace(eventToValidate.End.DateTime))
                errors.Add("End date and time are required.");

            if (eventToValidate.Start != null && eventToValidate.End != null)
            {
                if (DateTime.TryParse(eventToValidate.Start.DateTime, out DateTime startTime) &&
                    DateTime.TryParse(eventToValidate.End.DateTime, out DateTime endTime))
                {
                    if (endTime <= startTime)
                        errors.Add("End time must be after start time.");
                }
                else
                {
                    errors.Add("Invalid date and time format.");
                }
            }

            if (eventToValidate.Location == null || string.IsNullOrWhiteSpace(eventToValidate.Location.DisplayName))
                errors.Add("Location is required.");

            if (eventToValidate.Attendees == null || eventToValidate.Attendees.Count == 0)
                errors.Add("At least one attendee is required.");

            // Additional validations can be added here (e.g., Body content length)

            return errors;
        }

        /// <summary>
        /// Result class for CreateCalendarEventAsync method.
        /// </summary>
        public class CreateCalendarEventResult
        {
            public Event? CreatedEvent { get; set; }
            public string? ErrorMessage { get; set; }
        }

        public async Task<List<EventViewModel>> GetCalendarViewAsync(int maxEvents = 10, string startDateTime = null, string endDateTime = null)
        {
            try
            {
                _logger.LogInformation($"GetCalendarViewAsync: Starting to fetch up to {maxEvents} calendar events");

                DateTime startDateTimeParsed;
                DateTime endDateTimeParsed;

                // Parse startDateTime
                if (string.IsNullOrWhiteSpace(startDateTime))
                {
                    startDateTimeParsed = DateTime.UtcNow;
                }
                else if (!DateTime.TryParse(startDateTime, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out startDateTimeParsed))
                {
                    _logger.LogError($"Invalid startDateTime format: {startDateTime}");
                    throw new ArgumentException($"Invalid startDateTime format: {startDateTime}");
                }

                // Parse endDateTime
                if (string.IsNullOrWhiteSpace(endDateTime))
                {
                    endDateTimeParsed = startDateTimeParsed.AddDays(30);
                }
                else if (!DateTime.TryParse(endDateTime, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out endDateTimeParsed))
                {
                    _logger.LogError($"Invalid endDateTime format: {endDateTime}");
                    throw new ArgumentException($"Invalid endDateTime format: {endDateTime}");
                }

                // Ensure endDateTime is after startDateTime
                if (endDateTimeParsed <= startDateTimeParsed)
                {
                    _logger.LogError("endDateTime must be after startDateTime");
                    throw new ArgumentException("endDateTime must be after startDateTime");
                }

                _logger.LogInformation($"GetCalendarViewAsync: Date range - Start: {startDateTimeParsed:o}, End: {endDateTimeParsed:o}");

                var eventsResponse = await _graphClient.Me.CalendarView.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.StartDateTime = startDateTimeParsed.ToString("o");
                    requestConfiguration.QueryParameters.EndDateTime = endDateTimeParsed.ToString("o");
                    requestConfiguration.QueryParameters.Top = maxEvents;
                    requestConfiguration.QueryParameters.Orderby = new[] { AITGraph.Sdk.Me.CalendarView.GetOrderbyQueryParameterType.CreatedDateTime };
                    requestConfiguration.QueryParameters.Select = new[]
                    {
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.Subject,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.Organizer,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.Start,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.End,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.Location,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.BodyPreview,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.IsAllDay,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.IsCancelled,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.Importance,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.Categories,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.WebLink,
                        AITGraph.Sdk.Me.CalendarView.GetSelectQueryParameterType.Attendees
                    };
                });

                if (eventsResponse?.Value == null || !eventsResponse.Value.Any())
                {
                    _logger.LogInformation("GetCalendarViewAsync: No events found for the specified date range.");
                    return new List<EventViewModel>(); // Return an empty list
                }

                _logger.LogInformation($"GetCalendarViewAsync: Successfully fetched {eventsResponse.Value.Count} events");

                var eventList = eventsResponse.Value.ToList();

                // Map Event objects to EventViewModel manually
                var basicModelList = eventList.Select(evt => new EventViewModel
                {
                    Id = evt.Id,
                    Subject = evt.Subject ?? "No Subject",
                    BodyPreview = evt.BodyPreview ?? "No Preview Available",
                    WebLink = evt.WebLink ?? string.Empty,
                    Importance = evt.Importance.HasValue ? (EventImportance)evt.Importance.Value : EventImportance.Normal,
                    IsAllDay = evt.IsAllDay ?? false,
                    IsCancelled = evt.IsCancelled ?? false,
                    Categories = evt.Categories?.ToList() ?? new List<string>(),
                    Location = evt.Location ?? new Location
                    {
                        DisplayName = "No Location"
                        // Initialize other properties as needed
                    },
                    StartDateTime = evt.Start?.DateTime != null ? DateTimeOffset.Parse(evt.Start.DateTime) : (DateTimeOffset?)null,
                    EndDateTime = evt.End?.DateTime != null ? DateTimeOffset.Parse(evt.End.DateTime) : (DateTimeOffset?)null,
                    StartDateTimeFormatted = evt.Start?.DateTime != null ? DateTimeOffset.Parse(evt.Start.DateTime).ToLocalTime().ToString("g", CultureInfo.CurrentCulture) : "N/A",
                    EndDateTimeFormatted = evt.End?.DateTime != null ? DateTimeOffset.Parse(evt.End.DateTime).ToLocalTime().ToString("g", CultureInfo.CurrentCulture) : "N/A",
                    Attendees = evt.Attendees?.Where(a => a?.EmailAddress != null).ToList() ?? new List<Attendee>()
                }).ToList();

                _logger.LogInformation($"GetCalendarViewAsync: Successfully mapped events to EventViewModel");

                // Serialize the basicModelList to JSON
                var jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                };

                string jsonResult = JsonConvert.SerializeObject(basicModelList, jsonSettings);

                // Log the serialized JSON
                _logger.LogInformation($"GetCalendarViewAsync: Serialized EventViewModel List: {jsonResult}");

                return basicModelList; // Return the list directly
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred while fetching calendar events.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while fetching calendar events.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred while fetching calendar events.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (AITGraph.Sdk.Models.ODataErrors.ODataError odataError)
            {
                _logger.LogError(odataError, $"GetCalendarViewAsync: OData error occurred. Error Code: {odataError.Error?.Code}, Message: {odataError.Error?.Message}");
                _logger.LogError($"GetCalendarViewAsync: Full OData Error: {JsonConvert.SerializeObject(odataError, Formatting.Indented)}");
                throw new InvalidOperationException($"Failed to fetch calendar events. Error: {odataError.Error?.Message}", odataError);
            }
            catch (ApiException ex)
            {
                // Check for claims challenge in the WWW-Authenticate header
                if (ex.ResponseHeaders != null && ex.ResponseHeaders.TryGetValue("WWW-Authenticate", out var wwwAuthenticateValues))
                {
                    var wwwAuthenticate = wwwAuthenticateValues.FirstOrDefault();
                    if (wwwAuthenticate != null && wwwAuthenticate.Contains("claims", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge while fetching calendar events.");
                        _consentHandler.ChallengeUser(_graphScopes, wwwAuthenticate);
                        throw new AuthenticationRequiredException("Authentication challenge is required.");
                    }
                }

                _logger.LogError(ex, "API Exception occurred while fetching calendar events.");

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
                if (ex is IAdditionalDataHolder additionalDataHolder && additionalDataHolder.AdditionalData != null)
                {
                    var additionalDataJson = JsonConvert.SerializeObject(additionalDataHolder.AdditionalData, Formatting.Indented);
                    _logger.LogError($"Additional Data: {additionalDataJson}");
                }

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCalendarViewAsync: An unexpected error occurred while fetching calendar events.");
                throw;
            }
        }


        public async Task<string> GetRecentMessagesAsync(int count = 5)
        {
            try
            {
                _logger.LogInformation($"GetRecentMessagesAsync Attempting to fetch {count} recent messages");

                var messagesResponse = await GraphClient.Me.Messages.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Top = count;
                    requestConfiguration.QueryParameters.Orderby = new[] { AITGraph.Sdk.Me.Messages.GetOrderbyQueryParameterType.ReceivedDateTimeDesc };
                    requestConfiguration.QueryParameters.Select = new[]
                    {
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Id,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Subject,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.From,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ToRecipients,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.CcRecipients,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.BccRecipients,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ReceivedDateTime,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Body,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.HasAttachments,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Importance,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Categories,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.IsRead,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.InternetMessageId,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ConversationId,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.WebLink
                    };
                });

                _logger.LogInformation($"GetRecentMessagesAsync Successfully fetched {messagesResponse.Value?.Count ?? 0} messages");

                var messageList = messagesResponse.Value?.ToList() ?? new List<Message>();

                // **Log the raw messagesResponse.Value before processing**
                var rawMessagesJson = JsonConvert.SerializeObject(
                    messagesResponse.Value,
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore
                        // Add converters if necessary, e.g., StringEnumConverter
                    }
                );

                _logger.LogInformation($"GetRecentMessagesAsync Raw Messages Response: {rawMessagesJson}");

                var basicModelList = messageList.Select(message => new EmailViewBasicModel(message)).ToList();

                // Configure Newtonsoft.Json settings
                var jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                    // Add converters if you have enum properties or other custom serialization needs
                    // For example, to handle enums as strings:
                    // Converters = { new StringEnumConverter() }
                };

                // Serialize using Newtonsoft.Json
                var jsonResponse = JsonConvert.SerializeObject(basicModelList, jsonSettings);
                _logger.LogInformation($"GetRecentMessagesAsync jsonResponse: {jsonResponse}");

                return jsonResponse;
            }
            catch (Newtonsoft.Json.JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "GetRecentMessagesAsync JSON Serialization/Deserialization error occurred while fetching recent messages.");
                throw new InvalidOperationException("Failed to serialize messages to JSON.", jsonEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetRecentMessagesAsync An error occurred while fetching recent messages.");
                throw;
            }
        }

public async Task<string> GetRecentMessagesPureEmailViewBasicModelAsync(int count = 5)
{
    try
    {
        _logger.LogInformation($"GetRecentMessagesAsync: Attempting to fetch {count} recent messages.");

        var messagesResponse = await GraphClient.Me.Messages.GetAsync(requestConfiguration =>
        {
            requestConfiguration.QueryParameters.Top = count;
            requestConfiguration.QueryParameters.Orderby = new[] { AITGraph.Sdk.Me.Messages.GetOrderbyQueryParameterType.ReceivedDateTimeDesc };
            requestConfiguration.QueryParameters.Select = new[]
            {
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Id,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Subject,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.From,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ToRecipients,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.CcRecipients,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.BccRecipients,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ReceivedDateTime,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Body,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.HasAttachments,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Importance,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Categories,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.IsRead,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.InternetMessageId,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ConversationId,
                AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.WebLink
            };
        }).ConfigureAwait(false);

        _logger.LogInformation($"GetRecentMessagesPureEmailViewBasicModelAsync: Successfully fetched {messagesResponse.Value?.Count ?? 0} messages.");

        var messageList = messagesResponse.Value?.ToList() ?? new List<Message>();

        // **Log the raw messagesResponse.Value before processing**
        var rawMessagesJson = JsonConvert.SerializeObject(
            messageList,
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
                // Add converters if necessary, e.g., StringEnumConverter
            }
        );

        //_logger.LogDebug($"GetRecentMessagesPureEmailViewBasicModelAsync: Raw Messages Response: {rawMessagesJson}");
        _logger.LogInformation($"GetRecentMessagesPureEmailViewBasicModelAsync: Raw Messages Response: {rawMessagesJson}");

        var basicModelList = messageList.Select(message => new EmailViewBasicPureConstructorModel(message)).ToList();

        // **Wrap the list within EmailResult**
        var emailResult = new EmailResultPureConstructor
        {
            Emails = basicModelList
        };

        // **Configure Newtonsoft.Json settings**
        var jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
            // Add converters if you have enum properties or other custom serialization needs
            // For example, to handle enums as strings:
            // Converters = { new StringEnumConverter() }
        };

        // **Serialize the EmailResult object to JSON**
        var jsonResponse = JsonConvert.SerializeObject(emailResult, jsonSettings);
        _logger.LogInformation($"GetRecentMessagesPureEmailViewBasicModelAsync: JSON Response generated with {basicModelList.Count} emails.");

        return jsonResponse;
    }
    catch (Newtonsoft.Json.JsonException jsonEx)
    {
        _logger.LogError(jsonEx, "GetRecentMessagesPureEmailViewBasicModelAsync: JSON Serialization/Deserialization error occurred while fetching recent messages.");
        throw new InvalidOperationException("Failed to serialize messages to JSON.", jsonEx);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "GetRecentMessagesPureEmailViewBasicModelAsync: An unexpected error occurred while fetching recent messages.");
        throw;
    }
}
        public async Task DeleteEmailMessageAsync(string messageId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(messageId))
                {
                    _logger.LogError("Message ID cannot be null or empty.");
                    throw new ArgumentException("Message ID cannot be null or empty.", nameof(messageId));
                }

                _logger.LogInformation($"Attempting to delete email message with ID: {messageId}");

                // Create the request information
                var requestInfo = new RequestInformation
                {
                    HttpMethod = Method.DELETE,
                    UrlTemplate = "{+baseurl}/me/messages/{messageId}",
                    PathParameters = new Dictionary<string, object>
                    {
                        { "baseurl", _requestAdapter.BaseUrl },
                        { "messageId", messageId }
                    }
                };

                // Send the delete request
                await _requestAdapter.SendNoContentAsync(requestInfo, cancellationToken: CancellationToken.None);

                _logger.LogInformation($"Successfully deleted email message with ID: {messageId}");
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred while deleting an email message.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while deleting an email message.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred while deleting an email message.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (ApiException ex)
            {
                if (ex.ResponseHeaders != null && ex.ResponseHeaders.TryGetValue("WWW-Authenticate", out var wwwAuthenticateValues))
                {
                    var wwwAuthenticate = wwwAuthenticateValues.FirstOrDefault();
                    if (wwwAuthenticate != null && wwwAuthenticate.Contains("claims", StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.LogError(ex, "Continuous access evaluation resulted in claims challenge while deleting an email message.");
                        _consentHandler.ChallengeUser(_graphScopes, wwwAuthenticate);
                        throw new AuthenticationRequiredException("Authentication challenge is required.");
                    }
                }

                _logger.LogError(ex, "API Exception occurred while deleting an email message.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting an email message.");
                throw;
            }
        }

        public async Task<string> SearchMessagesAsync(string searchQuery, int count = 10)
        {
            try
            {
                _logger.LogInformation($"Attempting to search messages with query: {searchQuery}");

                // Format the search query
                string formattedQuery = $"\"{searchQuery.Replace("\"", "\\\"")}\"";

                var messagesResponse = await GraphClient.Me.Messages.GetAsync(requestConfiguration =>
                {
                    requestConfiguration.QueryParameters.Search = formattedQuery;
                    requestConfiguration.QueryParameters.Top = count;
                    requestConfiguration.QueryParameters.Select = new[]
                    {
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Id,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Subject,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.From,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ToRecipients,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.CcRecipients,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.BccRecipients,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ReceivedDateTime,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Body,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.HasAttachments,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Importance,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.Categories,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.IsRead,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.InternetMessageId,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.ConversationId,
                        AITGraph.Sdk.Me.Messages.GetSelectQueryParameterType.WebLink
                    };
                });

                _logger.LogInformation($"Successfully fetched {messagesResponse?.Value?.Count ?? 0} messages matching the search query");

                var basicModelList = messagesResponse.Value?
                    .Select(message => new PureEmailViewBasicModel
                    {
                        Id = message.Id,
                        Subject = message.Subject,
                        From = message.From != null ? new PureRecipientInfo { Name = message.From.EmailAddress.Name, EmailAddress = message.From.EmailAddress.Address } : null,
                        ToRecipients = message.ToRecipients?.Select(r => new PureRecipientInfo { Name = r.EmailAddress.Name, EmailAddress = r.EmailAddress.Address }).ToList(),
                        CcRecipients = message.CcRecipients?.Select(r => new PureRecipientInfo { Name = r.EmailAddress.Name, EmailAddress = r.EmailAddress.Address }).ToList(),
                        BccRecipients = message.BccRecipients?.Select(r => new PureRecipientInfo { Name = r.EmailAddress.Name, EmailAddress = r.EmailAddress.Address }).ToList(),
                        ReceivedDateTime = message.ReceivedDateTime?.ToString("o"),
                        BodyContent = message.Body?.Content,
                        BodyContentType = "text",//message.Body?.ContentType,
                        HasAttachments = message.HasAttachments ?? false,
                        Importance = message.Importance.ToString(),
                        Categories = message.Categories?.ToList(),
                        IsRead = message.IsRead ?? false,
                        InternetMessageId = message.InternetMessageId,
                        ConversationId = message.ConversationId,
                        WebLink = message.WebLink
                    })
                    .ToList();

                // Serialize to JSON
                var jsonResponse = JsonConvert.SerializeObject(basicModelList, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });

                _logger.LogInformation($"SearchMessagesAsync jsonResponse: {jsonResponse}");

                return jsonResponse;
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "JSON Serialization/Deserialization error occurred during search.");
                throw new InvalidOperationException("Failed to serialize messages to JSON.", jsonEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while searching messages with query: {searchQuery}");
                throw;
            }
        }

        public async Task DeleteCalendarEventAsync(string eventId)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete calendar event with ID: {eventId}");

                // Construct the request information
                var requestInfo = new RequestInformation
                {
                    HttpMethod = Method.DELETE,
                    UrlTemplate = "{+baseurl}/me/events/{eventId}",
                    PathParameters = new Dictionary<string, object>
                    {
                        { "baseurl", _requestAdapter.BaseUrl },
                        { "eventId", eventId }
                    }
                };

                // Send the delete request
                await _requestAdapter.SendNoContentAsync(requestInfo);

                _logger.LogInformation($"Successfully deleted calendar event with ID: {eventId}");
            }
            catch (MsalUiRequiredException ex)
            {
                _logger.LogError(ex, "MSAL UI Required Exception occurred while deleting a calendar event.");
                throw new AuthenticationRequiredException("Authentication is required.");
            }
            catch (MicrosoftIdentityWebChallengeUserException ex)
            {
                _logger.LogError(ex, "User challenge occurred while deleting a calendar event.");
                throw new AuthenticationRequiredException("Authentication challenge is required.");
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access exception occurred while deleting a calendar event.");
                throw new AuthenticationRequiredException("Unauthorized access.");
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, "API Exception occurred while deleting calendar event.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a calendar event.");
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
