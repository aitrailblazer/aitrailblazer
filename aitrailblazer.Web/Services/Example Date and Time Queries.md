# Example Date and Time Queries

To effectively utilize the comprehensive set of date and time functions integrated into your kernel, consider the following example queries. These are categorized by functionality, detailing which kernel functions they invoke and the expected responses.

---

## 1. Current Time and Date Retrieval

### a. Get Current Time

**Query:**  
`"What is the current time?"`

**Function Invoked:**  
`get_current_time`

**Expected Response:**  
`"The current local date and time is September 28, 2024 10:30 AM."`

### b. Today, Tomorrow, Yesterday

**Queries:**  
- `"What is today's date?"`  
- `"What is tomorrow's date?"`  
- `"What was yesterday's date?"`

**Functions Invoked:**  
- `today`  
- `tomorrow`  
- `yesterday`

**Expected Responses:**  
- `"Today's date is September 28, 2024."`  
- `"Tomorrow's date is September 29, 2024."`  
- `"Yesterday's date was September 27, 2024."`

### c. Retrieve Current Date Without Time Component

**Query:**  
`"What is the current date?"`

**Function Invoked:**  
`now`

**Expected Response:**  
`"The current date is September 28, 2024."`

---

## 2. Date and Time Representation

### a. Create a Specific Date

**Query:**  
`"Create a date for July 4, 2024."`

**Function Invoked:**  
`create_date`

**Expected Response:**  
`"The created date is July 04, 2024."`

### b. Create a Specific Time

**Query:**  
`"Create a time for 3:45 PM."`

**Function Invoked:**  
`create_time`

**Expected Response:**  
`"The created time is 15:45:00."`

### c. Create a Date Interval

**Query:**  
`"Create an interval between September 28, 2024, and October 5, 2024."`

**Function Invoked:**  
`create_date_interval`

**Expected Response:**  
`"The interval between September 28, 2024, and October 5, 2024, is 7 days."`

---

## 3. Date and Time Formatting

### a. Format a Date
TODO

**Query:**  
`"Format the date July 4, 2024, as 'MM/dd/yyyy'."`

**Function Invoked:**  
`date_string`

**Expected Response:**  
`"The formatted date is 07/04/2024."`

### b. Parse a Date String

**Query:**  
`"Convert the string '07/04/2024' to a date object using the format 'MM/dd/yyyy'."`

**Function Invoked:**  
`from_date_string`

**Expected Response:**  
`"The converted date is July 04, 2024."`

---

## 4. Time Calculations

### a. Add Days to a Date

**Query:**  
`"Add 10 days to September 28, 2024."`

**Function Invoked:**  
`date_plus_days`

**Expected Response:**  
`"Adding 10 days to September 28, 2024, results in October 08, 2024."`

### b. Subtract Weeks from a Date

**Query:**  
`"Subtract 2 weeks from October 15, 2024."`

**Function Invoked:**  
`date_plus_weeks`

**Expected Response:**  
`"Subtracting 2 weeks from October 15, 2024, results in October 01, 2024."`

### c. Calculate Difference in Days

**Query:**  
`"How many days are there between September 28, 2024, and December 25, 2024?"`

**Function Invoked:**  
`date_difference_days`

**Expected Response:**  
`"There are 88 days between September 28, 2024, and December 25, 2024."`

### d. Calculate Difference in Hours

**Query:**  
`"How many hours are there between September 28, 2024, 10:00 AM and September 28, 2024, 6:00 PM?"`

**Function Invoked:**  
`date_difference_hours`

**Expected Response:**  
`"There are 8 hours between September 28, 2024, 10:00 AM and September 28, 2024, 6:00 PM."`

### e. Generate a Date Range

**Query:**  
`"List all dates between September 28, 2024, and October 02, 2024."`

**Function Invoked:**  
`date_range`

**Expected Response:**  
The dates between September 28, 2024, and October 02, 2024, are:

September 28, 2024
September 29, 2024
September 30, 2024
October 01, 2024
October 02, 2024


---

## 5. Time Zone Handling

### a. Retrieve Local Time for a Specific Time Zone

**Query:**  
`"What is the local time in Pacific Standard Time (PST)?"`

**Function Invoked:**  
`local_time`

**Expected Response:**  
`"The local time in Pacific Standard Time (PST) is September 28, 2024, 02:30 PM."`

