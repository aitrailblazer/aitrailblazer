using Microsoft.JSInterop;
using System.Threading.Tasks;

public class TimeZoneService
{
    private readonly IJSRuntime _jsRuntime;

    public TimeZoneService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> GetTimeZoneAsync()
    {
        // Check if time zone is already stored
        string storedTimeZone = await _jsRuntime.InvokeAsync<string>("timezoneHelper.getStoredTimeZone");
        if (!string.IsNullOrEmpty(storedTimeZone))
        {
            return storedTimeZone;
        }

        // If not stored, get the time zone and store it
        string timeZone = await _jsRuntime.InvokeAsync<string>("timezoneHelper.getTimeZone");
        await _jsRuntime.InvokeVoidAsync("timezoneHelper.setTimeZone", timeZone);
        return timeZone;
    }
}
