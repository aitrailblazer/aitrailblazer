using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using aitrailblazer.net.Services;
using System.Threading.Tasks;

public class KernelSetup
{
    private readonly TimeFunctions _timeFunctions;
    private readonly ILogger<KernelSetup> _logger;

    public KernelSetup(TimeFunctions timeFunctions, ILogger<KernelSetup> logger)
    {
        _timeFunctions = timeFunctions;
        _logger = logger;
    }

    public Kernel SetupKernelTimePlugin(Kernel kernel)
    {
        // Add date and time-related functions to the kernel
        kernel.Plugins.AddFromFunctions("time_plugin",
            new[]
            {
                // Current Time and Date Functions
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<Task<DateTime>>(_timeFunctions.GetCurrentTimeAsync),
                    functionName: "get_current_time",
                    description: "Retrieve the current local date and time based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<Task<DateTime>>(_timeFunctions.GetTodayAsync),
                    functionName: "today",
                    description: "Retrieve today's date based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<Task<DateTime>>(_timeFunctions.GetNowAsync),
                    functionName: "now",
                    description: "Retrieve the current date without time component based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, Task<bool>>(_timeFunctions.IsBusinessDayFunctionAsync),
                    functionName: "is_business_day",
                    description: "Check if a given date is a business day (Monday to Friday) based on the user's time zone."
                ),

                // Date and Time Representation
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<int, int, int, Task<DateTime>>(_timeFunctions.CreateDateAsync),
                    functionName: "create_date",
                    description: "Create a DateTime object from year, month, and day based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<int, int, int, TimeSpan>(_timeFunctions.CreateTime),
                    functionName: "create_time",
                    description: "Create a TimeSpan object representing a time of day."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, Task<TimeSpan>>(_timeFunctions.CreateDateIntervalAsync),
                    functionName: "create_date_interval",
                    description: "Create a TimeSpan representing the interval between two dates based on the user's time zone."
                ),

                // Date and Time Formatting
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, string, Task<string>>(_timeFunctions.DateStringAsync),
                    functionName: "date_string",
                    description: "Convert a DateTime object to a string with the specified format based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, Task<DateTime>>(_timeFunctions.FromDateStringAsync),
                    functionName: "from_date_string",
                    description: "Convert a date string to a DateTime object using the specified format based on the user's time zone."
                ),

                // Time Calculations
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, Task<double>>(_timeFunctions.DateDifferenceDaysAsync),
                    functionName: "date_difference_days",
                    description: "Calculate the difference between two dates in days based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, Task<double>>(_timeFunctions.DateDifferenceHoursAsync),
                    functionName: "date_difference_hours",
                    description: "Calculate the difference between two dates in hours based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, Task<List<DateTime>>>(_timeFunctions.DateRangeAsync),
                    functionName: "date_range",
                    description: "Generate a list of dates between two dates based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, Task<double>>(_timeFunctions.DiffTimeAsync),
                    functionName: "diff_time",
                    description: "Get the difference between two times in seconds based on the user's time zone."
                ),

                // Time Zone Handling
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, Task<string>>(_timeFunctions.LocalTimeAsync),
                    functionName: "local_time",
                    description: "Retrieve the local time for a specified time zone or the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, string, string, Task<string>>(_timeFunctions.TimezoneConvertAsync),
                    functionName: "timezone_convert",
                    description: "Convert a DateTime from one time zone to another based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, Task<double>>(_timeFunctions.TimezoneOffsetAsync),
                    functionName: "timezone_offset",
                    description: "Calculate the offset in hours between two time zones based on the user's time zone."
                ),

                // Date and Time Operations
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<List<DateTime>, Func<DateTime, bool>, Task<List<DateTime>>>(_timeFunctions.DateSelectAsync),
                    functionName: "date_select",
                    description: "Select dates from a list based on specified criteria and the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<List<DateTime>, Task<DateBounds>>(_timeFunctions.GetDateBoundsAsync),
                    functionName: "date_bounds",
                    description: "Find the earliest and latest dates from a list based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, CalendarType, Task<string>>(_timeFunctions.TimesystemConvertAsync),
                    functionName: "timesystem_convert",
                    description: "Convert a DateTime object to a specified calendar system based on the user's time zone."
                ),

                // Date and Time Testing
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, DateTime, Task<bool>>(_timeFunctions.DateWithinQAsync),
                    functionName: "date_within_q",
                    description: "Determine if a date is within a specified date range based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, DateTime, DateTime, Task<bool>>(_timeFunctions.DateOverlapsQAsync),
                    functionName: "date_overlaps_q",
                    description: "Determine if two date ranges overlap based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, Task<bool>>(_timeFunctions.LeapYearQAsync),
                    functionName: "leap_year_q",
                    description: "Determine if the year of a given date is a leap year based on the user's time zone."
                ),

                // Specialized Day Operations
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, Task<List<DateTime>>>(_timeFunctions.DayRangeAsync),
                    functionName: "day_range",
                    description: "Generate a list of days between two dates based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, Task<bool>>(_timeFunctions.BusinessDayQAsync),
                    functionName: "business_day_q",
                    description: "Determine if a given date is a business day (Monday to Friday) based on the user's time zone."
                ),

                // Statistical Operations on Dates and Times
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<List<DateTime>, Task<DateTime?>>(_timeFunctions.MeanDateAsync),
                    functionName: "mean_date",
                    description: "Calculate the mean (average) date from a list of dates based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<List<DateTime>, Task<DateTime?>>(_timeFunctions.MedianDateAsync),
                    functionName: "median_date",
                    description: "Calculate the median date from a list of dates based on the user's time zone."
                )
            });

        _logger.LogInformation("Kernel setup completed successfully.");
        return kernel;
    }
}