### b. Convert Time Between Time Zones

**Query:**  
`"Convert September 28, 2024, 10:00 AM from UTC to Eastern Standard Time (EST)."`

**Function Invoked:**  
`timezone_convert`

**Expected Response:**  
`"September 28, 2024, 10:00 AM UTC is September 28, 2024, 06:00 AM Eastern Standard Time (EST)."`

### c. Calculate Time Zone Offset

**Query:**  
`"What is the time zone offset between Pacific Standard Time (PST) and Eastern Standard Time (EST)?"`

**Function Invoked:**  
`timezone_offset`

**Expected Response:**  
`"The time zone offset between Pacific Standard Time (PST) and Eastern Standard Time (EST) is 3 hours."`

---

## 6. Date and Time Operations

### a. Select Dates Based on Criteria

**Query:**  
`"Select all dates that are business days from September 25, 2024, to October 05, 2024."`

**Function Invoked:**  
`date_select` with criteria using `business_day_q`

**Expected Response:**  

The business days between September 25, 2024, and October 05, 2024, are:

September 25, 2024 (Wednesday)
September 26, 2024 (Thursday)
September 27, 2024 (Friday)
September 30, 2024 (Monday)
October 01, 2024 (Tuesday)
October 02, 2024 (Wednesday)
October 03, 2024 (Thursday)
October 04, 2024 (Friday)


### b. Find Date Bounds from a List

**Query:**  
`"Find the earliest and latest dates from the list: September 28, 2024; October 01, 2024; September 30, 2024."`

**Function Invoked:**  
`date_bounds`

**Expected Response:**  

"The earliest date is September 28, 2024, and the latest date is October 01, 2024."


### c. Convert Date to Different Calendar System

**Query:**  
`"Convert September 28, 2024, to the Hebrew calendar system."`

**Function Invoked:**  
`timesystem_convert`

**Expected Response:**  
`"September 28, 2024, in the Hebrew calendar system is [Hebrew Date]."`
*(Note: The exact Hebrew date will depend on the HebrewCalendar implementation.)*

---

## 7. Date and Time Testing

### a. Check if a Date is Within a Range

**Query:**  
`"Is October 01, 2024, within the range September 25, 2024, to October 05, 2024?"`

**Function Invoked:**  
`date_within_q`

**Expected Response:**  
`"Yes, October 01, 2024, is within the specified date range."`

### b. Check if Two Date Ranges Overlap

**Query:**  
`"Do the date ranges September 25, 2024, to October 05, 2024, and October 03, 2024, to October 10, 2024, overlap?"`

**Function Invoked:**  
`date_overlaps_q`

**Expected Response:**  
`"Yes, the two date ranges overlap."`

### c. Determine if a Year is a Leap Year

**Query:**  
`"Is 2024 a leap year?"`

**Function Invoked:**  
`leap_year_q`

**Expected Response:**  
`"Yes, 2024 is a leap year."`

---

## 8. Specialized Day Operations

### a. Generate a Range of Days

**Query:**  
`"Generate a list of all days from September 28, 2024, to October 02, 2024."`

**Function Invoked:**  
`day_range`

**Expected Response:**  


"The days from September 28, 2024, to October 02, 2024, are:

September 28, 2024
September 29, 2024
September 30, 2024
October 01, 2024
October 02, 2024"


### b. Add Days to a Specific Date

**Query:**  
`"Add 15 days to September 28, 2024."`

**Function Invoked:**  
`day_plus`

**Expected Response:**  
`"Adding 15 days to September 28, 2024, results in October 13, 2024."`

### c. Check if a Specific Date is a Business Day

**Query:**  
`"Is October 06, 2024, a business day?"`

**Function Invoked:**  
`business_day_q`

**Expected Response:**  
`"No, October 06, 2024, is not a business day."`  
*(Assuming October 06, 2024, is a Sunday.)*

---

## 9. Statistical Operations on Dates and Times

### a. Calculate Mean Date

**Query:**  
`"What is the mean date of the following dates: September 25, 2024; September 28, 2024; October 01, 2024?"`

**Function Invoked:**  
`mean_date`

**Expected Response:**  
`"The mean date is September 28, 2024."`

### b. Calculate Median Date

**Query:**  
`"What is the median date of the following dates: September 25, 2024; September 28, 2024; October 01, 2024?"`

