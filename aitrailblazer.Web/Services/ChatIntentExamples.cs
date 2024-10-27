using System.Collections.Generic;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Data;

/// <summary>
/// Provides a collection of few-shot examples for intent recognition.
/// </summary>
public static class ChatIntentExamples
{
    /// <summary>
    /// A list of chat histories representing few-shot examples.
    /// </summary>
    public static List<ChatHistory> Examples { get; } = new List<ChatHistory>
    {
        // -----------------------------
        // Conversation Intents
        // -----------------------------

        /// <summary>
        /// Example for ContinueConversation intent.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Can you send a very quick approval to the marketing team?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "ContinueConversation")
        },

        /// <summary>
        /// Example for EndConversation intent.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Thanks, I'm done for now"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "EndConversation")
        },

        // -----------------------------
        // TimePlugin Intents
        // -----------------------------

        /// <summary>
        /// Example 1: Future Date Query with Day Offset.
        /// Covers future date offset in days.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What will the date be in 10 days?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 2: Past Date Query with Week Offset.
        /// Covers past date offset in weeks.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What was the date 5 weeks ago?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 3: Current Date and Relative Weekday Query.
        /// Covers current date and relative weekday.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What is today's date and time?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "When is next Friday?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 4: Large Day Offset Query.
        /// Covers large day offset.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What will be the date 100 days from now?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 5: Date Differences Query.
        /// Covers calculating differences in months.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "How many months are between April 15, 2022, and October 15, 2022?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 6: Parsing Specific Relative Date.
        /// Covers parsing specific relative dates.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Tell me the date of the second Wednesday this month."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 7: Tomorrow's Date Query.
        /// Covers querying for tomorrow's date.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What is tomorrow's date?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 8: Yesterday's Date Query.
        /// Covers querying for yesterday's date.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What was yesterday's date?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 9: Last Day of the Year Query.
        /// Covers querying for the last day of the current year.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "When is the last day of this year?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 10: Date Calculations with Year Offsets.
        /// Covers year-based offsets.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What will be the date 2 years from today?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 11: Date Calculations with Extensive Day Offsets.
        /// Covers extensive day-based calculations.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Calculate the date 1000 days from now."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 12: Handling Date Range Queries (this week).
        /// Covers queries asking for start and end dates for specific periods like "this week."
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What is the start and end date of this week?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 13: Hour-Based Queries.
        /// Covers time calculations based on hours.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What time will it be in 3 hours?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 14: Minute-Based Queries.
        /// Covers time calculations based on minutes.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What time is it 45 minutes from now?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 15: Second-Based Queries.
        /// Covers time calculations based on seconds.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What time will it be in 30 seconds?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 16: Leap Year Queries.
        /// Covers queries related to leap years.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "When is the next leap year?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 17: Holiday Date Queries.
        /// Covers queries related to specific holidays.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "When is Thanksgiving this year?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 18: Season Start Queries.
        /// Covers queries about the start of seasons.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "When does summer start this year?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 19: Specific Time Zone Queries.
        /// Covers queries about time in different time zones.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What is the current time in New York?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 20: Repeating Date Queries.
        /// Covers queries about recurring dates.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What's the date every Friday for the next month?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        // -----------------------------
        // News-Related Intents
        // -----------------------------

        /// <summary>
        /// Example 1: search_news_async_ai Intent.
        /// Covers searching for specific news topics.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Find the latest news on artificial intelligence."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        /// <summary>
        /// Example 2: search_news_async_ai Intent.
        /// Covers searching for news articles about a different topic.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Can you get news articles about climate change?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        /// <summary>
        /// Example 3: get_trending_topics_ai Intent.
        /// Covers retrieving current trending news topics.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What are the current trending news topics?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        /// <summary>
        /// Example 4: get_trending_topics_ai Intent.
        /// Covers retrieving top trending news for the day.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Show me the top trending news today."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        /// <summary>
        /// Example 5: get_headline_news_ai Intent.
        /// Covers fetching today's headline news.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Give me today's headline news."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        /// <summary>
        /// Example 6: get_headline_news_ai Intent.
        /// Covers fetching main headlines for the day.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What are the main headlines today?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        // -----------------------------
        // Multi-Intent Cases (Separated)
        // -----------------------------

        /// <summary>
        /// Example 1: TimePlugin intent from multi-intent query.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What is tomorrow's date?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 2: NewsPlugin intent from multi-intent query.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Give me today's headline news."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        /// <summary>
        /// Example 3: TimePlugin intent from multi-intent query.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Can you tell me what the date will be next Monday?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 4: NewsPlugin intent from multi-intent query.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Find the latest sports news."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        /// <summary>
        /// Example 5: TimePlugin intent from multi-intent query.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "When is the last day of this month?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin")
        },

        /// <summary>
        /// Example 6: NewsPlugin intent from multi-intent query.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What are today's top headlines?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "NewsPlugin")
        },

        // -----------------------------
        // Calendar Plugin Intents
        // -----------------------------

        /// <summary>
        /// Example 1: AddEventAction intent.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Schedule a meeting with John and Sarah for tomorrow at 3 PM."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin. CalendarPlugin")
        },

        /// <summary>
        /// Example 2: RemoveEventAction intent.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Cancel my meeting with the marketing team on Friday."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin. CalendarPlugin")
        },

        /// <summary>
        /// Example 3: AddParticipantsAction intent.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Add Jane to the project kickoff meeting."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin. CalendarPlugin")
        },

        /// <summary>
        /// Example 4: ChangeTimeRangeAction intent.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Reschedule my meeting with the engineering team to 2 PM tomorrow."),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin. CalendarPlugin")
        },

        /// <summary>
        /// Example 5: ChangeDescriptionAction intent.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "Update the description for tomorrow's meeting to 'Project Kickoff.'"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin. CalendarPlugin")
        },

        /// <summary>
        /// Example 6: FindEventsAction intent.
        /// </summary>
        new ChatHistory
        {
            new ChatMessageContent(AuthorRole.User, "What events do I have scheduled this week?"),
            new ChatMessageContent(AuthorRole.System, "Intent:"),
            new ChatMessageContent(AuthorRole.Assistant, "TimePlugin. CalendarPlugin")
        }
    };
}
