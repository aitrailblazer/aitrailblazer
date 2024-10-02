using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using aitrailblazer.net.Services;
using System;
using System.Collections.Generic;

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
        kernel.Plugins.AddFromFunctions("time_plugin_ai",
                new[]
                {
                    // Current Time and Date for AI
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string>(_timeFunctions.GetCurrentTimeForAI),
                        functionName: "get_current_time_ai",
                        description: "Retrieve the current local date and time formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string>(_timeFunctions.GetTodayForAI),
                        functionName: "today_ai",
                        description: "Retrieve today's date formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string>(_timeFunctions.GetNowForAI),
                        functionName: "now_ai",
                        description: "Retrieve the current date without time component formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, string>(_timeFunctions.IsBusinessDayForAI),
                        functionName: "is_business_day_ai",
                        description: "Check if a given date is a business day (Monday to Friday) and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string>(_timeFunctions.GetDayOfWeekForAI),
                        functionName: "get_day_of_week_ai",
                        description: "Retrieve the current day of the week formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<int, int, int, int, int, int, string, string, string>(_timeFunctions.ConvertBetweenTimeZonesForAI),
                        functionName: "convert_between_time_zones_ai",
                        description: "Convert a date and time from one time zone to another and return the result formatted for AI interactions."
                    ),

                    // Newly Added AI-Exposed Wrapper Functions
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<int, int, int, string>(_timeFunctions.CreateDateForAI),
                        functionName: "create_date_ai",
                        description: "Create a date from year, month, and day and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<int, int, int, string>(_timeFunctions.CreateTimeForAI),
                        functionName: "create_time_ai",
                        description: "Create a time from hour, minute, and second and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, string>(_timeFunctions.CreateDateIntervalForAI),
                        functionName: "create_date_interval_ai",
                        description: "Create a date interval between two dates and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, string, string>(_timeFunctions.DateStringForAI),
                        functionName: "date_string_ai",
                        description: "Format a date with a specified format and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string, string, string>(_timeFunctions.FromDateStringForAI),
                        functionName: "from_date_string_ai",
                        description: "Parse a date string with a specified format and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, string>(_timeFunctions.DateDifferenceDaysForAI),
                        functionName: "date_difference_days_ai",
                        description: "Calculate the difference between two dates in days and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, string>(_timeFunctions.DateDifferenceHoursForAI),
                        functionName: "date_difference_hours_ai",
                        description: "Calculate the difference between two dates in hours and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, string>(_timeFunctions.DateRangeForAI),
                        functionName: "date_range_ai",
                        description: "Generate a list of dates between two dates and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, string>(_timeFunctions.DiffTimeForAI),
                        functionName: "diff_time_ai",
                        description: "Calculate the difference between two times in seconds and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string, string>(_timeFunctions.LocalTimeForAI),
                        functionName: "local_time_ai",
                        description: "Retrieve the local time information for a specified time zone or the user's time zone, formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, string, string, string>(_timeFunctions.TimezoneConvertForAI),
                        functionName: "timezone_convert_ai",
                        description: "Convert a DateTime from one time zone to another and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string, string, string>(_timeFunctions.TimezoneOffsetForAI),
                        functionName: "timezone_offset_ai",
                        description: "Calculate the offset in hours between two time zones and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<List<DateTime>, string>(_timeFunctions.GetDateBoundsForAI),
                        functionName: "get_date_bounds_ai",
                        description: "Find the earliest and latest dates from a list and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, CalendarType, string>(_timeFunctions.TimesystemConvertForAI),
                        functionName: "timesystem_convert_ai",
                        description: "Convert a DateTime object to a specified calendar system and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, DateTime, string>(_timeFunctions.DateWithinQForAI),
                        functionName: "date_within_q_ai",
                        description: "Determine if a date is within a specified date range and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, DateTime, DateTime, string>(_timeFunctions.DateOverlapsQForAI),
                        functionName: "date_overlaps_q_ai",
                        description: "Determine if two date ranges overlap and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, string>(_timeFunctions.LeapYearQForAI),
                        functionName: "leap_year_q_ai",
                        description: "Determine if a given date's year is a leap year and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, string>(_timeFunctions.DayRangeForAI),
                        functionName: "day_range_ai",
                        description: "Generate a list of days between two dates and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, string>(_timeFunctions.BusinessDayQForAI),
                        functionName: "business_day_q_ai",
                        description: "Determine if a given date is a business day and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<List<DateTime>, string>(_timeFunctions.MeanDateForAI),
                        functionName: "mean_date_ai",
                        description: "Calculate the mean (average) date from a list of dates and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<List<DateTime>, string>(_timeFunctions.MedianDateForAI),
                        functionName: "median_date_ai",
                        description: "Calculate the median date from a list of dates and return the result formatted for AI interactions."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<List<DateTime>, string, string>(_timeFunctions.DateSelectForAI),
                        functionName: "date_select_ai",
                        description: "Select dates from a list based on specified criteria and return the result formatted for AI interactions."
                    )
                });

        _logger.LogInformation("Kernel setup completed successfully.");
        return kernel;
    }
}
