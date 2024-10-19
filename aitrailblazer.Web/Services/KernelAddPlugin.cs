using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using aitrailblazer.net.Services;
using System;
using System.Collections.Generic;
using AITrailBlazer.Web.Services;

public class KernelAddPLugin
{
    private readonly TimeFunctions _timeFunctions;
    private readonly KQLFunctions _kqlFunctions;
    //private readonly SearchUrlPlugin _searchUrlPlugin;
    private readonly NewsFunctions _newsFunctions;
    private readonly AITGraphService _graphService;
    private readonly ILogger<KernelAddPLugin> _logger;

    public KernelAddPLugin(TimeFunctions timeFunctions, NewsFunctions newsFunctions, KQLFunctions kqlFunctions, AITGraphService graphService,ILogger<KernelAddPLugin> logger)
    {
        _timeFunctions = timeFunctions;
        _kqlFunctions = kqlFunctions;
        //_searchUrlPlugin = searchUrlPlugin;
        _newsFunctions = newsFunctions;
        _graphService = graphService;
        _logger = logger;
    }

    public Kernel KernelTimePlugin(Kernel kernel)
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
                    //KernelFunctionFactory.CreateFromMethod(
                    //    method: new Func<string>(_timeFunctions.GetNowForAI),
                    //    functionName: "now_ai",
                    //    description: "Retrieve the current date without time component formatted for AI interactions."
                    //),
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
/// <summary>
    /// Sets up the KQL plugin by registering all available KQL functions.
    /// </summary>
    /// <param name="kernel">The Kernel instance to which the plugin will be added.</param>
    /// <returns>The Kernel instance with the KQL plugin registered.</returns>
    public Kernel KQLPlugin(Kernel kernel)
    {
        kernel.Plugins.AddFromFunctions("kql_plugin",
            new[]
            {
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateBasicKQLQuery),
                    functionName: "generate_basic_kql",
                    description: "Generate a basic KQL query from a keyword."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, string>(_kqlFunctions.GenerateFieldSpecificKQLQuery),
                    functionName: "generate_field_specific_kql",
                    description: "Generate a KQL query for a specific field and value."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateAttachmentKQLQuery),
                    functionName: "generate_attachment_kql",
                    description: "Generate a KQL query for email attachments."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateBccKQLQuery),
                    functionName: "generate_bcc_kql",
                    description: "Generate a KQL query for BCC recipients."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateBodyKQLQuery),
                    functionName: "generate_body_kql",
                    description: "Generate a KQL query for email body content."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateCcKQLQuery),
                    functionName: "generate_cc_kql",
                    description: "Generate a KQL query for CC recipients."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateFromKQLQuery),
                    functionName: "generate_from_kql",
                    description: "Generate a KQL query for email sender."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<bool, string>(_kqlFunctions.GenerateHasAttachmentKQLQuery),
                    functionName: "generate_has_attachment_kql",
                    description: "Generate a KQL query to check for attachments."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateImportanceKQLQuery),
                    functionName: "generate_importance_kql",
                    description: "Generate a KQL query for email importance."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateKindKQLQuery),
                    functionName: "generate_kind_kql",
                    description: "Generate a KQL query for message kind."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateParticipantsKQLQuery),
                    functionName: "generate_participants_kql",
                    description: "Generate a KQL query for email participants."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, string>(_kqlFunctions.GenerateReceivedKQLQuery),
                    functionName: "generate_received_kql",
                    description: "Generate a KQL query for received date."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateRecipientsKQLQuery),
                    functionName: "generate_recipients_kql",
                    description: "Generate a KQL query for email recipients."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<DateTime, string>(_kqlFunctions.GenerateSentKQLQuery),
                    functionName: "generate_sent_kql",
                    description: "Generate a KQL query for sent date."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<int, int, string>(_kqlFunctions.GenerateSizeKQLQuery),
                    functionName: "generate_size_kql",
                    description: "Generate a KQL query for email size range."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateSubjectKQLQuery),
                    functionName: "generate_subject_kql",
                    description: "Generate a KQL query for email subject."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateToKQLQuery),
                    functionName: "generate_to_kql",
                    description: "Generate a KQL query for email recipients in the 'To' field."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, string, string>(_kqlFunctions.CombineKQLQueries),
                    functionName: "combine_kql_queries",
                    description: "Combine two KQL queries with a specified operator (AND/OR)."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateKQLQueryFromNaturalLanguage),
                    functionName: "generate_kql_from_natural_language",
                    description: "Generate a KQL query from a natural language description."
                ),
                // New KQL Functions Added Below
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, int, bool, string>(_kqlFunctions.GenerateProximityKQLQuery),
                    functionName: "generate_proximity_kql",
                    description: "Generate a KQL query using proximity operators (NEAR/ONEAR). Parameters: term1, term2, distance, preserveOrder."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, string, string>(_kqlFunctions.GenerateRangeKQLQuery),
                    functionName: "generate_range_kql",
                    description: "Generate a KQL query for range-based searches. Parameters: property, start, end."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<IEnumerable<string>, string, string>(_kqlFunctions.GroupKQLQueries),
                    functionName: "group_kql_queries",
                    description: "Group multiple KQL queries using a specified Boolean operator (AND/OR). Parameters: queries, groupingOperator."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, string>(_kqlFunctions.GenerateSynonymKQLQuery),
                    functionName: "generate_synonym_kql",
                    description: "Generate a KQL query using the WORDS operator for synonyms. Parameters: term1, term2."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string>(_kqlFunctions.GenerateWildcardKQLQuery),
                    functionName: "generate_wildcard_kql",
                    description: "Generate a KQL query using the wildcard operator for prefix matching. Parameter: prefix."
                ),
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, string, float, float, float, float, float, float, int, string>(_kqlFunctions.GenerateXRANKKQLQuery),
                    functionName: "generate_xrank_kql",
                    description: "Generate a KQL query using the XRANK operator to boost dynamic ranking. Parameters: matchExpression, rankExpression, cb, nb, rb, pb, avgb, stdb, n."
                )
            });

        _logger.LogInformation("KQL plugin setup completed successfully.");
        
        return kernel;
    }

    /// <summary>
    /// Sets up the News plugin by registering all available news-related functions.
    /// </summary>
    /// <param name="kernel">The Kernel instance to which the plugin will be added.</param>
    /// <returns>The Kernel instance with the News plugin registered.</returns>
    public Kernel NewsPlugin(Kernel kernel)
    {
        kernel.Plugins.AddFromFunctions("news_plugin",
            new[]
            {
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, Task<string>>(_newsFunctions.SearchNewsAsyncForAI),
                    functionName: "search_news_async_ai",
                    description: "Performs a search for news articles based on a specified query using Bing News Search. Returns the results in a structured format optimized for AI interactions."
                ),

                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<string, Task<string>>(_newsFunctions.GetTrendingTopicsAsyncForAI),
                    functionName: "get_trending_topics_ai",
                    description: "Retrieves a list of trending news topics from Bing News, highlighting popular stories across various categories. Returns results formatted for seamless AI interactions."
                ),

                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<Task<string>>(_newsFunctions.GetHeadlineNewsAsyncForAI),
                    functionName: "get_headline_news_ai",
                    description: "Fetches today's top news articles across all major categories from Bing News. Provides results in a structured format ideal for AI-driven responses."
                )

            });

        _logger.LogInformation("News plugin setup completed successfully.");
        return kernel;
    }

    /// <summary>
    /// Sets up the Email plugin by registering all available email-related functions.
    /// </summary>
    /// <param name="kernel">The Kernel instance to which the plugin will be added.</param>
    /// <param name="graphService">The AITGraphService instance to fetch email messages.</param>
    /// <returns>The Kernel instance with the Email plugin registered.</returns>
    public Kernel AddEmailPlugin(Kernel kernel)
    {
        kernel.Plugins.AddFromFunctions("email_plugin",
            new[]
            {
                KernelFunctionFactory.CreateFromMethod(
                    method: new Func<int, Task<string>>(_graphService.GetRecentMessagesAsync), 
                    functionName: "get_recent_messages_async_ai",
                    description: "Retrieves recent email messages for the authenticated user."
                )
            });

        _logger.LogInformation("Email plugin setup completed successfully.");
        return kernel;
    }

}
