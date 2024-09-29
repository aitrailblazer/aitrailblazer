using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace aitrailblazer.net.Services
{
    // Define the DateBounds class
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
        // Add more calendar types as needed
    }

    public class TimeFunctions
    {
        public readonly UserTimeZoneService _userTimeZoneService;
        public readonly ILogger<TimeFunctions> _logger;

        public TimeFunctions(UserTimeZoneService userTimeZoneService, ILogger<TimeFunctions> logger)
        {
            _userTimeZoneService = userTimeZoneService;
            _logger = logger;
        }

        public DateTime ConvertToUserTimeZone(DateTime dateTime)
        {
            try
            {
                // Retrieve the user's time zone ID
                string userTimeZoneId = _userTimeZoneService.TimeZoneId;

                // Find the corresponding TimeZoneInfo
                TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);

                // Convert the provided DateTime to the user's time zone
                DateTime userLocalTime = TimeZoneInfo.ConvertTime(dateTime, userTimeZone);

                return userLocalTime;
            }
            catch (TimeZoneNotFoundException)
            {
                _logger.LogError($"Time zone '{_userTimeZoneService.TimeZoneId}' not found. Defaulting to UTC.");
                return dateTime.ToUniversalTime();
            }
            catch (InvalidTimeZoneException)
            {
                _logger.LogError($"Time zone '{_userTimeZoneService.TimeZoneId}' is invalid. Defaulting to UTC.");
                return dateTime.ToUniversalTime();
            }
        }

        #region Kernel Function Methods
        // -------------------------------
        // Current Time and Date Functions
        // -------------------------------
        public DateTime GetCurrentTime()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime userLocalTime = ConvertToUserTimeZone(utcNow);
            _logger.LogInformation($"Function 'get_current_time' called. Returning: {userLocalTime}");
            return userLocalTime;
        }

        public DateTime GetToday()
        {
            DateTime utcToday = DateTime.UtcNow.Date;
            DateTime userLocalToday = ConvertToUserTimeZone(utcToday);
            _logger.LogInformation($"Function 'today' called. Returning: {userLocalToday}");
            return userLocalToday;
        }

        /*
        public DateTime GetTomorrow()
        {
            DateTime utcTomorrow = DateTime.UtcNow.Date.AddDays(1);
            DateTime userLocalTomorrow = ConvertToUserTimeZone(utcTomorrow);
            _logger.LogInformation($"Function 'tomorrow' called. Returning: {userLocalTomorrow}");
            return userLocalTomorrow;
        }

        public DateTime GetYesterday()
        {
            DateTime utcYesterday = DateTime.UtcNow.Date.AddDays(-1);
            DateTime userLocalYesterday = ConvertToUserTimeZone(utcYesterday);
            _logger.LogInformation($"Function 'yesterday' called. Returning: {userLocalYesterday}");
            return userLocalYesterday;
        }
        */

        public DateTime GetNow()
        {
            DateTime utcNowDate = DateTime.UtcNow.Date;
            DateTime userLocalNowDate = ConvertToUserTimeZone(utcNowDate);
            _logger.LogInformation($"Function 'now' called. Returning: {userLocalNowDate}");
            return userLocalNowDate;
        }

        public bool IsBusinessDayFunction(DateTime date)
        {
            DateTime userLocalDate = ConvertToUserTimeZone(date);
            bool isBusiness = userLocalDate.DayOfWeek != DayOfWeek.Saturday && userLocalDate.DayOfWeek != DayOfWeek.Sunday;
            _logger.LogInformation($"Function 'is_business_day' called with date: {userLocalDate}. Returning: {isBusiness}");
            return isBusiness;
        }

        // ---------------------------
        // Date and Time Representation
        // ---------------------------
        public DateTime CreateDate(int year, int month, int day)
        {
            DateTime createdDateUtc = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
            DateTime userLocalDate = ConvertToUserTimeZone(createdDateUtc);
            _logger.LogInformation($"Function 'create_date' called with year: {year}, month: {month}, day: {day}. Returning: {userLocalDate}");
            return userLocalDate;
        }

        public TimeSpan CreateTime(int hour, int minute, int second)
        {
            TimeSpan createdTime = new TimeSpan(hour, minute, second);
            _logger.LogInformation($"Function 'create_time' called with hour: {hour}, minute: {minute}, second: {second}. Returning: {createdTime}");
            return createdTime;
        }

        public TimeSpan CreateDateInterval(DateTime start, DateTime end)
        {
            DateTime userStart = ConvertToUserTimeZone(start);
            DateTime userEnd = ConvertToUserTimeZone(end);
            TimeSpan interval = userEnd - userStart;
            _logger.LogInformation($"Function 'create_date_interval' called with start: {userStart}, end: {userEnd}. Returning interval: {interval}");
            return interval;
        }

        // ------------------------
        // Date and Time Formatting
        // ------------------------
        public string DateString(DateTime date, string format)
        {
            DateTime userDate = ConvertToUserTimeZone(date);
            // Specify the kind as Unspecified to prevent time zone shifts
            DateTime specifiedDate = DateTime.SpecifyKind(userDate.Date, DateTimeKind.Unspecified);
            string formatted = specifiedDate.ToString(format, CultureInfo.InvariantCulture);
            _logger.LogInformation($"Function 'date_string' called with date: {specifiedDate}, format: '{format}'. Returning: {formatted}");
            return formatted;
        }

        public DateTime FromDateString(string dateString, string format)
        {
            DateTime parsedDateUtc = DateTime.SpecifyKind(DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture), DateTimeKind.Utc);
            DateTime userParsedDate = ConvertToUserTimeZone(parsedDateUtc);
            _logger.LogInformation($"Function 'from_date_string' called with dateString: '{dateString}', format: '{format}'. Returning: {userParsedDate}");
            return userParsedDate;
        }

        // -----------------
        // Time Calculations
        // -----------------
        /*
        public DateTime DatePlusDays(DateTime date, int days)
        {
            DateTime userDate = ConvertToUserTimeZone(date);
            DateTime newDate = userDate.AddDays(days);
            _logger.LogInformation($"Function 'date_plus_days' called with date: {userDate}, days: {days}. Returning: {newDate}");
            return newDate;
        }

        public DateTime DatePlusWeeks(DateTime date, int weeks)
        {
            DateTime userDate = ConvertToUserTimeZone(date);
            DateTime newDate = userDate.AddDays(weeks * 7);
            _logger.LogInformation($"Function 'date_plus_weeks' called with date: {userDate}, weeks: {weeks}. Returning: {newDate}");
            return newDate;
        }
        */

        public double DateDifferenceDays(DateTime start, DateTime end)
        {
            DateTime userStart = ConvertToUserTimeZone(start);
            DateTime userEnd = ConvertToUserTimeZone(end);
            double totalDays = (userEnd - userStart).TotalDays;
            _logger.LogInformation($"Function 'date_difference_days' called with start: {userStart}, end: {userEnd}. Returning: {totalDays} days");
            return totalDays;
        }

        public double DateDifferenceHours(DateTime start, DateTime end)
        {
            DateTime userStart = ConvertToUserTimeZone(start);
            DateTime userEnd = ConvertToUserTimeZone(end);
            double totalHours = (userEnd - userStart).TotalHours;
            _logger.LogInformation($"Function 'date_difference_hours' called with start: {userStart}, end: {userEnd}. Returning: {totalHours} hours");
            return totalHours;
        }

        public List<DateTime> DateRange(DateTime start, DateTime end)
        {
            DateTime userStart = ConvertToUserTimeZone(start);
            DateTime userEnd = ConvertToUserTimeZone(end);
            List<DateTime> dateRange = Enumerable.Range(0, (userEnd - userStart).Days + 1)
                .Select(d => userStart.AddDays(d))
                .ToList();
            string dateList = string.Join(", ", dateRange.Select(d => d.ToString("MMMM dd, yyyy")));
            _logger.LogInformation($"Function 'date_range' called with start: {userStart}, end: {userEnd}. Returning: {dateList}");
            return dateRange;
        }

        public double DiffTime(DateTime start, DateTime end)
        {
            DateTime userStart = ConvertToUserTimeZone(start);
            DateTime userEnd = ConvertToUserTimeZone(end);
            double totalSeconds = (userEnd - userStart).TotalSeconds;
            _logger.LogInformation($"Function 'diff_time' called with start: {userStart}, end: {userEnd}. Returning: {totalSeconds} seconds");
            return totalSeconds;
        }

        // -------------------
        // Time Zone Handling
        // -------------------
        public string LocalTime(string timeZoneId = null)
        {
            string effectiveTimeZoneId = timeZoneId ?? _userTimeZoneService.TimeZoneId;

            try
            {
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById(effectiveTimeZoneId);
                DateTime utcNow = DateTime.UtcNow;
                DateTime localTime = TimeZoneInfo.ConvertTime(utcNow, tz);
                string result = localTime.ToString("MMMM dd, yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                _logger.LogInformation($"Function 'local_time' called with timeZoneId: '{effectiveTimeZoneId}'. Returning: {result}");
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

        public string TimezoneConvert(DateTime dateTime, string fromTimeZoneId, string toTimeZoneId)
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

        public double TimezoneOffset(string timeZoneId1, string timeZoneId2)
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

        // ------------------------
        // Date and Time Operations
        // ------------------------
        public List<DateTime> DateSelect(List<DateTime> dates, Func<DateTime, bool> criteria)
        {
            var userDates = dates.Select(d => ConvertToUserTimeZone(d)).ToList();
            var selectedDates = userDates.Where(criteria).ToList();
            string selectedDateList = string.Join(", ", selectedDates.Select(d => d.ToString("MMMM dd, yyyy")));
            _logger.LogInformation($"Function 'date_select' called with dates count: {dates.Count}. Returning: {selectedDateList}");
            return selectedDates;
        }

        public DateBounds GetDateBounds(List<DateTime> dates)
        {
            if (dates == null || dates.Count == 0)
            {
                _logger.LogWarning("Function 'date_bounds' called with an empty or null list.");
                throw new ArgumentException("Date list cannot be null or empty.", nameof(dates));
            }

            var userDates = dates.Select(d => ConvertToUserTimeZone(d)).ToList();
            DateTime firstDate = userDates.Min();
            DateTime lastDate = userDates.Max();
            _logger.LogInformation($"Function 'date_bounds' called. Returning FirstDate: {firstDate}, LastDate: {lastDate}");
            return new DateBounds { FirstDate = firstDate, LastDate = lastDate };
        }

        public string TimesystemConvert(DateTime dateTime, CalendarType calendarType)
        {
            var userDateTime = ConvertToUserTimeZone(dateTime);
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
                // Add more calendar types as needed
                default:
                    culture.DateTimeFormat.Calendar = new GregorianCalendar();
                    break;
            }
            string convertedDate = userDateTime.ToString("D", culture);
            _logger.LogInformation($"Function 'timesystem_convert' called with dateTime: {userDateTime}, calendarType: {calendarType}. Returning: {convertedDate}");
            return convertedDate;
        }

        // -----------------------
        // Date and Time Testing
        // -----------------------
        public bool DateWithinQ(DateTime inner, DateTime outerStart, DateTime outerEnd)
        {
            DateTime userInner = ConvertToUserTimeZone(inner);
            DateTime userOuterStart = ConvertToUserTimeZone(outerStart);
            DateTime userOuterEnd = ConvertToUserTimeZone(outerEnd);
            bool isWithin = userInner >= userOuterStart && userInner <= userOuterEnd;
            _logger.LogInformation($"Function 'date_within_q' called with inner: {userInner}, outerStart: {userOuterStart}, outerEnd: {userOuterEnd}. Returning: {isWithin}");
            return isWithin;
        }

        public bool DateOverlapsQ(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            DateTime userStart1 = ConvertToUserTimeZone(start1);
            DateTime userEnd1 = ConvertToUserTimeZone(end1);
            DateTime userStart2 = ConvertToUserTimeZone(start2);
            DateTime userEnd2 = ConvertToUserTimeZone(end2);
            bool overlaps = userStart1 <= userEnd2 && userStart2 <= userEnd1;
            _logger.LogInformation($"Function 'date_overlaps_q' called with start1: {userStart1}, end1: {userEnd1}, start2: {userStart2}, end2: {userEnd2}. Returning: {overlaps}");
            return overlaps;
        }

        public bool LeapYearQ(DateTime date)
        {
            DateTime userDate = ConvertToUserTimeZone(date);
            bool isLeapYear = DateTime.IsLeapYear(userDate.Year);
            _logger.LogInformation($"Function 'leap_year_q' called with date: {userDate}. Returning: {isLeapYear}");
            return isLeapYear;
        }

        // -------------------------
        // Specialized Day Operations
        // -------------------------
        public List<DateTime> DayRange(DateTime start, DateTime end)
        {
            DateTime userStart = ConvertToUserTimeZone(start);
            DateTime userEnd = ConvertToUserTimeZone(end);
            List<DateTime> dayRange = Enumerable.Range(0, (userEnd - userStart).Days + 1)
                .Select(d => userStart.AddDays(d))
                .ToList();
            string dayList = string.Join(", ", dayRange.Select(d => d.ToString("MMMM dd, yyyy")));
            _logger.LogInformation($"Function 'day_range' called with start: {userStart}, end: {userEnd}. Returning: {dayList}");
            return dayRange;
        }

        /*
        public DateTime DayPlus(DateTime date, int days)
        {
            // Use only the date component (ignores the time)
            DateTime userDate = ConvertToUserTimeZone(date).Date;

            // Add the specified number of days
            DateTime newDate = userDate.AddDays(days);

            // Log the call details
            _logger.LogInformation($"Function 'day_plus' called with date: {userDate.ToShortDateString()}, days: {days}. Returning: {newDate.ToShortDateString()}");

            return newDate;
        }
        */

        public bool BusinessDayQ(DateTime date)
        {
            DateTime userDate = ConvertToUserTimeZone(date);
            bool isBusinessDay = userDate.DayOfWeek != DayOfWeek.Saturday && userDate.DayOfWeek != DayOfWeek.Sunday;
            _logger.LogInformation($"Function 'business_day_q' called with date: {userDate}. Returning: {isBusinessDay}");
            return isBusinessDay;
        }

        // ------------------------------
        // Statistical Operations on Dates and Times
        // ------------------------------
        public DateTime? MeanDate(List<DateTime> dates)
        {
            if (dates == null || dates.Count == 0)
            {
                _logger.LogWarning("Function 'mean_date' called with an empty or null list.");
                return null;
            }
            var userDates = dates.Select(d => ConvertToUserTimeZone(d)).ToList();
            long avgTicks = (long)userDates.Average(d => d.Ticks);
            DateTime meanDate = new DateTime(avgTicks, DateTimeKind.Unspecified);
            _logger.LogInformation($"Function 'mean_date' called with {dates.Count} dates. Returning: {meanDate}");
            return meanDate;
        }

        public DateTime? MedianDate(List<DateTime> dates)
        {
            if (dates == null || dates.Count == 0)
            {
                _logger.LogWarning("Function 'median_date' called with an empty or null list.");
                return null;
            }
            var sortedDates = dates.Select(d => ConvertToUserTimeZone(d)).OrderBy(d => d).ToList();
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

        #endregion

        #region Kernel Setup
        public Kernel SetupKernelTimePlugin(Kernel kernel)
        {
            // Add date and time-related functions to the kernel
            kernel.Plugins.AddFromFunctions("time_plugin",
                new[]
                {
                    // Current Time and Date Functions
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime>(GetCurrentTime),
                        functionName: "get_current_time",
                        description: "Retrieve the current local date and time based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime>(GetToday),
                        functionName: "today",
                        description: "Retrieve today's date based on the user's time zone."
                    ),
                    /*
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime>(GetTomorrow),
                        functionName: "tomorrow",
                        description: "Retrieve tomorrow's date based on the user's time zone."
                    ),

                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime>(GetYesterday),
                        functionName: "yesterday",
                        description: "Retrieve yesterday's date based on the user's time zone."
                    ),
                    */
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime>(GetNow),
                        functionName: "now",
                        description: "Retrieve the current date without time component based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, bool>(IsBusinessDayFunction),
                        functionName: "is_business_day",
                        description: "Check if a given date is a business day (Monday to Friday) based on the user's time zone."
                    ),

                    // Date and Time Representation
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<int, int, int, DateTime>(CreateDate),
                        functionName: "create_date",
                        description: "Create a DateTime object from year, month, and day based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<int, int, int, TimeSpan>(CreateTime),
                        functionName: "create_time",
                        description: "Create a TimeSpan object representing a time of day."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, TimeSpan>(CreateDateInterval),
                        functionName: "create_date_interval",
                        description: "Create a TimeSpan representing the interval between two dates based on the user's time zone."
                    ),

                    // Date and Time Formatting
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, string, string>(DateString),
                        functionName: "date_string",
                        description: "Convert a DateTime object to a string with the specified format based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string, string, DateTime>(FromDateString),
                        functionName: "from_date_string",
                        description: "Convert a date string to a DateTime object using the specified format based on the user's time zone."
                    ),

                    // Time Calculations
                    /*
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, int, DateTime>(DatePlusDays),
                        functionName: "date_plus_days",
                        description: "Add or subtract days from a DateTime object based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, int, DateTime>(DatePlusWeeks),
                        functionName: "date_plus_weeks",
                        description: "Add or subtract weeks from a DateTime object based on the user's time zone."
                    ),
                    */
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, double>(DateDifferenceDays),
                        functionName: "date_difference_days",
                        description: "Calculate the difference between two dates in days based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, double>(DateDifferenceHours),
                        functionName: "date_difference_hours",
                        description: "Calculate the difference between two dates in hours based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, List<DateTime>>(DateRange),
                        functionName: "date_range",
                        description: "Generate a list of dates between two dates based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, double>(DiffTime),
                        functionName: "diff_time",
                        description: "Get the difference between two times in seconds based on the user's time zone."
                    ),

                    // Time Zone Handling
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string, string>(LocalTime),
                        functionName: "local_time",
                        description: "Retrieve the local time for a specified time zone or the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, string, string, string>(TimezoneConvert),
                        functionName: "timezone_convert",
                        description: "Convert a DateTime from one time zone to another based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<string, string, double>(TimezoneOffset),
                        functionName: "timezone_offset",
                        description: "Calculate the offset in hours between two time zones based on the user's time zone."
                    ),

                    // Date and Time Operations
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<List<DateTime>, Func<DateTime, bool>, List<DateTime>>(DateSelect),
                        functionName: "date_select",
                        description: "Select dates from a list based on specified criteria and the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<List<DateTime>, DateBounds>(GetDateBounds),
                        functionName: "date_bounds",
                        description: "Find the earliest and latest dates from a list based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, CalendarType, string>(TimesystemConvert),
                        functionName: "timesystem_convert",
                        description: "Convert a DateTime object to a specified calendar system based on the user's time zone."
                    ),

                    // Date and Time Testing
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, DateTime, bool>(DateWithinQ),
                        functionName: "date_within_q",
                        description: "Determine if a date is within a specified date range based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, DateTime, DateTime, bool>(DateOverlapsQ),
                        functionName: "date_overlaps_q",
                        description: "Determine if two date ranges overlap based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, bool>(LeapYearQ),
                        functionName: "leap_year_q",
                        description: "Determine if the year of a given date is a leap year based on the user's time zone."
                    ),

                    // Specialized Day Operations
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, DateTime, List<DateTime>>(DayRange),
                        functionName: "day_range",
                        description: "Generate a list of days between two dates based on the user's time zone."
                    ),
                    /*
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, int, DateTime>(DayPlus),
                        functionName: "day_plus",
                        description: "Add or subtract days from a given date based on the user's time zone."
                    ),
                    */
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<DateTime, bool>(BusinessDayQ),
                        functionName: "business_day_q",
                        description: "Determine if a given date is a business day (Monday to Friday) based on the user's time zone."
                    ),

                    // Statistical Operations on Dates and Times
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<List<DateTime>, DateTime?>(MeanDate),
                        functionName: "mean_date",
                        description: "Calculate the mean (average) date from a list of dates based on the user's time zone."
                    ),
                    KernelFunctionFactory.CreateFromMethod(
                        method: new Func<List<DateTime>, DateTime?>(MedianDate),
                        functionName: "median_date",
                        description: "Calculate the median date from a list of dates based on the user's time zone."
                    )
                });

            _logger.LogInformation("Kernel setup completed successfully.");
            return kernel;
        }
        #endregion
    }
}
