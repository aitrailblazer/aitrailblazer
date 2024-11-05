using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using TimeZoneConverter;
using Newtonsoft.Json;

namespace AITrailblazer.net.Services
{
    public class UserIDsService
    {
        private readonly ILogger<UserIDsService> _logger;
        private readonly AITGraphService _graphService;
        private readonly IMemoryCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIDsService(
            AITGraphService graphService,
            ILogger<UserIDsService> logger,
            IMemoryCache cache,
            IHttpContextAccessor httpContextAccessor)
        {
            _graphService = graphService;
            _logger = logger;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Logs all user claims for debugging purposes.
        /// </summary>
        public async Task ListAllClaimsAsync()
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims
                .Select(c => new
                {
                    Type = c.Type,
                    Value = c.Value
                });

            if (claims != null)
            {
                foreach (var claim in claims)
                {
                    _logger.LogInformation($"UserClaim {claim.Type}: {claim.Value}");
                }
            }
            else
            {
                _logger.LogWarning("No user claims found.");
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves the UserId and TenantId from the user's claims.
        /// </summary>
        /// <returns>A tuple containing UserId and TenantId.</returns>
        public async Task<(string UserId, string TenantId)> GetUserIDsAsync()
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;

            var userId = claims?.FirstOrDefault(c => c.Type == "uid")?.Value ?? "currentUserIdentityID";
            var tenantId = claims?.FirstOrDefault(c => c.Type == "utid")?.Value ?? "currentTenantID";

            _logger.LogInformation($"UserClaim uid: {userId}");
            _logger.LogInformation($"UserClaim utid: {tenantId}");

            // Store tenantId and userId in cache for quick access
            _cache.Set("TenantId", tenantId, TimeSpan.FromHours(1));
            _cache.Set("UserId", userId, TimeSpan.FromHours(1));

            return (userId, tenantId);
        }

        /// <summary>
        /// Generates a unique cache key based on UserId and TenantId.
        /// </summary>
        /// <returns>A string representing the cache key.</returns>
        private async Task<string> GetCacheKeyAsync()
        {
            var (userId, tenantId) = await GetUserIDsAsync();

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID is null or empty. Using 'Anonymous' as fallback.");
                userId = "Anonymous";
            }

            return $"{userId}_{tenantId}_TimeZone";
        }

        /// <summary>
        /// Retrieves the user's time zone, either from cache or by fetching from the Graph API.
        /// </summary>
        /// <returns>A string representing the user's time zone.</returns>
        public async Task<string> GetTimeZoneAsync()
        {
            var cacheKey = await GetCacheKeyAsync();
            _logger.LogInformation($"Attempting to retrieve time zone for cache key: {cacheKey}");

            if (!_cache.TryGetValue(cacheKey, out string timeZone))
            {
                _logger.LogInformation("Time zone not found in cache. Fetching from Graph API.");
                try
                {
                    var mailboxSettings = await _graphService.GetMailboxSettingsAsync();

                    if (mailboxSettings != null && !string.IsNullOrEmpty(mailboxSettings.TimeZone))
                    {
                        timeZone = mailboxSettings.TimeZone;
                        _logger.LogInformation($"Retrieved time zone from mailbox settings: {timeZone}");

                        // Store the time zone in cache with the specific cache key
                        _cache.Set(cacheKey, timeZone, TimeSpan.FromHours(1));
                        _logger.LogInformation($"Time zone '{timeZone}' cached with key '{cacheKey}'.");
                    }
                    else
                    {
                        _logger.LogWarning("Mailbox settings or time zone is null. Defaulting to UTC.");
                        timeZone = "UTC";
                        _cache.Set(cacheKey, timeZone, TimeSpan.FromHours(1));
                        _logger.LogInformation($"Default time zone 'UTC' cached with key '{cacheKey}'.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error retrieving time zone from mailbox settings: {ex.Message}");
                    timeZone = "UTC";
                    _cache.Set(cacheKey, timeZone, TimeSpan.FromHours(1));
                    _logger.LogInformation($"Error occurred. Default time zone 'UTC' cached with key '{cacheKey}'.");
                }
            }
            else
            {
                _logger.LogInformation($"Time zone retrieved from cache: {timeZone}");
            }

            return timeZone;
        }

        /// <summary>
        /// Converts a UTC DateTime to the user's local time zone.
        /// </summary>
        /// <param name="dateTime">The UTC DateTime to convert.</param>
        /// <returns>The converted local DateTime.</returns>
        public async Task<DateTime> ConvertToUserTimeZoneAsync(DateTime dateTime)
        {
            try
            {
                string userTimeZoneId = await GetTimeZoneAsync();
                _logger.LogInformation($"Converting time. User's time zone ID: {userTimeZoneId}");

                TimeZoneInfo userTimeZone;
                try
                {
                    // Attempt to find the time zone directly
                    userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
                }
                catch (TimeZoneNotFoundException)
                {
                    _logger.LogWarning($"Time zone ID '{userTimeZoneId}' not found. Attempting to convert using TimeZoneConverter.");
                    // Convert Windows to IANA time zone ID
                    string ianaTimeZone = TZConvert.WindowsToIana(userTimeZoneId);
                    userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(ianaTimeZone);
                }

                DateTime userLocalTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), userTimeZone);

                _logger.LogInformation($"Time conversion result. Input (UTC): {dateTime.ToUniversalTime()}, Output (Local): {userLocalTime}, Time zone: {userTimeZone.DisplayName}");

                return userLocalTime;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error converting time: {ex.Message}. Defaulting to UTC.");
                return dateTime.ToUniversalTime();
            }
        }

        /// <summary>
        /// Synchronous method to get the user's time zone. Should be used sparingly.
        /// </summary>
        /// <returns>A string representing the user's time zone.</returns>
        public string GetTimeZone()
        {
            return GetTimeZoneAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// Synchronous method to convert UTC DateTime to the user's local time zone. Should be used sparingly.
        /// </summary>
        /// <param name="dateTime">The UTC DateTime to convert.</param>
        /// <returns>The converted local DateTime.</returns>
        public DateTime ConvertToUserTimeZone(DateTime dateTime)
        {
            return ConvertToUserTimeZoneAsync(dateTime).GetAwaiter().GetResult();
        }
    }
}
