using System.Text.Json.Serialization;
using Microsoft.TypeChat.Schema;

namespace AITrailblazer.net.Models;

/// <summary>
/// Represents a structured response for various date and time operations.
/// </summary>
[Comment("Defines the structure of DateTimeOperationsResponse for handling various date and time-related queries, with a list of possible actions.")]
public class DateTimeOperationsResponseStructured
{
    public DateTimeOperationsResponseStructured()
    {
        Name = "DateTimeOperationsResponseStructured"; // Hardcoded name for recognition
    }

    [JsonPropertyName("name")]
    [Comment("The name of the structured response for identifying the type of operation.")]
    public string Name { get; }

    [JsonPropertyName("actions")]
    [Comment("A list of date and time actions performed in response to a user's query.")]
    public DateTimeAction[] Actions { get; set; }

    [JsonPropertyName("response")]
    [Comment("The response to the user's query.")]
    public string Response { get; set; }

}

/// <summary>
/// Base class for various date and time actions.
/// </summary>
[JsonPolymorphic]
[JsonDerivedType(typeof(GetDateWithOffsetAction), typeDiscriminator: nameof(GetDateWithOffsetAction))]
[JsonDerivedType(typeof(GetDateWithWeekOffsetAction), typeDiscriminator: nameof(GetDateWithWeekOffsetAction))]
[JsonDerivedType(typeof(GetDateWithMonthOffsetAction), typeDiscriminator: nameof(GetDateWithMonthOffsetAction))]
[JsonDerivedType(typeof(GetTomorrowAction), typeDiscriminator: nameof(GetTomorrowAction))]
[JsonDerivedType(typeof(GetYesterdayAction), typeDiscriminator: nameof(GetYesterdayAction))]
[JsonDerivedType(typeof(DateDifferenceWeeksAction), typeDiscriminator: nameof(DateDifferenceWeeksAction))]
[JsonDerivedType(typeof(DateDifferenceMonthsAction), typeDiscriminator: nameof(DateDifferenceMonthsAction))]
[JsonDerivedType(typeof(DateDifferenceYearsAction), typeDiscriminator: nameof(DateDifferenceYearsAction))]
[JsonDerivedType(typeof(ParseRelativeDateAction), typeDiscriminator: nameof(ParseRelativeDateAction))]
[JsonDerivedType(typeof(GetNextWeekdayAction), typeDiscriminator: nameof(GetNextWeekdayAction))]
[JsonDerivedType(typeof(ConvertTimezoneAction), typeDiscriminator: nameof(ConvertTimezoneAction))]
public abstract class DateTimeAction { }

/// <summary>
/// Action to get the date with a specified number of days offset.
/// </summary>
public class GetDateWithOffsetAction : DateTimeAction
{
    [JsonPropertyName("daysOffset")]
    [Comment("The number of days offset from today.")]
    public int DaysOffset { get; set; }

    [JsonPropertyName("resultDate")]
    [Comment("The resulting date after applying the days offset.")]
    public string ResultDate { get; set; }
}

/// <summary>
/// Action to get the date with a specified number of weeks offset.
/// </summary>
public class GetDateWithWeekOffsetAction : DateTimeAction
{
    [JsonPropertyName("weeksOffset")]
    [Comment("The number of weeks offset from today.")]
    public int WeeksOffset { get; set; }

    [JsonPropertyName("resultDate")]
    [Comment("The resulting date after applying the weeks offset.")]
    public string ResultDate { get; set; }
}

/// <summary>
/// Action to get the date with a specified number of months offset.
/// </summary>
public class GetDateWithMonthOffsetAction : DateTimeAction
{
    [JsonPropertyName("monthsOffset")]
    [Comment("The number of months offset from today.")]
    public int MonthsOffset { get; set; }

    [JsonPropertyName("resultDate")]
    [Comment("The resulting date after applying the months offset.")]
    public string ResultDate { get; set; }
}

