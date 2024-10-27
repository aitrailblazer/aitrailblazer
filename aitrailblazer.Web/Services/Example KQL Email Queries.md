Natural Language Email Search Queries

Below are examples of natural language email search descriptions that correspond to various KQL functions registered in the SetupKQLPlugin method. These queries can be translated into precise KQL queries using GPT functions integrated with your C# application.

1. Basic KQL Queries

Find all emails containing the keyword 'urgent'.
Find emails mentioning 'ASAP'.
Find emails discussing the Q3 project report.
2. Field-Specific KQL Queries

Find all emails where the sender is 'john.doe@example.com'.
Find emails where the BCC recipient is 'manager@example.com'.
Find emails sent to 'marketing@company.com'.
Find emails sent to multiple people in the sales team.
3. Attachment-Related KQL Queries

Find emails with PDF attachments.
Search for emails that have attachments larger than 5MB.
Find emails with Excel spreadsheets attached.
4. Recipient-Based KQL Queries

Find emails I sent to 'marketing@company.com'.
Show emails where I was CC'd.
Find emails sent to multiple people in the sales team.
5. Importance and Flags KQL Queries

Show all high importance emails.
Find flagged emails from my boss.
Search for low priority emails from last month.
6. Content-Based KQL Queries

Find emails mentioning the Q3 project report.
Show emails containing the phrase 'please review'.
Search for emails discussing budget cuts.
Find emails mentioning 'contract renewals'.
7. Combination KQL Queries

Find unread emails from John with attachments.
Show high priority emails from clients sent this week.
Find emails about the conference that have PDF attachments.
Find high-importance emails from 'alice@example.com' or 'bob@example.com' containing 'meeting agenda'.
8. Exclusion KQL Queries

Show all emails not from the IT department.
Find emails without attachments sent last month.
Search for emails about the project, excluding status updates.
Exclude emails with attachments from 'spam@example.com'.
9. Size-Based KQL Queries

Find large emails over 10MB.
Show small emails less than 1KB from yesterday.
10. Proximity KQL Queries

Find emails where 'budget' is near 'forecast' in the subject.
Find emails where 'meeting' appears near 'agenda' in the body.
Search for emails where 'deadline' is close to 'project' in the text.
11. Range-Based KQL Queries

Find emails received between January 1, 2023, and December 31, 2023.
Find emails sent after March 15, 2023.
Search for emails received from June to August 2023.
12. Grouping KQL Queries

Combine emails from 'alice@example.com' and 'bob@example.com' using OR.
Group search results for multiple senders using AND.
Group multiple conditions to find emails from Sarah or Michael about the annual report.
13. Synonym KQL Queries

Find emails containing either 'TV' or 'Television' in the body.
Search for emails with synonyms for 'budget' like 'financial plan'.
Find emails mentioning 'meeting' or 'conference'.
14. Wildcard KQL Queries

Find emails with subjects starting with 'Serv' (e.g., 'Service Update', 'Server Issues').
Search for emails with attachments that start with 'Report*'.
Find emails where the subject begins with 'Proje*'.
15. XRANK KQL Queries

Find emails containing 'project update' in the body and boost their ranking if the subject is 'urgent'.
Boost the ranking of emails where 'budget' appears near 'forecast'.
Enhance the prominence of emails with 'important' in the subject.
16. Kind KQL Queries

Find emails that are of kind 'email'.
Search for voicemails in the messages.
Retrieve emails and voicemails related to the recent project.
17. Participants KQL Queries

Find emails involving participants 'John' and 'Jane'.
Search for emails with multiple participants in the meeting.
Locate emails that include 'Alice', 'Bob', and 'Charlie' as participants.
18. Sent and Received Date KQL Queries

Find emails sent in the last two weeks and received by 'manager@example.com'.
Search for emails sent before January 2023 and received after December 2022.
Find emails received on weekends.
19. Participants and Kind KQL Queries

Find email conversations involving 'John' and 'Sarah' about the merger.
Search for threads with 'Alice' discussing project timelines.
Retrieve all email conversations that include 'Bob' and are related to 'budget planning'.
20. Combining Multiple KQL Functions

Find high-importance emails from Sarah or Michael about the annual report, sent in the last two weeks, with PowerPoint attachments.
Show unread, high importance emails from clients, received this month, mentioning contract renewals.
Find emails I sent to the finance team last quarter, excluding automated reports, with Excel attachments larger than 2MB.
21. Folder or Category-Based KQL Queries

Search for emails in the 'Projects' folder about the website redesign.
Find emails categorized as 'Personal' received this year.
Locate all emails in the 'Receipts' folder with attachments.
22. Conversation or Thread-Based KQL Queries

Show me all email threads involving John and Sarah about the merger.
Find the latest email in the thread about budget approvals.
Retrieve all messages in the conversation about the new product launch.
These natural language queries cover a wide range of search scenarios that correspond to the KQL functions registered in your SetupKQLPlugin method. By translating these descriptions into KQL queries using GPT functions, you can enhance the search capabilities of your applications, providing users with powerful and intuitive tools to locate their emails efficiently.