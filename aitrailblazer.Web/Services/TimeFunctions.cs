using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using aitrailblazer.Web.Services;

namespace aitrailblazer.net.Services
{
    public class DateBounds
    {
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
    }

    public enum CalendarType
    {
        Gregorian,
        Hebrew,
        Islamic,
        Japanese,
    }

    public class TimeFunctions
    {
        private readonly TimeZoneService _timeZoneService;
        private readonly ILogger<TimeFunctions> _logger;

        public TimeFunctions(TimeZoneService timeZoneService, ILogger<TimeFunctions> logger)
        {
            _timeZoneService = timeZoneService;
            _logger = logger;
        }
        public async Task<string> GetUserTimeZoneAsync()
        {
            string timeZoneId = await _timeZoneService.GetTimeZoneAsync();
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            DateTime localNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            
            string offsetString = timeZone.BaseUtcOffset.ToString(@"hh\:mm");
            string offsetDirection = timeZone.BaseUtcOffset >= TimeSpan.Zero ? "+" : "-";
            
            string result = $"User's time zone: {timeZoneId}\n" +
                            $"Display name: {timeZone.DisplayName}\n" +
                            $"Current local time: {localNow:yyyy-MM-dd HH:mm:ss}\n" +
                            $"UTC offset: UTC{offsetDirection}{offsetString}";
            
            _logger.LogInformation($"Function 'get_user_time_zone' called. Returning:\n{result}");
            return result;
        }

        public async Task<DateTime> ConvertToUserTimeZoneAsync(DateTime dateTime)
        {
            return await _timeZoneService.ConvertToUserTimeZoneAsync(dateTime);
        }
        public TimeSpan StringToTimeSpan(string timeString)
        {
            try
            {
                if (TimeSpan.TryParse(timeString, out TimeSpan result))
                {
                    _logger.LogInformation($"Function 'StringToTimeSpan' called with input: '{timeString}'. Returning: {result}");
                    return result;
                }
                else
                {
                    throw new ArgumentException($"Invalid time format: {timeString}. Expected format: HH:mm:ss or HH:mm");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Function 'StringToTimeSpan' error: {ex.Message}");
                throw;
            }
        }
        public async Task<DateTime> GetCurrentTimeAsync()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime userLocalTime = await ConvertToUserTimeZoneAsync(utcNow);
            _logger.LogInformation($"Function 'get_current_time' called. Returning: {userLocalTime}");
            return userLocalTime;
        }

        public async Task<DateTime> GetTodayAsync()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime userLocalNow = await ConvertToUserTimeZoneAsync(utcNow);
            DateTime userLocalToday = userLocalNow.Date;
            _logger.LogInformation($"Function 'today' called. UTC Now: {utcNow}, User Local Now: {userLocalNow}, Returning: {userLocalToday}");
            return userLocalToday;
        }

        public async Task<DateTime> GetNowAsync()
        {
            DateTime utcNowDate = DateTime.UtcNow.Date;
            DateTime userLocalNowDate = await ConvertToUserTimeZoneAsync(utcNowDate);
            _logger.LogInformation($"Function 'now' called. Returning: {userLocalNowDate}");
            return userLocalNowDate;
        }

        public async Task<bool> IsBusinessDayFunctionAsync(DateTime date)
        {
            DateTime userLocalDate = await ConvertToUserTimeZoneAsync(date);
            bool isBusiness = userLocalDate.DayOfWeek != DayOfWeek.Saturday && userLocalDate.DayOfWeek != DayOfWeek.Sunday;
            _logger.LogInformation($"Function 'is_business_day' called with date: {userLocalDate}. Returning: {isBusiness}");
            return isBusiness;
        }

        public async Task<DateTime> CreateDateAsync(int year, int month, int day)
        {
            string userTimeZoneId = await _timeZoneService.GetTimeZoneAsync();
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);

