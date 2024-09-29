using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

public class WebPageContentExtractionService
{
    private readonly HttpClient httpClient;

    public WebPageContentExtractionService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<WebPageData>> ExtractUrlsContentAsync(string urls)
    {
        var lines = urls.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        var webPages = new List<WebPageData>();
        var titleCounts = new Dictionary<string, int>();

        foreach (var url in lines)
        {
            Console.WriteLine($"Processing URL: {url}");

            try
            {
                // Validate URL format
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
                    (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                {
                    Console.WriteLine($"Invalid URL format: {url}");
                    continue;
                }

                // Fetch the content from the URL
                Console.WriteLine($"Fetching content from: {uri.AbsoluteUri}");
                var response = await httpClient.GetStringAsync(uri.AbsoluteUri);
                Console.WriteLine($"Successfully fetched content from: {uri.AbsoluteUri}");

                // Load HTML content
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(response);
                Console.WriteLine($"Loaded HTML content for: {uri.AbsoluteUri}");

                // Extract the title
                var titleNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
                var title = titleNode?.InnerText ?? "No Title";
                Console.WriteLine($"Extracted title: {title}");

                // Ensure unique titles
                if (titleCounts.ContainsKey(title))
                {
                    titleCounts[title]++;
                    title = $"{title} ({titleCounts[title]})";
                    Console.WriteLine($"Duplicate title found, adjusted title: {title}");
                }
                else
                {
                    titleCounts[title] = 1;
                }

                // Extract icon URL
                var iconNode = htmlDocument.DocumentNode.SelectSingleNode("//link[@rel='icon']") ??
                               htmlDocument.DocumentNode.SelectSingleNode("//link[@rel='shortcut icon']");
                var iconUrl = iconNode?.GetAttributeValue("href", string.Empty);
                if (iconUrl != null)
                {
                    iconUrl = new Uri(uri, iconUrl).AbsoluteUri;
                    Console.WriteLine($"Extracted icon URL: {iconUrl}");
                }
                else
                {
                    Console.WriteLine("No icon URL found.");
                }

                // Format and extract content
                var content = FormatText(htmlDocument.DocumentNode.InnerText);
                Console.WriteLine($"Formatted content for: {uri.AbsoluteUri}");

                // Add web page data to the list
                webPages.Add(new WebPageData
                {
                    Url = uri.AbsoluteUri,
                    Title = title,
                    Content = content,
                    IconUrl = iconUrl
                });

                Console.WriteLine($"Successfully processed URL: {uri.AbsoluteUri}");
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Error fetching content from {url}: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error with URL {url}: {ex.Message}");
            }
        }

        Console.WriteLine("Finished processing all URLs.");
        return webPages;
    }

    private string FormatText(string text)
    {
        var trimmedText = text.Trim();
        var formattedText = Regex.Replace(trimmedText, @"\s*\n\s*", "\n\n");

        const int maxLength = 50000;
        if (formattedText.Length > maxLength)
        {
            formattedText = formattedText.Substring(0, maxLength) + "...";
        }

        return formattedText;
    }
}