using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

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

        public async Task ListAllClaims()
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims
                .Select(c => new
                {
                    Type = c.Type,
                    Value = c.Value
                });

            foreach (var claim in claims)
            {
                _logger.LogInformation($"UserClaim {claim.Type}: {claim.Value}");
            }
        }

        public async Task<(string UserId, string TenantId)> GetUserIDs()
        {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;

            var userId = claims?.FirstOrDefault(c => c.Type == "uid")?.Value ?? "currentUserIdentityID";
            var tenantId = claims?.FirstOrDefault(c => c.Type == "utid")?.Value ?? "currentTenantID";

            _logger.LogInformation($"UserClaim uid: {userId}");
            _logger.LogInformation($"UserClaim utid: {tenantId}");

            // Store tenantId and userId in cache
            _cache.Set("TenantId", tenantId, TimeSpan.FromHours(1));
            _cache.Set("UserId", userId, TimeSpan.FromHours(1));

            return (userId, tenantId);
        }

        private string GetCacheKey()
        {
            var (userId, tenantId) = GetUserIDs().GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID is null or empty. Cannot generate cache key.");
                userId = "Anonymous";
            }

            return $"{userId}_{tenantId}_TimeZone";
        }

        public async Task<string> GetTimeZoneAsync()
        {
            var cacheKey = GetCacheKey();
            _logger.LogInformation($"Attempting to retrieve time zone for cache key: {cacheKey}");

            if (!_cache.TryGetValue(cacheKey, out string timeZone))
            {
                try
                {
                    _logger.LogInformation("Time zone not found in cache. Fetching from Graph API.");
                    var mailboxSettings = await _graphService.GetMailboxSettingsAsync();

                    if (mailboxSettings != null && !string.IsNullOrEmpty(mailboxSettings.TimeZone))
                    {
                        timeZone = mailboxSettings.TimeZone;
                        _logger.LogInformation($"Retrieved time zone from mailbox settings: {timeZone}");

                        // Store the time zone in cache along with tenantId and userId
                        _cache.Set(cacheKey, timeZone, TimeSpan.FromHours(1));
                        _cache.Set("TimeZone", timeZone, TimeSpan.FromHours(1));
                    }
                    else
                    {
                        _logger.LogWarning("Mailbox settings or time zone is null. Defaulting to UTC.");
                        timeZone = "UTC";
                        _cache.Set(cacheKey, timeZone, TimeSpan.FromHours(1));
                        _cache.Set("TimeZone", timeZone, TimeSpan.FromHours(1));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error retrieving time zone from mailbox settings: {ex.Message}");
                    timeZone = "UTC";
                    _cache.Set(cacheKey, timeZone, TimeSpan.FromHours(1));
                    _cache.Set("TimeZone", timeZone, TimeSpan.FromHours(1));
                }
            }
            else
            {
                _logger.LogInformation($"Time zone retrieved from cache: {timeZone}");
            }

            return timeZone;
        }

        public async Task<DateTime> ConvertToUserTimeZoneAsync(DateTime dateTime)
        {
            try
            {
                string userTimeZoneId = await GetTimeZoneAsync();
                _logger.LogInformation($"Converting time. User's time zone ID: {userTimeZoneId}");

                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
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

        public string GetTimeZone()
        {
            return GetTimeZoneAsync().GetAwaiter().GetResult();
        }

        public DateTime ConvertToUserTimeZone(DateTime dateTime)
        {
            return ConvertToUserTimeZoneAsync(dateTime).GetAwaiter().GetResult();
        }
    }
}