/// <summary>
/// Action to retrieve tomorrow's date.
/// </summary>
public class GetTomorrowAction : DateTimeAction
{
    [JsonPropertyName("resultDate")]
    [Comment("The date for tomorrow.")]
    public string ResultDate { get; set; }
}

/// <summary>
/// Action to retrieve yesterday's date.
/// </summary>
public class GetYesterdayAction : DateTimeAction
{
    [JsonPropertyName("resultDate")]
    [Comment("The date for yesterday.")]
    public string ResultDate { get; set; }
}

/// <summary>
/// Action to calculate the difference in weeks between two dates.
/// </summary>
public class DateDifferenceWeeksAction : DateTimeAction
{
    [JsonPropertyName("startDate")]
    [Comment("The starting date for the week difference calculation.")]
    public string StartDate { get; set; }

    [JsonPropertyName("endDate")]
    [Comment("The ending date for the week difference calculation.")]
    public string EndDate { get; set; }

    [JsonPropertyName("differenceInWeeks")]
    [Comment("The calculated difference in weeks between the two dates.")]
    public double DifferenceInWeeks { get; set; }
}

/// <summary>
/// Action to calculate the difference in months between two dates.
/// </summary>
public class DateDifferenceMonthsAction : DateTimeAction
{
    [JsonPropertyName("startDate")]
    [Comment("The starting date for the month difference calculation.")]
    public string StartDate { get; set; }

    [JsonPropertyName("endDate")]
    [Comment("The ending date for the month difference calculation.")]
    public string EndDate { get; set; }

    [JsonPropertyName("differenceInMonths")]
    [Comment("The calculated difference in months between the two dates.")]
    public int DifferenceInMonths { get; set; }
}

/// <summary>
/// Action to calculate the difference in years between two dates.
/// </summary>
public class DateDifferenceYearsAction : DateTimeAction
{
    [JsonPropertyName("startDate")]
    [Comment("The starting date for the year difference calculation.")]
    public string StartDate { get; set; }

    [JsonPropertyName("endDate")]
    [Comment("The ending date for the year difference calculation.")]
    public string EndDate { get; set; }

    [JsonPropertyName("differenceInYears")]
    [Comment("The calculated difference in years between the two dates.")]
    public int DifferenceInYears { get; set; }
}

/// <summary>
/// Action to parse relative date expressions like "next Friday" or "two weeks from today."
/// </summary>
public class ParseRelativeDateAction : DateTimeAction
{
    [JsonPropertyName("expression")]
    [Comment("The natural language expression of the relative date (e.g., 'next Friday').")]
    public string Expression { get; set; }

    [JsonPropertyName("parsedDate")]
    [Comment("The date parsed from the expression.")]
    public string ParsedDate { get; set; }
}

/// <summary>
/// Action to get the next occurrence of a specified weekday.
/// </summary>
public class GetNextWeekdayAction : DateTimeAction
{
    [JsonPropertyName("weekday")]
    [Comment("The name of the next weekday to find (e.g., 'Monday').")]
    public string Weekday { get; set; }

    [JsonPropertyName("resultDate")]
    [Comment("The resulting date of the next occurrence of the specified weekday.")]
    public string ResultDate { get; set; }
}

/// <summary>
/// Action to convert time between two different time zones.
/// </summary>
public class ConvertTimezoneAction : DateTimeAction
{
    [JsonPropertyName("sourceTime")]
    [Comment("The time in the source timezone.")]
    public string SourceTime { get; set; }

    [JsonPropertyName("sourceTimezone")]
    [Comment("The name of the source timezone.")]
    public string SourceTimezone { get; set; }

    [JsonPropertyName("targetTimezone")]
    [Comment("The name of the target timezone.")]
    public string TargetTimezone { get; set; }

    [JsonPropertyName("convertedTime")]
    [Comment("The converted time in the target timezone.")]
    public string ConvertedTime { get; set; }
}
