using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using aitrailblazer.Web.Services;
using CognitiveServices.Sdk.News;
using CognitiveServices.Sdk.News.Search;
using CognitiveServices.Sdk.News.Trendingtopics;
using CognitiveServices.Sdk.Models;
using System.Text.Json; // For JSON serialization
using aitrailblazer.net.Models; // For NewsArticle and NewsResponse
using SearchSdk = CognitiveServices.Sdk.News.Search;
using TrendingSdk = CognitiveServices.Sdk.News.Trendingtopics;
using OurNewsArticle = aitrailblazer.net.Models.NewsArticle;
using ExternalNewsArticle = CognitiveServices.Sdk.Models.NewsArticle;

namespace aitrailblazer.net.Services
{
    public class NewsFunctions
    {
        private readonly BingNewsService _bingNewsService;
        private readonly ILogger<NewsFunctions> _logger;

        public NewsFunctions(
            BingNewsService bingNewsService,
            ILogger<NewsFunctions> logger)
        {
            _bingNewsService = bingNewsService;
            _logger = logger;
        }

        #region Core Functions

        // Function to search for news articles
        public async Task<string> SearchNewsAsync(string userQuery)
        {
            _logger.LogInformation("Entering 'SearchNewsAsync' with userQuery: {UserQuery}", userQuery);

            var newsResults = await _bingNewsService.SearchNewsAsync(userQuery, queryParameters =>
            {
                queryParameters.Mkt = "en-US";
                queryParameters.Count = 10;
                queryParameters.Freshness = SearchSdk.GetFreshnessQueryParameterType.Day;
                queryParameters.SafeSearch = SearchSdk.GetSafeSearchQueryParameterType.Moderate;
                queryParameters.TextFormat = SearchSdk.GetTextFormatQueryParameterType.Raw;
                queryParameters.OriginalImg = true;
            });
              // Serialize the response into a well-formatted JSON output
            var jsonResponse = JsonSerializer.Serialize(newsResults, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            _logger.LogInformation("Exiting 'SearchNewsAsync' with response: {Response}", jsonResponse);

            return jsonResponse;
        
            //return FormatNewsResults(newsResults, userQuery);
        }

        // Function to fetch trending topics
        public async Task<string> GetTrendingTopicsAsync(string userQuery)
        {
            _logger.LogInformation("Entering 'GetTrendingTopicsAsync'");

            var trendingTopics = await _bingNewsService.GetTrendingTopicsAsync(queryParameters =>
            {
                queryParameters.Mkt = "en-US";
                queryParameters.Count = 5;
                queryParameters.SafeSearch = TrendingSdk.GetSafeSearchQueryParameterType.Moderate;
                queryParameters.TextFormat = TrendingSdk.GetTextFormatQueryParameterType.Raw;
            });

            return FormatTrendingTopics(trendingTopics, userQuery);
        }

        // Function to fetch headline news
        public async Task<string> GetHeadlineNewsAsync()
        {
            _logger.LogInformation("Entering 'GetHeadlineNewsAsync'");

            var headlineNews = await _bingNewsService.GetHeadlineNewsAsync(queryParameters =>
            {
                queryParameters.Mkt = "en-US";
                queryParameters.Count = 10;
                queryParameters.SafeSearch = SearchSdk.GetSafeSearchQueryParameterType.Moderate;
            });

            return FormatNewsResults(headlineNews, "Headline News");
        }

        // Helper method to format news results
 private string FormatNewsResults(News? newsResults, string query)
{
    if (newsResults?.Value != null && newsResults.Value.Count > 0)
    {
        var newsArticles = newsResults.Value;

        // Format the news articles with additional fields (e.g., DatePublished, Source, Category)
        var formattedArticles = newsArticles.Select(article => new OurNewsArticle
        {
            Name = article.Name,
            Url = article.Url,
            Description = article.Description,
            ThumbnailUrl = article.Image?.Thumbnail?.ContentUrl ?? string.Empty,
            //DatePublished = article.DatePublished.HasValue
            //    ? article.DatePublished.Value.ToString("yyyy-MM-dd HH:mm:ss")
            //    : "Date not available",
            Source = article.Provider != null && article.Provider.Any()
                ? article.Provider.FirstOrDefault()?.Name ?? "Unknown source"
                : "Unknown source",
            //Category = article.Category ?? "General"
        }).ToList();

        var newsResponse = new NewsResponse
        {
            Query = query,
            TotalResults = newsArticles.Count,
            Articles = formattedArticles,
            //FetchedAt = DateTime.UtcNow // Capture the time of fetching the news
        };

        // Serialize the response into a well-formatted JSON output
        var jsonResponse = JsonSerializer.Serialize(newsResponse, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        _logger.LogInformation("Exiting 'FormatNewsResults' with response: {Response}", jsonResponse);

        return jsonResponse;
    }
    else
    {
        _logger.LogWarning("No news articles found for query: {Query}", query);

        var emptyResponse = new NewsResponse
        {
            Query = query,
            TotalResults = 0,
            Articles = new List<OurNewsArticle>(),
            //FetchedAt = DateTime.UtcNow
        };

        var jsonEmptyResponse = JsonSerializer.Serialize(emptyResponse, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        return jsonEmptyResponse;
    }
}

        // Helper method to format trending topics
        private string FormatTrendingTopics(TrendingTopics? trendingTopics, string query)
        {
            if (trendingTopics?.Value != null && trendingTopics.Value.Count > 0)
            {
                var topics = trendingTopics.Value;

                var formattedTopics = topics.Select(topic => new OurNewsArticle
                {
                    Name = topic.Name,
                    Url = topic.WebSearchUrl,
                    Description = topic.Query?.Text ?? string.Empty,
                    ThumbnailUrl = topic.Image?.Url ?? string.Empty
                }).ToList();

                var newsResponse = new NewsResponse
                {
                    Query = query,
                    TotalResults = topics.Count,
                    Articles = formattedTopics
                };

                var jsonResponse = JsonSerializer.Serialize(newsResponse, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                _logger.LogInformation("Exiting 'FormatTrendingTopics' with response: {Response}", jsonResponse);
                return jsonResponse;
            }
            else
            {
                _logger.LogWarning("No trending topics found.");

                var emptyResponse = JsonSerializer.Serialize(new
                {
                    Query = "Trending Topics",
                    TotalResults = 0,
                    Articles = new List<OurNewsArticle>()
                });

                return emptyResponse;
            }
        }

        #endregion

        #region AI-Exposed Wrapper Functions

        // Wrapper function to expose SearchNewsAsync for AI interactions
        public async Task<string> SearchNewsAsyncForAI(string userQuery)
        {
            _logger.LogInformation("Entering 'SearchNewsAsyncForAI' with userQuery: {UserQuery}", userQuery);

            var newsResponse = await SearchNewsAsync(userQuery);

            _logger.LogInformation("Exiting 'SearchNewsAsyncForAI' with newsResponse length: {OutputLength}", newsResponse.Length);

            return newsResponse;
        }

        // Wrapper function to expose GetTrendingTopicsAsync for AI interactions
        public async Task<string> GetTrendingTopicsAsyncForAI(string userQuery)
        {
            _logger.LogInformation("Entering 'GetTrendingTopicsAsyncForAI' with userQuery: {UserQuery}", userQuery);

            var newsResponse = await GetTrendingTopicsAsync(userQuery);

            _logger.LogInformation("Exiting 'GetTrendingTopicsAsyncForAI' with newsResponse length: {OutputLength}", newsResponse.Length);

            return newsResponse;
        }

        // Wrapper function to expose GetHeadlineNewsAsync for AI interactions
        public async Task<string> GetHeadlineNewsAsyncForAI()
        {
            _logger.LogInformation("Entering 'GetHeadlineNewsAsyncForAI'");

            var newsResponse = await GetHeadlineNewsAsync();

            _logger.LogInformation("Exiting 'GetHeadlineNewsAsyncForAI' with newsResponse length: {OutputLength}", newsResponse.Length);

            return newsResponse;
        }

        #endregion
    }
}
