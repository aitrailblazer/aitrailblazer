using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public TimeFunctions(
            TimeZoneService timeZoneService,
            ILogger<TimeFunctions> logger)
        {
            _timeZoneService = timeZoneService;
            _logger = logger;
        }

        #region Core Functions

        public string GetUserTimeZone()
        {
            string timeZoneId = _timeZoneService.GetTimeZone();
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

        public DateTime ConvertToUserTimeZone(DateTime dateTime)
        {
            return _timeZoneService.ConvertToUserTimeZone(dateTime);
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

        public DateTime GetCurrentTime()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime userLocalTime = ConvertToUserTimeZone(utcNow);
            _logger.LogInformation($"Function 'get_current_time' called. Returning: {userLocalTime}");
            return userLocalTime;
        }

        public DateTime GetToday()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime userLocalNow = ConvertToUserTimeZone(utcNow);
            DateTime userLocalToday = userLocalNow.Date;
            _logger.LogInformation($"Function 'today' called. UTC Now: {utcNow}, User Local Now: {userLocalNow}, Returning: {userLocalToday}");
            return userLocalToday;
        }

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

        public DateTime CreateDate(int year, int month, int day)
        {
            string userTimeZoneId = _timeZoneService.GetTimeZone();
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

        public TimeSpan CreateDateInterval(DateTime start, DateTime end)
        {
            DateTime userStart = ConvertToUserTimeZone(start);
            DateTime userEnd = ConvertToUserTimeZone(end);
            TimeSpan interval = userEnd - userStart;
            _logger.LogInformation($"Function 'create_date_interval' called with start: {userStart}, end: {userEnd}. Returning interval: {interval}");
            return interval;
        }

        public string DateString(DateTime date, string format)
        {
            DateTime userDate = ConvertToUserTimeZone(date);
            DateTime specifiedDate = DateTime.SpecifyKind(userDate.Date, DateTimeKind.Unspecified);
            string formatted = specifiedDate.ToString(format, CultureInfo.InvariantCulture);
            _logger.LogInformation($"Function 'date_string' called with date: {specifiedDate}, format: '{format}'. Returning: {formatted}");
            return formatted;
        }

        public DateTime FromDateString(string dateString, string format)
        {
            // Parse the date string as an unspecified DateTime (neither local nor UTC)
            DateTime parsedDate = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            // Get the user's time zone
            string userTimeZoneId = _timeZoneService.GetTimeZone();
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

        public string LocalTime(string timeZoneId = null)
        {
            string effectiveTimeZoneId = timeZoneId ?? _timeZoneService.GetTimeZone();

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

        public List<DateTime> DateSelect(List<DateTime> dates, Func<DateTime, bool> criteria)
        {
            var userDates = dates.Select(d => ConvertToUserTimeZone(d)).ToArray();
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

            var userDates = dates.Select(d => ConvertToUserTimeZone(d)).ToArray();
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
                default:
                    culture.DateTimeFormat.Calendar = new GregorianCalendar();
                    break;
            }
            string convertedDate = userDateTime.ToString("D", culture);
            _logger.LogInformation($"Function 'timesystem_convert' called with dateTime: {userDateTime}, calendarType: {calendarType}. Returning: {convertedDate}");
            return convertedDate;
        }

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

        public bool BusinessDayQ(DateTime date)
        {
            DateTime userDate = ConvertToUserTimeZone(date);
            bool isBusinessDay = userDate.DayOfWeek != DayOfWeek.Saturday && userDate.DayOfWeek != DayOfWeek.Sunday;
            _logger.LogInformation($"Function 'business_day_q' called with date: {userDate}. Returning: {isBusinessDay}");
            return isBusinessDay;
        }

        public DateTime? MeanDate(List<DateTime> dates)
        {
            if (dates == null || dates.Count == 0)
            {
                _logger.LogWarning("Function 'mean_date' called with an empty or null list.");
                return null;
            }
            var userDates = dates.Select(d => ConvertToUserTimeZone(d)).ToArray();
            double avgTicks = userDates.Average(d => d.Ticks);
            DateTime meanDate = new DateTime((long)avgTicks, DateTimeKind.Unspecified);
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
            var userDates = dates.Select(d => ConvertToUserTimeZone(d)).OrderBy(d => d).ToList();
            int count = userDates.Count;
            DateTime medianDate;
            if (count % 2 == 1)
            {
                medianDate = userDates[count / 2];
            }
            else
            {
                long medianTicks = (userDates[(count / 2) - 1].Ticks + userDates[count / 2].Ticks) / 2;
                medianDate = new DateTime(medianTicks, DateTimeKind.Unspecified);
            }
            _logger.LogInformation($"Function 'median_date' called with {dates.Count} dates. Returning: {medianDate}");
            return medianDate;
        }

        #endregion

        #region AI-Exposed Wrapper Functions

        /// <summary>
        /// Wrapper function to expose GetCurrentTime for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <returns>Formatted current local time string with StopSequence.</returns>
        public string GetCurrentTimeForAI()
        {
            DateTime currentTime = GetCurrentTime();
            string formattedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
            string aiReadyOutput = $"The current local time is {formattedTime}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'GetCurrentTimeForAI' called. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose GetToday for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <returns>Formatted today's date string with StopSequence.</returns>
        public string GetTodayForAI()
        {
            DateTime today = GetToday();
            string formattedToday = today.ToString("yyyy-MM-dd");
            string aiReadyOutput = $"Today's date is {formattedToday}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'GetTodayForAI' called. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose GetNow for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <returns>Formatted current date string with StopSequence.</returns>
        ///public string GetNowForAI()
        //{
        //    DateTime now = GetNow();
        //    string formattedNow = now.ToString("yyyy-MM-dd");
        //    string aiReadyOutput = $"The current date is {formattedNow}.\n\n"; // "\n\n" as StopSequence
        //    _logger.LogInformation($"Function 'GetNowForAI' called. Returning: {aiReadyOutput}");
        //    return aiReadyOutput;
        //}

        /// <summary>
        /// Wrapper function to expose IsBusinessDayFunction for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="date">Date to check.</param>
        /// <returns>Formatted business day status string with StopSequence.</returns>
        public string IsBusinessDayForAI(DateTime date)
        {
            bool isBusinessDay = IsBusinessDayFunction(date);
            string formattedStatus = isBusinessDay ? "Yes" : "No";
            string aiReadyOutput = $"Is the date {date:yyyy-MM-dd} a business day? {formattedStatus}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'IsBusinessDayForAI' called. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose GetDayOfWeek for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <returns>Formatted day of the week string with StopSequence.</returns>
        public string GetDayOfWeekForAI()
        {
            DayOfWeek day = DateTime.Now.DayOfWeek;
            string aiReadyOutput = $"Today is {day}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'GetDayOfWeekForAI' called. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose ConvertBetweenTimeZones for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="year">Year component.</param>
        /// <param name="month">Month component.</param>
        /// <param name="day">Day component.</param>
        /// <param name="hour">Hour component.</param>
        /// <param name="minute">Minute component.</param>
        /// <param name="second">Second component.</param>
        /// <param name="fromTimeZone">Source time zone ID.</param>
        /// <param name="toTimeZone">Target time zone ID.</param>
        /// <returns>Formatted conversion result string with StopSequence.</returns>
        public string ConvertBetweenTimeZonesForAI(int year, int month, int day, int hour, int minute, int second, string fromTimeZone, string toTimeZone)
        {
            string conversionResult = ConvertBetweenTimeZones(year, month, day, hour, minute, second, fromTimeZone, toTimeZone);
            string aiReadyOutput = $"Time conversion result:\n{conversionResult}\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'ConvertBetweenTimeZonesForAI' called. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        // ======== Newly Added AI-Exposed Wrapper Functions Start Here ========

        /// <summary>
        /// Wrapper function to expose CreateDate for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="year">Year component.</param>
        /// <param name="month">Month component.</param>
        /// <param name="day">Day component.</param>
        /// <returns>Formatted created date string with StopSequence.</returns>
        public string CreateDateForAI(int year, int month, int day)
        {
            DateTime createdDate = CreateDate(year, month, day);
            string formattedDate = createdDate.ToString("yyyy-MM-dd");
            string aiReadyOutput = $"Created date: {formattedDate}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'CreateDateForAI' called with year: {year}, month: {month}, day: {day}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose CreateTime for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="hour">Hour component.</param>
        /// <param name="minute">Minute component.</param>
        /// <param name="second">Second component.</param>
        /// <returns>Formatted created time string with StopSequence.</returns>
        public string CreateTimeForAI(int hour, int minute, int second)
        {
            TimeSpan createdTime = CreateTime(hour, minute, second);
            string formattedTime = createdTime.ToString(@"hh\:mm\:ss");
            string aiReadyOutput = $"Created time: {formattedTime}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'CreateTimeForAI' called with hour: {hour}, minute: {minute}, second: {second}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose CreateDateInterval for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="start">Start date-time.</param>
        /// <param name="end">End date-time.</param>
        /// <returns>Formatted date interval string with StopSequence.</returns>
        public string CreateDateIntervalForAI(DateTime start, DateTime end)
        {
            TimeSpan interval = CreateDateInterval(start, end);
            string formattedInterval = $"{interval.Days} days, {interval.Hours} hours, {interval.Minutes} minutes, {interval.Seconds} seconds";
            string aiReadyOutput = $"Date interval: {formattedInterval}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'CreateDateIntervalForAI' called with start: {start}, end: {end}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DateString for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="date">Date to format.</param>
        /// <param name="format">Desired format string.</param>
        /// <returns>Formatted date string with StopSequence.</returns>
        public string DateStringForAI(DateTime date, string format)
        {
            string formattedDate = DateString(date, format);
            string aiReadyOutput = $"Formatted date: {formattedDate}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'DateStringForAI' called with date: {date}, format: '{format}'. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose FromDateString for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="dateString">Date string to parse.</param>
        /// <param name="format">Format of the date string.</param>
        /// <returns>Formatted parsed date string with StopSequence.</returns>
        public string FromDateStringForAI(string dateString, string format)
        {
            DateTime parsedDate = FromDateString(dateString, format);
            string formattedParsedDate = parsedDate.ToString("yyyy-MM-dd HH:mm:ss");
            string aiReadyOutput = $"Parsed date: {formattedParsedDate}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'FromDateStringForAI' called with dateString: '{dateString}', format: '{format}'. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DateDifferenceDays for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="start">Start date-time.</param>
        /// <param name="end">End date-time.</param>
        /// <returns>Formatted difference in days with StopSequence.</returns>
        public string DateDifferenceDaysForAI(DateTime start, DateTime end)
        {
            double daysDifference = DateDifferenceDays(start, end);
            string aiReadyOutput = $"The difference between the two dates is {daysDifference} days.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'DateDifferenceDaysForAI' called with start: {start}, end: {end}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DateDifferenceHours for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="start">Start date-time.</param>
        /// <param name="end">End date-time.</param>
        /// <returns>Formatted difference in hours with StopSequence.</returns>
        public string DateDifferenceHoursForAI(DateTime start, DateTime end)
        {
            double hoursDifference = DateDifferenceHours(start, end);
            string aiReadyOutput = $"The difference between the two dates is {hoursDifference} hours.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'DateDifferenceHoursForAI' called with start: {start}, end: {end}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DateRange for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="start">Start date-time.</param>
        /// <param name="end">End date-time.</param>
        /// <returns>Formatted date range list with StopSequence.</returns>
        public string DateRangeForAI(DateTime start, DateTime end)
        {
            List<DateTime> range = DateRange(start, end);
            string rangeList = string.Join(", ", range.Select(d => d.ToString("yyyy-MM-dd")));
            string aiReadyOutput = $"Date range: {rangeList}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'DateRangeForAI' called with start: {start}, end: {end}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DiffTime for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="start">Start date-time.</param>
        /// <param name="end">End date-time.</param>
        /// <returns>Formatted difference in seconds with StopSequence.</returns>
        public string DiffTimeForAI(DateTime start, DateTime end)
        {
            double secondsDifference = DiffTime(start, end);
            string aiReadyOutput = $"The difference between the two times is {secondsDifference} seconds.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'DiffTimeForAI' called with start: {start}, end: {end}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose LocalTime for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="timeZoneId">Optional time zone ID.</param>
        /// <returns>Formatted local time string with StopSequence.</returns>
        public string LocalTimeForAI(string timeZoneId = null)
        {
            string localTime = LocalTime(timeZoneId);
            string aiReadyOutput = $"Local time information:\n{localTime}\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'LocalTimeForAI' called with timeZoneId: '{timeZoneId}'. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose TimezoneConvert for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="dateTime">Date-time to convert.</param>
        /// <param name="fromTimeZoneId">Source time zone ID.</param>
        /// <param name="toTimeZoneId">Target time zone ID.</param>
        /// <returns>Formatted converted time string with StopSequence.</returns>
        public string TimezoneConvertForAI(DateTime dateTime, string fromTimeZoneId, string toTimeZoneId)
        {
            string convertedTime = TimezoneConvert(dateTime, fromTimeZoneId, toTimeZoneId);
            string aiReadyOutput = $"Converted time: {convertedTime}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'TimezoneConvertForAI' called with dateTime: {dateTime}, fromTimeZoneId: '{fromTimeZoneId}', toTimeZoneId: '{toTimeZoneId}'. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose TimezoneOffset for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="timeZoneId1">First time zone ID.</param>
        /// <param name="timeZoneId2">Second time zone ID.</param>
        /// <returns>Formatted timezone offset string with StopSequence.</returns>
        public string TimezoneOffsetForAI(string timeZoneId1, string timeZoneId2)
        {
            double offset = TimezoneOffset(timeZoneId1, timeZoneId2);
            string aiReadyOutput = $"The time zone offset between {timeZoneId1} and {timeZoneId2} is {offset} hours.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'TimezoneOffsetForAI' called with timeZoneId1: '{timeZoneId1}', timeZoneId2: '{timeZoneId2}'. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose GetDateBounds for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="dates">List of dates.</param>
        /// <returns>Formatted date bounds string with StopSequence.</returns>
        public string GetDateBoundsForAI(List<DateTime> dates)
        {
            try
            {
                DateBounds bounds = GetDateBounds(dates);
                string aiReadyOutput = $"Date bounds:\nFirst Date: {bounds.FirstDate:yyyy-MM-dd}\nLast Date: {bounds.LastDate:yyyy-MM-dd}\n\n"; // "\n\n" as StopSequence
                _logger.LogInformation($"Function 'GetDateBoundsForAI' called. Returning: {aiReadyOutput}");
                return aiReadyOutput;
            }
            catch (ArgumentException ex)
            {
                string aiReadyError = $"Error: {ex.Message}\n\n"; // "\n\n" as StopSequence
                _logger.LogError($"Function 'GetDateBoundsForAI' error: {ex.Message}");
                return aiReadyError;
            }
        }

        /// <summary>
        /// Wrapper function to expose TimesystemConvert for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="dateTime">Date-time to convert.</param>
        /// <param name="calendarType">Target calendar type.</param>
        /// <returns>Formatted converted timesystem string with StopSequence.</returns>
        public string TimesystemConvertForAI(DateTime dateTime, CalendarType calendarType)
        {
            string convertedDate = TimesystemConvert(dateTime, calendarType);
            string aiReadyOutput = $"Converted date ({calendarType} calendar): {convertedDate}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'TimesystemConvertForAI' called with dateTime: {dateTime}, calendarType: {calendarType}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DateWithinQ for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="inner">Inner date-time.</param>
        /// <param name="outerStart">Outer start date-time.</param>
        /// <param name="outerEnd">Outer end date-time.</param>
        /// <returns>Formatted within query result with StopSequence.</returns>
        public string DateWithinQForAI(DateTime inner, DateTime outerStart, DateTime outerEnd)
        {
            bool isWithin = DateWithinQ(inner, outerStart, outerEnd);
            string status = isWithin ? "is within" : "is not within";
            string aiReadyOutput = $"The date {inner:yyyy-MM-dd} {status} the range {outerStart:yyyy-MM-dd} to {outerEnd:yyyy-MM-dd}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'DateWithinQForAI' called with inner: {inner}, outerStart: {outerStart}, outerEnd: {outerEnd}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DateOverlapsQ for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="start1">First start date-time.</param>
        /// <param name="end1">First end date-time.</param>
        /// <param name="start2">Second start date-time.</param>
        /// <param name="end2">Second end date-time.</param>
        /// <returns>Formatted overlap query result with StopSequence.</returns>
        public string DateOverlapsQForAI(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            bool overlaps = DateOverlapsQ(start1, end1, start2, end2);
            string status = overlaps ? "overlap" : "do not overlap";
            string aiReadyOutput = $"The date ranges {start1:yyyy-MM-dd} to {end1:yyyy-MM-dd} and {start2:yyyy-MM-dd} to {end2:yyyy-MM-dd} {status}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'DateOverlapsQForAI' called with start1: {start1}, end1: {end1}, start2: {start2}, end2: {end2}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose LeapYearQ for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="date">Date to check for leap year.</param>
        /// <returns>Formatted leap year status with StopSequence.</returns>
        public string LeapYearQForAI(DateTime date)
        {
            bool isLeap = LeapYearQ(date);
            string status = isLeap ? "is a leap year" : "is not a leap year";
            string aiReadyOutput = $"The year {date.Year} {status}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'LeapYearQForAI' called with date: {date}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DayRange for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="start">Start date-time.</param>
        /// <param name="end">End date-time.</param>
        /// <returns>Formatted day range list with StopSequence.</returns>
        public string DayRangeForAI(DateTime start, DateTime end)
        {
            List<DateTime> dayRange = DayRange(start, end);
            string dayList = string.Join(", ", dayRange.Select(d => d.ToString("yyyy-MM-dd")));
            string aiReadyOutput = $"Day range: {dayList}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'DayRangeForAI' called with start: {start}, end: {end}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose BusinessDayQ for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="date">Date to check.</param>
        /// <returns>Formatted business day status with StopSequence.</returns>
        public string BusinessDayQForAI(DateTime date)
        {
            bool isBusinessDay = BusinessDayQ(date);
            string status = isBusinessDay ? "is a business day" : "is not a business day";
            string aiReadyOutput = $"The date {date:yyyy-MM-dd} {status}.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'BusinessDayQForAI' called with date: {date}. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose MeanDate for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="dates">List of dates to calculate the mean.</param>
        /// <returns>Formatted mean date string with StopSequence.</returns>
        public string MeanDateForAI(List<DateTime> dates)
        {
            DateTime? meanDate = MeanDate(dates);
            string aiReadyOutput = meanDate.HasValue 
                ? $"The mean date is {meanDate.Value:yyyy-MM-dd}.\n\n" 
                : $"Error: Unable to calculate mean date. The list is empty or null.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'MeanDateForAI' called with {dates?.Count ?? 0} dates. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose MedianDate for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="dates">List of dates to calculate the median.</param>
        /// <returns>Formatted median date string with StopSequence.</returns>
        public string MedianDateForAI(List<DateTime> dates)
        {
            DateTime? medianDate = MedianDate(dates);
            string aiReadyOutput = medianDate.HasValue 
                ? $"The median date is {medianDate.Value:yyyy-MM-dd}.\n\n" 
                : $"Error: Unable to calculate median date. The list is empty or null.\n\n"; // "\n\n" as StopSequence
            _logger.LogInformation($"Function 'MedianDateForAI' called with {dates?.Count ?? 0} dates. Returning: {aiReadyOutput}");
            return aiReadyOutput;
        }

        /// <summary>
        /// Wrapper function to expose DateSelect for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="dates">List of dates to select from.</param>
        /// <param name="criteria">Criteria function as a string (e.g., "IsWeekend").</param>
        /// <returns>Formatted selected dates list with StopSequence.</returns>
        public string DateSelectForAI(List<DateTime> dates, string criteria)
        {
            // For AI interactions, defining criteria as predefined options
            Func<DateTime, bool> criteriaFunc = criteria.ToLower() switch
            {
                "is_weekend" => (DateTime d) => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday,
                "is_weekday" => (DateTime d) => d.DayOfWeek != DayOfWeek.Saturday && d.DayOfWeek != DayOfWeek.Sunday,
                "is_leap_year" => (DateTime d) => DateTime.IsLeapYear(d.Year),
                _ => throw new ArgumentException($"Unknown criteria: {criteria}")
            };

            try
            {
                List<DateTime> selectedDates = DateSelect(dates, criteriaFunc);
                string selectedDateList = string.Join(", ", selectedDates.Select(d => d.ToString("yyyy-MM-dd")));
                string aiReadyOutput = $"Selected dates based on '{criteria}': {selectedDateList}.\n\n"; // "\n\n" as StopSequence
                _logger.LogInformation($"Function 'DateSelectForAI' called with criteria: '{criteria}'. Returning: {aiReadyOutput}");
                return aiReadyOutput;
            }
            catch (ArgumentException ex)
            {
                string aiReadyError = $"Error: {ex.Message}\n\n"; // "\n\n" as StopSequence
                _logger.LogError($"Function 'DateSelectForAI' error: {ex.Message}");
                return aiReadyError;
            }
        }

        // ======== Newly Added AI-Exposed Wrapper Functions End Here ========

        #endregion

     }
}
