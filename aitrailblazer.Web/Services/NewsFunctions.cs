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

            if (newsResults?.Value != null && newsResults.Value.Count > 0)
            {
                var newsArticles = newsResults.Value;

                var formattedArticles = newsArticles.Select(article => new OurNewsArticle
                {
                    Name = article.Name,
                    Url = article.Url,
                    Description = article.Description,
                    ThumbnailUrl = article.Image?.Thumbnail?.ContentUrl
                }).ToList();

                var newsResponse = new NewsResponse
                {
                    Query = userQuery,
                    TotalResults = newsArticles.Count,
                    Articles = formattedArticles
                };

                var jsonResponse = JsonSerializer.Serialize(newsResponse, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                _logger.LogInformation("Exiting 'SearchNewsAsync' with response: {Response}", jsonResponse);

                return jsonResponse;
            }
            else
            {
                _logger.LogWarning("No news articles found for userQuery: {UserQuery}", userQuery);

                var emptyResponse = new NewsResponse
                {
                    Query = userQuery,
                    TotalResults = 0,
                    Articles = new List<OurNewsArticle>()
                };

                var jsonEmptyResponse = JsonSerializer.Serialize(emptyResponse, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                return jsonEmptyResponse;
            }
        }

        // Updated GetTrendingTopicsAsync
        public async Task<string> GetTrendingTopicsAsync()
        {
            _logger.LogInformation("Entering 'GetTrendingTopicsAsync'");

            var trendingTopics = await _bingNewsService.GetTrendingTopicsAsync(queryParameters =>
            {
                queryParameters.Mkt = "en-US";
                queryParameters.Count = 10;
                queryParameters.SafeSearch = TrendingSdk.GetSafeSearchQueryParameterType.Moderate;
                queryParameters.TextFormat = TrendingSdk.GetTextFormatQueryParameterType.Raw;
            });

            if (trendingTopics?.Value != null && trendingTopics.Value.Count > 0)
            {
                var topics = trendingTopics.Value;

                var formattedTopics = topics.Select(topic => new OurNewsArticle
                {
                    Name = topic.Name,
                    Url = topic.WebSearchUrl,
                    Description = topic.Query?.Text ?? string.Empty,
                    ThumbnailUrl = null // Assuming trending topics don't have thumbnails
                }).ToList();

                var newsResponse = new NewsResponse
                {
                    Query = "Trending Topics",
                    TotalResults = topics.Count,
                    Articles = formattedTopics
                };

                // Serialize the response to JSON
                var jsonResponse = JsonSerializer.Serialize(newsResponse, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                _logger.LogInformation("Exiting 'GetTrendingTopicsAsync' with response: {Response}", jsonResponse);
                return jsonResponse;
            }
            else
            {
                _logger.LogWarning("No trending topics found.");
                // Return an empty response or an error message in JSON
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

        /// <summary>
        /// Wrapper function to expose SearchNewsAsync for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <param name="userQuery">The search query entered by the user.</param>
        /// <returns>Formatted news search results with StopSequence.</returns>
        public async Task<string> SearchNewsAsyncForAI(string userQuery)
        {
            _logger.LogInformation("Entering 'SearchNewsAsyncForAI' with userQuery: {UserQuery}", userQuery);

            var newsResponse = await SearchNewsAsync(userQuery);

            _logger.LogInformation("Exiting 'SearchNewsAsyncForAI' with newsResponse length: {OutputLength}", newsResponse.Length);

            return newsResponse;
        }

        /// <summary>
        /// Wrapper function to expose GetTrendingTopicsAsync for AI interactions with StopSequence "\n\n".
        /// </summary>
        /// <returns>Formatted trending topics with StopSequence.</returns>
        public async Task<string> GetTrendingTopicsAsyncForAI()
        {
            _logger.LogInformation("Entering 'GetTrendingTopicsAsyncForAI'");

            var newsResponse = await GetTrendingTopicsAsync();


            _logger.LogInformation("Exiting 'GetTrendingTopicsAsyncForAI' with newsResponse length: {OutputLength}", newsResponse.Length);

            return newsResponse;
        }
        #endregion
    }
}
