Here’s a fully comprehensive guide covering various date and time operations, handling local time zone conversions, date offsets, and relative date queries. Each section includes example questions, the associated function, and expected responses. This guide ensures all possible cases are covered:

1. Local Time Zone Operations

a. Show me my current time
Query:
Show me my current time
Function Invoked:
display_local_time
Expected Response:
The current time in your local time zone (EST) is October 20, 2024, 3:15 PM.
b. What is my current time zone?
Query:
What is my current time zone?
Function Invoked:
detect_local_timezone
Expected Response:
Your current time zone is Eastern Standard Time (EST).
c. Convert 10:00 AM UTC to my local time
Query:
Convert 10:00 AM UTC to my local time
Function Invoked:
convert_utc_to_local
Expected Response:
10:00 AM UTC is 6:00 AM EST in your local time zone.
d. Schedule a meeting at 3:00 PM in my local time
Query:
Schedule a meeting at 3:00 PM in my local time
Function Invoked:
schedule_event_local
Expected Response:
A meeting has been scheduled at 3:00 PM EST in your local time zone.
e. Adjust for Daylight Saving Time in my time zone
Query:
What time will it be at 2:00 AM on November 3, 2024, in my time zone, considering Daylight Saving Time?
Function Invoked:
adjust_dst_local
Expected Response:
On November 3, 2024, at 2:00 AM, your local time will adjust to 1:00 AM EST due to the end of Daylight Saving Time.
f. What is the UTC offset for my time zone?
Query:
What is the UTC offset for my time zone?
Function Invoked:
local_timezone_offset
Expected Response:
Your local time zone (EST) is currently UTC-5 hours.
g. Show the New Year time in my local time zone
Query:
Show the New Year time in my local time zone
Function Invoked:
display_event_local
Expected Response:
The New Year will occur on December 31, 2024, at 11:59 PM EST in your local time zone.
h. Is my birthday before today in my local time zone?
Query:
Is my birthday on July 4, 2024, before today in my local time zone?
Function Invoked:
compare_local_dates
Expected Response:
Your birthday on July 4, 2024, is before today in your local time zone.
i. List upcoming holidays in my local time zone
Query:
List upcoming holidays in my local time zone
Function Invoked:
list_local_holidays
Expected Response:
`Upcoming holidays in your local time zone (EST):
New Year's Day: January 01, 2024
Independence Day: July 04, 2024
Thanksgiving: November 28, 2024
Christmas Day: December 25, 2024`
j. Convert 9:00 AM my time to PST
Query:
Convert 9:00 AM my time to PST
Function Invoked:
convert_local_to_timezone
Expected Response:
9:00 AM EST is 6:00 AM PST.
2. Date Offset Queries

a. Future Date Queries
Query:
What will the date be in 5 days?
Tell me the date 10 days from now.
What's the date 3 days from today?
Function Invoked:
GetDateWithOffsetForAI
Expected Response:
The date 5 days from today is October 25, 2024.
The date 10 days from now is October 30, 2024.
b. Past Date Queries
Query:
What was the date 7 days ago?
What date was it 15 days ago?
Function Invoked:
GetDateWithOffsetForAI
Expected Response:
The date 7 days ago was October 13, 2024.
The date 15 days ago was October 5, 2024.
c. Zero Offset Query
Query:
What is today's date?
Function Invoked:
GetNowForAI
Expected Response:
Today's date is October 20, 2024.
3. Week Offset Queries

a. Queries for Future Weeks
Query:
What date will it be 2 weeks from now?
What's the date 4 weeks from today?
Function Invoked:
GetDateWithWeekOffsetForAI
Expected Response:
The date 2 weeks from now is November 3, 2024.
The date 4 weeks from today is November 17, 2024.
b. Queries for Past Weeks
Query:
What was the date 5 weeks before today?
Tell me the date 3 weeks ago.
Function Invoked:
GetDateWithWeekOffsetForAI
Expected Response:
The date 5 weeks ago was September 15, 2024.
The date 3 weeks ago was September 29, 2024.
4. Month Offset Queries

a. Queries for Future Months
Query:
What will the date be in 1 month?
Tell me the date 2 months from now.
Function Invoked:
GetDateWithMonthOffsetForAI
Expected Response:
The date 1 month from now is November 20, 2024.
The date 2 months from now is December 20, 2024.
b. Queries for Past Months
Query:
What was the date 6 months ago?
Function Invoked:
GetDateWithMonthOffsetForAI
Expected Response:
The date 6 months ago was April 20, 2024.
5. Tomorrow’s and Yesterday’s Date

a. Tomorrow’s Date Queries
Query:
What is tomorrow's date?
Tell me the date for tomorrow.
Function Invoked:
GetTomorrowForAI
Expected Response:
Tomorrow's date is October 21, 2024.
b. Yesterday’s Date Queries
Query:
What was yesterday's date?
Function Invoked:
GetYesterdayForAI
Expected Response:
Yesterday's date was October 19, 2024.
6. Date Difference Queries

a. Difference in Weeks
Query:
How many weeks are there between today and December 31, 2024?
Function Invoked:
DateDifferenceWeeksForAI
Expected Response:
There are 10 weeks and 4 days between today and December 31, 2024.
b. Difference in Months
Query:
How many months are between April 15, 2022, and October 15, 2022?
Function Invoked:
DateDifferenceMonthsForAI
Expected Response:
There are 6 months between April 15, 2022, and October 15, 2022.
c. Difference in Years
Query:
How many years have passed since 1995?
Function Invoked:
DateDifferenceYearsForAI
Expected Response:
There have been 29 years since 1995.
7. Relative Date Parsing

a. Parsing Next Weekday Queries
Query:
What is the date for next Friday?
Tell me the date for the second Wednesday of this month.
Function Invoked:
ParseRelativeDateForAI
Expected Response:
The date for next Friday is October 27, 2024.
The second Wednesday of this month is October 9, 2024.
8. Complex Date Calculations

a. Example Queries with Week and Year Offsets
Query:
What will be the date two weeks from next Friday?
What date is it 10 years from today?
Function Invoked:
GetDateWithWeekOffsetForAI (for weeks)
GetDateWithOffsetForAI (for years)
Expected Response:
Two weeks from next Friday (October 27, 2024) is November 10, 2024.
The date 10 years from today is October 20, 2034.
These examples ensure that all possible date and time-related queries are covered. If you'd like further customization or examples, feel free to ask!