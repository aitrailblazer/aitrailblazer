# Date and Time Operations Guide

This guide provides examples of date and time operations related to your local time zone. Each operation includes a simple query you can ask, the function invoked, and the expected response.

---

## 1. Local Time Zone Operations

### a. Show me my current time

**Query:**  
`"Show me my current time"`

**Function Invoked:**  
`display_local_time`

**Expected Response:**  
`"The current time in your local time zone (EST) is September 28, 2024, 10:30 AM."`

### b. What is my current time zone?

**Query:**  
`"What is my current time zone?"`

**Function Invoked:**  
`detect_local_timezone`

**Expected Response:**  
`"Your current time zone is Eastern Standard Time (EST)."`

### c. Convert 10:00 AM UTC to my local time

**Query:**  
`"Convert 10:00 AM UTC to my local time"`

**Function Invoked:**  
`convert_utc_to_local`

**Expected Response:**  
`"10:00 AM UTC is 6:00 AM EST in your local time zone."`

### d. Schedule a meeting at 3:00 PM in my local time

**Query:**  
`"Schedule a meeting at 3:00 PM in my local time"`

**Function Invoked:**  
`schedule_event_local`

**Expected Response:**  
`"A meeting has been scheduled at 3:00 PM EST in your local time zone."`

### e. Adjust for Daylight Saving Time in my time zone

**Query:**  
`"What time will it be at 2:00 AM on November 3, 2024, in my time zone, considering Daylight Saving Time?"`

**Function Invoked:**  
`adjust_dst_local`

**Expected Response:**  
`"On November 3, 2024, at 2:00 AM, your local time will adjust to 1:00 AM EST due to the end of Daylight Saving Time."`

### f. What is the UTC offset for my time zone?

**Query:**  
`"What is the UTC offset for my time zone?"`

**Function Invoked:**  
`local_timezone_offset`

**Expected Response:**  
`"Your local time zone (EST) is currently UTC-5 hours."`

### g. Show the New Year time in my local time zone

**Query:**  
`"Show the New Year time in my local time zone"`

**Function Invoked:**  
`display_event_local`

**Expected Response:**  
`"The New Year will occur on December 31, 2024, at 11:59 PM EST in your local time zone."`

### h. Is my birthday before today in my local time zone?

**Query:**  
`"Is my birthday on July 4, 2024, before today in my local time zone?"`

**Function Invoked:**  
`compare_local_dates`

**Expected Response:**  
`"Your birthday on July 4, 2024, is before today in your local time zone."`

### i. List upcoming holidays in my local time zone

**Query:**  
`"List upcoming holidays in my local time zone"`

**Function Invoked:**  
`list_local_holidays`

**Expected Response:**
"Upcoming holidays in your local time zone (EST):

New Year's Day: January 01, 2024
Independence Day: July 04, 2024
Thanksgiving: November 28, 2024
Christmas Day: December 25, 2024"


### j. Convert 9:00 AM my time to PST

**Query:**  
`"Convert 9:00 AM my time to PST"`

**Function Invoked:**  
`convert_local_to_timezone`

**Expected Response:**  
`"9:00 AM EST is 6:00 AM PST."`

---

These examples provide a simple and user-friendly way to interact with date and time functions related to your local time zone. From displaying your current time to handling time zone conversions and holiday listings, these queries ensure personalized and accurate results.

If you need further examples or have specific scenarios in mind, feel free to ask!