            // Create the date in the user's time zone
            DateTime userLocalDate = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Unspecified);
            
            // Convert to UTC for internal consistency
            DateTime dateUtc = TimeZoneInfo.ConvertTimeToUtc(userLocalDate, userTimeZone);
            
            // Convert back to user's time zone (this step might seem redundant, but it ensures consistency)
            DateTime resultDate = TimeZoneInfo.ConvertTimeFromUtc(dateUtc, userTimeZone);

            _logger.LogInformation($"Function 'create_date' called with year: {year}, month: {month}, day: {day}. " +
                                $"User time zone: {userTimeZoneId}. Returning: {resultDate} ({resultDate.Kind})");
            return resultDate;
        }

        public TimeSpan CreateTime(int hour, int minute, int second)
        {
            TimeSpan createdTime = new TimeSpan(hour, minute, second);
            _logger.LogInformation($"Function 'create_time' called with hour: {hour}, minute: {minute}, second: {second}. Returning: {createdTime}");
            return createdTime;
        }

        public async Task<TimeSpan> CreateDateIntervalAsync(DateTime start, DateTime end)
        {
            DateTime userStart = await ConvertToUserTimeZoneAsync(start);
            DateTime userEnd = await ConvertToUserTimeZoneAsync(end);
            TimeSpan interval = userEnd - userStart;
            _logger.LogInformation($"Function 'create_date_interval' called with start: {userStart}, end: {userEnd}. Returning interval: {interval}");
            return interval;
        }

        public async Task<string> DateStringAsync(DateTime date, string format)
        {
            DateTime userDate = await ConvertToUserTimeZoneAsync(date);
            DateTime specifiedDate = DateTime.SpecifyKind(userDate.Date, DateTimeKind.Unspecified);
            string formatted = specifiedDate.ToString(format, CultureInfo.InvariantCulture);
            _logger.LogInformation($"Function 'date_string' called with date: {specifiedDate}, format: '{format}'. Returning: {formatted}");
            return formatted;
        }

        public async Task<DateTime> FromDateStringAsync(string dateString, string format)
        {
            // Parse the date string as an unspecified DateTime (neither local nor UTC)
            DateTime parsedDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            // Get the user's time zone
            string userTimeZoneId = await _timeZoneService.GetTimeZoneAsync();
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);

            // Specify that the parsed date is in the user's local time zone
            DateTimeOffset userLocalDate = new DateTimeOffset(parsedDate, userTimeZone.GetUtcOffset(parsedDate));

            _logger.LogInformation($"Function 'from_date_string' called with dateString: '{dateString}', format: '{format}'. " +
                                $"Interpreted as local time: {userLocalDate.DateTime} {userTimeZone.DisplayName}");

            // Return the local DateTime
            return userLocalDate.DateTime;
        }
        public string ConvertBetweenTimeZones(int year, int month, int day, int hour, int minute, int second, string fromTimeZone, string toTimeZone)
        {
            try
            {
                TimeZoneInfo fromTZ = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZone);
                TimeZoneInfo toTZ = TimeZoneInfo.FindSystemTimeZoneById(toTimeZone);

                // Create the datetime in the 'from' timezone
                DateTimeOffset fromDate = new DateTimeOffset(year, month, day, hour, minute, second, fromTZ.GetUtcOffset(new DateTime(year, month, day, hour, minute, second)));

                // Convert to the target time zone
                DateTimeOffset convertedTime = TimeZoneInfo.ConvertTime(fromDate, toTZ);

                string result = $"Input: {fromDate:yyyy-MM-dd HH:mm:ss} {fromTimeZone}\n" +
                                $"Output: {convertedTime:yyyy-MM-dd HH:mm:ss} {toTimeZone}\n" +
                                $"UTC Offset of result: {toTZ.GetUtcOffset(convertedTime):hh\\:mm}";

                _logger.LogInformation($"Function 'ConvertBetweenTimeZones' called. {result}");
                return result;
            }
            catch (Exception ex)
            {
                string error = $"Error converting time: {ex.Message}";
                _logger.LogError($"Function 'ConvertBetweenTimeZones' error: {error}");
                return error;
            }
        }
        public async Task<double> DateDifferenceDaysAsync(DateTime start, DateTime end)
        {
            DateTime userStart = await ConvertToUserTimeZoneAsync(start);
            DateTime userEnd = await ConvertToUserTimeZoneAsync(end);
            double totalDays = (userEnd - userStart).TotalDays;
            _logger.LogInformation($"Function 'date_difference_days' called with start: {userStart}, end: {userEnd}. Returning: {totalDays} days");
            return totalDays;
        }

        public async Task<double> DateDifferenceHoursAsync(DateTime start, DateTime end)
        {
            DateTime userStart = await ConvertToUserTimeZoneAsync(start);
            DateTime userEnd = await ConvertToUserTimeZoneAsync(end);
            double totalHours = (userEnd - userStart).TotalHours;
            _logger.LogInformation($"Function 'date_difference_hours' called with start: {userStart}, end: {userEnd}. Returning: {totalHours} hours");
            return totalHours;
        }

        public async Task<List<DateTime>> DateRangeAsync(DateTime start, DateTime end)
        {
            DateTime userStart = await ConvertToUserTimeZoneAsync(start);
            DateTime userEnd = await ConvertToUserTimeZoneAsync(end);
            List<DateTime> dateRange = Enumerable.Range(0, (userEnd - userStart).Days + 1)
                .Select(d => userStart.AddDays(d))
                .ToList();
            string dateList = string.Join(", ", dateRange.Select(d => d.ToString("MMMM dd, yyyy")));
            _logger.LogInformation($"Function 'date_range' called with start: {userStart}, end: {userEnd}. Returning: {dateList}");
            return dateRange;
        }

        public async Task<double> DiffTimeAsync(DateTime start, DateTime end)
        {
            DateTime userStart = await ConvertToUserTimeZoneAsync(start);
            DateTime userEnd = await ConvertToUserTimeZoneAsync(end);
            double totalSeconds = (userEnd - userStart).TotalSeconds;
            _logger.LogInformation($"Function 'diff_time' called with start: {userStart}, end: {userEnd}. Returning: {totalSeconds} seconds");
            return totalSeconds;
        }

        public async Task<string> LocalTimeAsync(string timeZoneId = null)
        {
            string effectiveTimeZoneId = timeZoneId ?? await _timeZoneService.GetTimeZoneAsync();

            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(effectiveTimeZoneId);
                DateTime utcNow = DateTime.UtcNow;
                DateTime localTime = TimeZoneInfo.ConvertTime(utcNow, tz);
                
                string offsetString = tz.BaseUtcOffset.ToString(@"hh\:mm");
                string offsetDirection = tz.BaseUtcOffset >= TimeSpan.Zero ? "+" : "-";
                
                string result = $"Time zone: {effectiveTimeZoneId}\n" +
                                $"Display name: {tz.DisplayName}\n" +
                                $"Local time: {localTime:MMMM dd, yyyy HH:mm:ss}\n" +
                                $"UTC offset: UTC{offsetDirection}{offsetString}";
                
                _logger.LogInformation($"Function 'local_time' called with timeZoneId: '{effectiveTimeZoneId}'. Returning:\n{result}");
                return result;
            }
            catch (TimeZoneNotFoundException)
            {
                string error = $"Error: Time zone '{effectiveTimeZoneId}' not found.";
                _logger.LogError($"Function 'local_time' error: {error}");
                throw new ArgumentException(error);
            }
            catch (InvalidTimeZoneException)
            {
                string error = $"Error: Time zone '{effectiveTimeZoneId}' is invalid.";
                _logger.LogError($"Function 'local_time' error: {error}");
                throw new ArgumentException(error);
            }
        }

        public async Task<string> TimezoneConvertAsync(DateTime dateTime, string fromTimeZoneId, string toTimeZoneId)
        {
            try
            {
                TimeZoneInfo fromTZ = TimeZoneInfo.FindSystemTimeZoneById(fromTimeZoneId);
                TimeZoneInfo toTZ = TimeZoneInfo.FindSystemTimeZoneById(toTimeZoneId);
                DateTime convertedTime = TimeZoneInfo.ConvertTime(dateTime, fromTZ, toTZ);
                string result = convertedTime.ToString("MMMM dd, yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                _logger.LogInformation($"Function 'timezone_convert' called with dateTime: {dateTime}, fromTimeZoneId: '{fromTimeZoneId}', toTimeZoneId: '{toTimeZoneId}'. Returning: {result}");
                return result;
            }
            catch (TimeZoneNotFoundException)
            {
                string error = $"Error: One of the time zones '{fromTimeZoneId}' or '{toTimeZoneId}' was not found.";
                _logger.LogError($"Function 'timezone_convert' error: {error}");
                throw new ArgumentException(error);
            }
            catch (InvalidTimeZoneException)
            {
                string error = $"Error: One of the time zones '{fromTimeZoneId}' or '{toTimeZoneId}' is invalid.";
                _logger.LogError($"Function 'timezone_convert' error: {error}");
                throw new ArgumentException(error);
            }
        }

        public async Task<double> TimezoneOffsetAsync(string timeZoneId1, string timeZoneId2)
        {
            try
            {
                TimeZoneInfo tz1 = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId1);
                TimeZoneInfo tz2 = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId2);
                double offset = (tz2.BaseUtcOffset - tz1.BaseUtcOffset).TotalHours;
                _logger.LogInformation($"Function 'timezone_offset' called with timeZoneId1: '{timeZoneId1}', timeZoneId2: '{timeZoneId2}'. Returning: {offset} hours");
                return offset;
            }
            catch (TimeZoneNotFoundException)
            {
                string error = $"Error: One of the time zones '{timeZoneId1}' or '{timeZoneId2}' was not found.";
                _logger.LogError($"Function 'timezone_offset' error: {error}");
                throw new ArgumentException(error);
            }
            catch (InvalidTimeZoneException)
            {
                string error = $"Error: One of the time zones '{timeZoneId1}' or '{timeZoneId2}' is invalid.";
                _logger.LogError($"Function 'timezone_offset' error: {error}");
                throw new ArgumentException(error);
            }
        }

        public async Task<List<DateTime>> DateSelectAsync(List<DateTime> dates, Func<DateTime, bool> criteria)
        {
            var userDates = await Task.WhenAll(dates.Select(async d => await ConvertToUserTimeZoneAsync(d)));
            var selectedDates = userDates.Where(criteria).ToList();
            string selectedDateList = string.Join(", ", selectedDates.Select(d => d.ToString("MMMM dd, yyyy")));
            _logger.LogInformation($"Function 'date_select' called with dates count: {dates.Count}. Returning: {selectedDateList}");
            return selectedDates;
        }

        public async Task<DateBounds> GetDateBoundsAsync(List<DateTime> dates)
        {
            if (dates == null || dates.Count == 0)
            {
                _logger.LogWarning("Function 'date_bounds' called with an empty or null list.");
                throw new ArgumentException("Date list cannot be null or empty.", nameof(dates));
            }

            var userDates = await Task.WhenAll(dates.Select(async d => await ConvertToUserTimeZoneAsync(d)));
            DateTime firstDate = userDates.Min();
            DateTime lastDate = userDates.Max();
            _logger.LogInformation($"Function 'date_bounds' called. Returning FirstDate: {firstDate}, LastDate: {lastDate}");
            return new DateBounds { FirstDate = firstDate, LastDate = lastDate };
        }

        public async Task<string> TimesystemConvertAsync(DateTime dateTime, CalendarType calendarType)
        {
            var userDateTime = await ConvertToUserTimeZoneAsync(dateTime);
            var culture = new CultureInfo("en-US");
            switch (calendarType)
            {
                case CalendarType.Hebrew:
                    culture.DateTimeFormat.Calendar = new HebrewCalendar();
                    break;
                case CalendarType.Islamic:
                    culture.DateTimeFormat.Calendar = new HijriCalendar();
                    break;
                case CalendarType.Japanese:
                    culture.DateTimeFormat.Calendar = new JapaneseCalendar();
                    break;
                default:
                    culture.DateTimeFormat.Calendar = new GregorianCalendar();
                    break;
            }
            string convertedDate = userDateTime.ToString("D", culture);
            _logger.LogInformation($"Function 'timesystem_convert' called with dateTime: {userDateTime}, calendarType: {calendarType}. Returning: {convertedDate}");
            return convertedDate;
        }

        public async Task<bool> DateWithinQAsync(DateTime inner, DateTime outerStart, DateTime outerEnd)
        {
            DateTime userInner = await ConvertToUserTimeZoneAsync(inner);
            DateTime userOuterStart = await ConvertToUserTimeZoneAsync(outerStart);
            DateTime userOuterEnd = await ConvertToUserTimeZoneAsync(outerEnd);
            bool isWithin = userInner >= userOuterStart && userInner <= userOuterEnd;
            _logger.LogInformation($"Function 'date_within_q' called with inner: {userInner}, outerStart: {userOuterStart}, outerEnd: {userOuterEnd}. Returning: {isWithin}");
            return isWithin;
        }

        public async Task<bool> DateOverlapsQAsync(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            DateTime userStart1 = await ConvertToUserTimeZoneAsync(start1);
            DateTime userEnd1 = await ConvertToUserTimeZoneAsync(end1);
            DateTime userStart2 = await ConvertToUserTimeZoneAsync(start2);
            DateTime userEnd2 = await ConvertToUserTimeZoneAsync(end2);
            bool overlaps = userStart1 <= userEnd2 && userStart2 <= userEnd1;
            _logger.LogInformation($"Function 'date_overlaps_q' called with start1: {userStart1}, end1: {userEnd1}, start2: {userStart2}, end2: {userEnd2}. Returning: {overlaps}");
            return overlaps;
        }

        public async Task<bool> LeapYearQAsync(DateTime date)
        {
            DateTime userDate = await ConvertToUserTimeZoneAsync(date);
            bool isLeapYear = DateTime.IsLeapYear(userDate.Year);
            _logger.LogInformation($"Function 'leap_year_q' called with date: {userDate}. Returning: {isLeapYear}");
            return isLeapYear;
        }

        public async Task<List<DateTime>> DayRangeAsync(DateTime start, DateTime end)
        {
            DateTime userStart = await ConvertToUserTimeZoneAsync(start);
            DateTime userEnd = await ConvertToUserTimeZoneAsync(end);
            List<DateTime> dayRange = Enumerable.Range(0, (userEnd - userStart).Days + 1)
                .Select(d => userStart.AddDays(d))
                .ToList();
            string dayList = string.Join(", ", dayRange.Select(d => d.ToString("MMMM dd, yyyy")));
            _logger.LogInformation($"Function 'day_range' called with start: {userStart}, end: {userEnd}. Returning: {dayList}");
            return dayRange;
        }

        public async Task<bool> BusinessDayQAsync(DateTime date)
        {
            DateTime userDate = await ConvertToUserTimeZoneAsync(date);
            bool isBusinessDay = userDate.DayOfWeek != DayOfWeek.Saturday && userDate.DayOfWeek != DayOfWeek.Sunday;
            _logger.LogInformation($"Function 'business_day_q' called with date: {userDate}. Returning: {isBusinessDay}");
            return isBusinessDay;
        }

        public async Task<DateTime?> MeanDateAsync(List<DateTime> dates)
        {
            if (dates == null || dates.Count == 0)
            {
                _logger.LogWarning("Function 'mean_date' called with an empty or null list.");
                return null;
            }
            var userDates = await Task.WhenAll(dates.Select(async d => await ConvertToUserTimeZoneAsync(d)));
            long avgTicks = (long)userDates.Average(d => d.Ticks);
            DateTime meanDate = new DateTime(avgTicks, DateTimeKind.Unspecified);
            _logger.LogInformation($"Function 'mean_date' called with {dates.Count} dates. Returning: {meanDate}");
            return meanDate;
        }

        public async Task<DateTime?> MedianDateAsync(List<DateTime> dates)
        {
            if (dates == null || dates.Count == 0)
            {
                _logger.LogWarning("Function 'median_date' called with an empty or null list.");
                return null;
            }
            var userDates = await Task.WhenAll(dates.Select(async d => await ConvertToUserTimeZoneAsync(d)));
            var sortedDates = userDates.OrderBy(d => d).ToList();
            int count = sortedDates.Count;
            DateTime medianDate;
            if (count % 2 == 1)
            {
                medianDate = sortedDates[count / 2];
            }
            else
            {
                long medianTicks = (sortedDates[(count / 2) - 1].Ticks + sortedDates[count / 2].Ticks) / 2;
                medianDate = new DateTime(medianTicks, DateTimeKind.Unspecified);
            }
            _logger.LogInformation($"Function 'median_date' called with {dates.Count} dates. Returning: {medianDate}");
            return medianDate;
        }

        public Kernel SetupKernelTimePlugin(Kernel kernel)
        {
            kernel.Plugins.AddFromFunctions("time_plugin",
                new[]
                {
                    KernelFunctionFactory.CreateFromMethod(
                    method: new Func<Task<string>>(GetUserTimeZoneAsync),
                    functionName: "get_user_time_zone",
                    description: "Retrieve the user's current time zone ID."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: ConvertBetweenTimeZones,
                    functionName: "convert_between_time_zones",
                    description: "Convert a date and time from one time zone to another."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: StringToTimeSpan,
                    functionName: "string_to_timespan",
                    description: "Convert a time string (format: HH:mm:ss or HH:mm) to a TimeSpan object."
                ),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<Task<DateTime>>(GetCurrentTimeAsync), functionName: "get_current_time", description: "Retrieve the current local date and time based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<Task<DateTime>>(GetTodayAsync), functionName: "today", description: "Retrieve today's date based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<Task<DateTime>>(GetNowAsync), functionName: "now", description: "Retrieve the current date without time component based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, Task<bool>>(IsBusinessDayFunctionAsync), functionName: "is_business_day", description: "Check if a given date is a business day (Monday to Friday) based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<int, int, int, Task<DateTime>>(CreateDateAsync), functionName: "create_date", description: "Create a DateTime object from year, month, and day based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<int, int, int, TimeSpan>(CreateTime), functionName: "create_time", description: "Create a TimeSpan object representing a time of day."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, DateTime, Task<TimeSpan>>(CreateDateIntervalAsync), functionName: "create_date_interval", description: "Create a TimeSpan representing the interval between two dates based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, string, Task<string>>(DateStringAsync), functionName: "date_string", description: "Convert a DateTime object to a string with the specified format based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<string, string, Task<DateTime>>(FromDateStringAsync), functionName: "from_date_string", description: "Convert a date string to a DateTime object using the specified format based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, DateTime, Task<double>>(DateDifferenceDaysAsync), functionName: "date_difference_days", description: "Calculate the difference between two dates in days based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, DateTime, Task<double>>(DateDifferenceHoursAsync), functionName: "date_difference_hours", description: "Calculate the difference between two dates in hours based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, DateTime, Task<List<DateTime>>>(DateRangeAsync), functionName: "date_range", description: "Generate a list of dates between two dates based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, DateTime, Task<double>>(DiffTimeAsync), functionName: "diff_time", description: "Get the difference between two times in seconds based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<string, Task<string>>(LocalTimeAsync), functionName: "local_time", description: "Retrieve the local time for a specified time zone or the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, string, string, Task<string>>(TimezoneConvertAsync), functionName: "timezone_convert", description: "Convert a DateTime from one time zone to another based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<string, string, Task<double>>(TimezoneOffsetAsync), functionName: "timezone_offset", description: "Calculate the offset in hours between two time zones based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<List<DateTime>, Func<DateTime, bool>, Task<List<DateTime>>>(DateSelectAsync), functionName: "date_select", description: "Select dates from a list based on specified criteria and the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<List<DateTime>, Task<DateBounds>>(GetDateBoundsAsync), functionName: "date_bounds", description: "Find the earliest and latest dates from a list based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, CalendarType, Task<string>>(TimesystemConvertAsync), functionName: "timesystem_convert", description: "Convert a DateTime object to a specified calendar system based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, DateTime, DateTime, Task<bool>>(DateWithinQAsync), functionName: "date_within_q", description: "Determine if a date is within a specified date range based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, DateTime, DateTime, DateTime, Task<bool>>(DateOverlapsQAsync), functionName: "date_overlaps_q", description: "Determine if two date ranges overlap based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, Task<bool>>(LeapYearQAsync), functionName: "leap_year_q", description: "Determine if the year of a given date is a leap year based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, DateTime, Task<List<DateTime>>>(DayRangeAsync), functionName: "day_range", description: "Generate a list of days between two dates based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<DateTime, Task<bool>>(BusinessDayQAsync), functionName: "business_day_q", description: "Determine if a given date is a business day (Monday to Friday) based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<List<DateTime>, Task<DateTime?>>(MeanDateAsync), functionName: "mean_date", description: "Calculate the mean (average) date from a list of dates based on the user's time zone."),
                    KernelFunctionFactory.CreateFromMethod(method: new Func<List<DateTime>, Task<DateTime?>>(MedianDateAsync), functionName: "median_date", description: "Calculate the median date from a list of dates based on the user's time zone.")
                });

            _logger.LogInformation("Kernel setup completed successfully.");
            return kernel;
        }
    }
}