**Function Invoked:**  
`median_date`

**Expected Response:**  
`"The median date is September 28, 2024."`

---

## 10. Additional Example Queries

### a. Convert a Time String to TimeSpan

**Query:**  
`"Convert the string '15:30:00' to a TimeSpan object."`

**Function Invoked:**  
`create_time`

**Expected Response:**  
`"The TimeSpan object represents 15 hours, 30 minutes, and 0 seconds."`

### b. Check if a Specific Date Falls Within a Date Range

**Query:**  
`"Does September 29, 2024, fall between September 25, 2024, and September 28, 2024?"`

**Function Invoked:**  
`date_within_q`

**Expected Response:**  
`"No, September 29, 2024, does not fall within the specified date range."`

### c. Calculate the Number of Seconds Between Two Times

**Query:**  
`"How many seconds are there between 10:00 AM and 3:30 PM?"`

**Function Invoked:**  
`diff_time`

**Expected Response:**  
`"There are 19800 seconds between 10:00 AM and 3:30 PM."`  
*(Note: 5.5 hours × 3600 seconds/hour = 19800 seconds)*

---

## Integrating Input Queries into Your Application

To effectively handle these queries within your application, consider the following steps:

### a. Parsing User Input

Ensure that your application can **interpret user input** and map it to the appropriate kernel functions. This can be achieved through:

- **Natural Language Processing (NLP):** Utilize the AI model (e.g., GPT-4) to understand the intent and extract relevant parameters.
  
- **Function Invocation Logic:** Based on the interpreted intent, call the corresponding kernel function with the necessary arguments.

### b. Handling Function Responses

After invoking a kernel function, process its response to present it to the user in a friendly format.

### c. Example Integration Workflow

1. **User Input:**  
   `"How many days are there between today and Christmas?"`

2. **AI Model Interpretation:**  
   - **Intent:** Calculate the difference in days.
   - **Extracted Dates:**  
     - **Start Date:** Today (`DateTime.Today`)
     - **End Date:** Christmas (`new DateTime(2024, 12, 25)`)

3. **Function Invocation:**  
   - Call `date_difference_days` with `start = DateTime.Today` and `end = new DateTime(2024, 12, 25)`

4. **Function Response:**  
   - `88` (days)

5. **Application Response:**  
   `"There are 88 days between today (September 28, 2024) and Christmas (December 25, 2024)."`

---

## Best Practices for Writing Input Queries

To maximize the effectiveness of your kernel functions, adhere to the following best practices when designing input queries:

### a. Clarity and Specificity

- **Be Clear:** Ensure that the query clearly states what the user wants to know.
  
  - **Good Example:** `"What is the local time in Tokyo right now?"`
  
  - **Bad Example:** `"Time in Tokyo?"`

### b. Provide Necessary Parameters

- **Include All Required Information:** If a function requires specific parameters, ensure the query includes them.
  
  - **Good Example:** `"Add 5 days to October 10, 2024."`
  
  - **Bad Example:** `"Add days to date."`

### c. Use Recognizable Date and Time Formats

- **Consistent Formatting:** Use standard date and time formats to facilitate accurate parsing.
  
  - **Good Example:** `"Convert '07/04/2024' to a DateTime object using 'MM/dd/yyyy' format."`
  
  - **Bad Example:** `"Convert July fourth to date."`

### d. Handle Ambiguities

- **Clarify Ambiguous Queries:** If a query could be interpreted in multiple ways, provide additional context.
  
  - **Example:**  
    `"What is the difference in days between the start of the project on September 28 and the deadline on December 25?"`

### e. Utilize Contextual Information

- **Maintain Context:** If your application maintains conversational context, ensure that follow-up queries reference previous information appropriately.
  
  - **Example:**  
    - **User:** `"What is the current time in UTC?"`  
    - **App:** `"The current time in UTC is 12:30 PM."`  
    - **User:** `"What time is that in Pacific Standard Time?"`  
    - **App:** `"12:30 PM UTC is 4:30 AM Pacific Standard Time."`

---

## Final Thoughts

By structuring your input queries thoughtfully and leveraging the comprehensive set of date and time functions you've integrated, your application can provide **powerful and flexible** date and time-related functionalities to users. Remember to **test extensively** with various query types to ensure that all functions behave as expected and handle edge cases gracefully.

