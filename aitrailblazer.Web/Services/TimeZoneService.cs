using Microsoft.JSInterop;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace aitrailblazer.Web.Services
{
    public class TimeZoneService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ILogger<TimeZoneService> _logger;
        private string _cachedTimeZone;

        public TimeZoneService(IJSRuntime jsRuntime, ILogger<TimeZoneService> logger)
        {
            _jsRuntime = jsRuntime;
            _logger = logger;
        }

        // Existing Asynchronous Methods
        public async Task<string> GetTimeZoneAsync()
        {
            if (string.IsNullOrEmpty(_cachedTimeZone))
            {
                try
                {
                    _cachedTimeZone = await _jsRuntime.InvokeAsync<string>("timezoneHelper.getTimeZone");
                    _logger.LogInformation($"Retrieved time zone: {_cachedTimeZone}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error retrieving time zone: {ex.Message}");
                    _cachedTimeZone = "UTC"; // Default to UTC if there's an error
                }
            }
            return _cachedTimeZone;
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

        // New Synchronous Methods
        public string GetTimeZone()
        {
            // Synchronously wait for the asynchronous method to complete
            return GetTimeZoneAsync().GetAwaiter().GetResult();
        }

        public DateTime ConvertToUserTimeZone(DateTime dateTime)
        {
            // Synchronously wait for the asynchronous method to complete
            return ConvertToUserTimeZoneAsync(dateTime).GetAwaiter().GetResult();
        }
    }
}
