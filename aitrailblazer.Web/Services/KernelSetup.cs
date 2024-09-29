using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using aitrailblazer.net.Services; 
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
                    method: new Func<DateTime>(_timeFunctions.GetCurrentTime),
                    functionName: "get_current_time",
                    description: "Retrieve the current local date and time based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime>(_timeFunctions.GetToday),
                    functionName: "today",
                    description: "Retrieve today's date based on the user's time zone."
                ),
                /*
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime>(_timeFunctions.GetTomorrow),
                    functionName: "tomorrow",
                    description: "Retrieve tomorrow's date based on the user's time zone."
                ),

                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime>(_timeFunctions.GetYesterday),
                    functionName: "yesterday",
                    description: "Retrieve yesterday's date based on the user's time zone."
                ),
                */
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime>(_timeFunctions.GetNow),
                    functionName: "now",
                    description: "Retrieve the current date without time component based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, bool>(_timeFunctions.IsBusinessDayFunction),
                    functionName: "is_business_day",
                    description: "Check if a given date is a business day (Monday to Friday) based on the user's time zone."
                ),

                // Date and Time Representation
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<int, int, int, DateTime>(_timeFunctions.CreateDate),
                    functionName: "create_date",
                    description: "Create a DateTime object from year, month, and day based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<int, int, int, TimeSpan>(_timeFunctions.CreateTime),
                    functionName: "create_time",
                    description: "Create a TimeSpan object representing a time of day."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, TimeSpan>(_timeFunctions.CreateDateInterval),
                    functionName: "create_date_interval",
                    description: "Create a TimeSpan representing the interval between two dates based on the user's time zone."
                ),

                // Date and Time Formatting
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, string, string>(_timeFunctions.DateString),
                    functionName: "date_string",
                    description: "Convert a DateTime object to a string with the specified format based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, DateTime>(_timeFunctions.FromDateString),
                    functionName: "from_date_string",
                    description: "Convert a date string to a DateTime object using the specified format based on the user's time zone."
                ),

                // Time Calculations
                /*
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, int, DateTime>(_timeFunctions.DatePlusDays),
                    functionName: "date_plus_days",
                    description: "Add or subtract days from a DateTime object based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, int, DateTime>(_timeFunctions.DatePlusWeeks),
                    functionName: "date_plus_weeks",
                    description: "Add or subtract weeks from a DateTime object based on the user's time zone."
                ),
                */
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, double>(_timeFunctions.DateDifferenceDays),
                    functionName: "date_difference_days",
                    description: "Calculate the difference between two dates in days based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, double>(_timeFunctions.DateDifferenceHours),
                    functionName: "date_difference_hours",
                    description: "Calculate the difference between two dates in hours based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, List<DateTime>>(_timeFunctions.DateRange),
                    functionName: "date_range",
                    description: "Generate a list of dates between two dates based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, double>(_timeFunctions.DiffTime),
                    functionName: "diff_time",
                    description: "Get the difference between two times in seconds based on the user's time zone."
                ),

                // Time Zone Handling
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_timeFunctions.LocalTime),
                    functionName: "local_time",
                    description: "Retrieve the local time for a specified time zone or the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, string, string, string>(_timeFunctions.TimezoneConvert),
                    functionName: "timezone_convert",
                    description: "Convert a DateTime from one time zone to another based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, double>(_timeFunctions.TimezoneOffset),
                    functionName: "timezone_offset",
                    description: "Calculate the offset in hours between two time zones based on the user's time zone."
                ),

                // Date and Time Operations
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<List<DateTime>, Func<DateTime, bool>, List<DateTime>>(_timeFunctions.DateSelect),
                    functionName: "date_select",
                    description: "Select dates from a list based on specified criteria and the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<List<DateTime>, DateBounds>(_timeFunctions.GetDateBounds),
                    functionName: "date_bounds",
                    description: "Find the earliest and latest dates from a list based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, CalendarType, string>(_timeFunctions.TimesystemConvert),
                    functionName: "timesystem_convert",
                    description: "Convert a DateTime object to a specified calendar system based on the user's time zone."
                ),

                // Date and Time Testing
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, DateTime, bool>(_timeFunctions.DateWithinQ),
                    functionName: "date_within_q",
                    description: "Determine if a date is within a specified date range based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, DateTime, DateTime, bool>(_timeFunctions.DateOverlapsQ),
                    functionName: "date_overlaps_q",
                    description: "Determine if two date ranges overlap based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, bool>(_timeFunctions.LeapYearQ),
                    functionName: "leap_year_q",
                    description: "Determine if the year of a given date is a leap year based on the user's time zone."
                ),

                // Specialized Day Operations
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, DateTime, List<DateTime>>(_timeFunctions.DayRange),
                    functionName: "day_range",
                    description: "Generate a list of days between two dates based on the user's time zone."
                ),
                /*
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, int, DateTime>(_timeFunctions.DayPlus),
                    functionName: "day_plus",
                    description: "Add or subtract days from a given date based on the user's time zone."
                ),
                */
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, bool>(_timeFunctions.BusinessDayQ),
                    functionName: "business_day_q",
                    description: "Determine if a given date is a business day (Monday to Friday) based on the user's time zone."
                ),

                // Statistical Operations on Dates and Times
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<List<DateTime>, DateTime?>(_timeFunctions.MeanDate),
                    functionName: "mean_date",
                    description: "Calculate the mean (average) date from a list of dates based on the user's time zone."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<List<DateTime>, DateTime?>(_timeFunctions.MedianDate),
                    functionName: "median_date",
                    description: "Calculate the median date from a list of dates based on the user's time zone."
                )
            });

        _logger.LogInformation("Kernel setup completed successfully.");
        return kernel;
    }
}
