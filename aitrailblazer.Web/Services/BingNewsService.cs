using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using CognitiveServices.Sdk.News;
using CognitiveServices.Sdk.News.Search;
using CognitiveServices.Sdk.News.Trendingtopics;
using CognitiveServices.Sdk.Models;
using Microsoft.Extensions.Logging;
using aitrailblazer.net.Services; // Assuming this is the namespace where ParametersAzureService is located
using System.Net.Http; // Added for HttpClient
using System.Text.Json; // Added for JSON serialization
using System.Text.Json.Serialization; // Added for JSON serialization options

public class BingNewsService
{
    private readonly NewsRequestBuilder _newsRequestBuilder;
    private readonly ILogger<BingNewsService> _logger;
    private readonly ParametersAzureService _parametersAzureService;

    public BingNewsService(
        ParametersAzureService parametersAzureService,
        ILogger<BingNewsService> logger,
        string baseUrl = "https://api.bing.microsoft.com/v7.0")
    {
        _parametersAzureService = parametersAzureService ?? throw new ArgumentNullException(nameof(parametersAzureService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Retrieve the API key internally
        var apiKey = _parametersAzureService.BingSearchApiKey;
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogError("Bing Search API key is not provided in ParametersAzureService.BingSearchApiKey.");
            throw new InvalidOperationException("Bing Search API key must be provided in ParametersAzureService.BingSearchApiKey.");
        }

        _logger.LogInformation("Initializing BingNewsService with base URL: {BaseUrl}", baseUrl);

        // Create an authentication provider that adds the API key to the request headers
        var authenticationProvider = new BingApiKeyAuthenticationProvider(apiKey);

        // Create the custom logging handler
        var loggingHandler = new LoggingHandler(_logger)
        {
            InnerHandler = new HttpClientHandler() // Ensure the inner handler is set
        };

        // Create an HttpClient with the logging handler
        var httpClient = new HttpClient(loggingHandler);

        // Create the request adapter using the custom HttpClient
        var requestAdapter = new HttpClientRequestAdapter(
            authenticationProvider: authenticationProvider,
            httpClient: httpClient // Pass the HttpClient as a named parameter
        );

        // Set the base URL for the Bing News API
        requestAdapter.BaseUrl = baseUrl;

        // Initialize the NewsRequestBuilder with an empty path parameters dictionary
        var pathParameters = new Dictionary<string, object>();
        _newsRequestBuilder = new NewsRequestBuilder(pathParameters, requestAdapter);

        _logger.LogInformation("BingNewsService initialized successfully.");
    }

    // Method to fetch trending topics
    public async Task<TrendingTopics?> GetTrendingTopicsAsync(
        Action<TrendingtopicsRequestBuilder.TrendingtopicsRequestBuilderGetQueryParameters>? queryParameters = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Starting GetTrendingTopicsAsync.");

        try
        {
            _logger.LogDebug("Building request for trending topics.");

            var trendingTopics = await _newsRequestBuilder.Trendingtopics.GetAsync(requestConfiguration =>
            {
                if (queryParameters != null)
                {
                    _logger.LogDebug("Applying query parameters for trending topics.");
                    requestConfiguration.QueryParameters = new TrendingtopicsRequestBuilder.TrendingtopicsRequestBuilderGetQueryParameters();
                    queryParameters.Invoke(requestConfiguration.QueryParameters);
                }
            }, cancellationToken).ConfigureAwait(false);

            if (trendingTopics != null && trendingTopics.Value != null)
            {
                _logger.LogInformation("Successfully retrieved trending topics. Count: {Count}", trendingTopics.Value.Count);

                // Log details of each trending topic
                foreach (var topic in trendingTopics.Value)
                {
                    _logger.LogInformation("Trending Topic - Name: {Name}, Query: {Query}, URL: {WebSearchUrl}",
                        topic.Name, topic.Query?.Text, topic.WebSearchUrl);
                }
            }
            else
            {
                _logger.LogWarning("Trending topics response is null or empty.");
            }

            return trendingTopics;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API Error while fetching trending topics: {Message}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching trending topics: {Message}", ex.Message);
            throw;
        }
        finally
        {
            _logger.LogInformation("GetTrendingTopicsAsync completed.");
        }
    }

    // Method to search for news articles
    public async Task<News?> SearchNewsAsync(
        string query,
        Action<SearchRequestBuilder.SearchRequestBuilderGetQueryParameters>? queryParameters = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            _logger.LogWarning("Search query is null or empty.");
            throw new ArgumentException("Search query must be provided.", nameof(query));
        }

        _logger.LogInformation("Starting SearchNewsAsync with query: {Query}", query);

        try
        {
            _logger.LogDebug("Building request for news search.");

            var news = await _newsRequestBuilder.Search.GetAsync(requestConfiguration =>
            {
                requestConfiguration.QueryParameters = new SearchRequestBuilder.SearchRequestBuilderGetQueryParameters
                {
                    Q = query
                };

                if (queryParameters != null)
                {
                    _logger.LogDebug("Applying additional query parameters for news search.");
                    queryParameters.Invoke(requestConfiguration.QueryParameters);
                }
            }, cancellationToken).ConfigureAwait(false);

            if (news != null && news.Value != null)
            {
                _logger.LogInformation("Successfully retrieved news articles for query '{Query}'. Count: {Count}", query, news.Value.Count);

                // Log details of each news article
                foreach (var article in news.Value)
                {
                    // Attempt to access the publication date
                    var datePublished = GetArticleDatePublished(article);

                    _logger.LogInformation("News Article - Title: {Title}, URL: {Url}, DatePublished: {DatePublished}",
                        article.Name, article.Url, datePublished ?? "N/A");
                }
            }
            else
            {
                _logger.LogWarning("News search response is null or empty for query '{Query}'.", query);
            }

            return news;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, "API Error while searching news for query '{Query}': {Message}", query, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while searching news for query '{Query}': {Message}", query, ex.Message);
            throw;
        }
        finally
        {
            _logger.LogInformation("SearchNewsAsync completed for query: {Query}", query);
        }
    }

    // Helper method to extract DatePublished from the article
    private string? GetArticleDatePublished(NewsArticle article)
    {
        // Attempt to get the DatePublished property
        if (article.AdditionalData != null && article.AdditionalData.TryGetValue("datePublished", out var datePublishedValue))
        {
            return datePublishedValue?.ToString();
        }

        return null;
    }
}

// Custom DelegatingHandler to log raw JSON responses
public class LoggingHandler : DelegatingHandler
{
    private readonly ILogger _logger;

    public LoggingHandler(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // Set the InnerHandler to HttpClientHandler if not already set
        if (InnerHandler == null)
        {
            InnerHandler = new HttpClientHandler();
        }
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Log request details
        _logger.LogInformation("Sending request to {Url}", request.RequestUri);

        // Proceed with the request
        var response = await base.SendAsync(request, cancellationToken);

        // Read the raw JSON response
        var content = await response.Content.ReadAsStringAsync();

        // Log the raw JSON
        _logger.LogInformation("Received response from {Url} with status code {StatusCode}:\n{Json}",
            request.RequestUri, response.StatusCode, content);

        // Return the response
        return response;
    }
}

public class BingApiKeyAuthenticationProvider : IAuthenticationProvider
{
    private readonly string _apiKey;

    public BingApiKeyAuthenticationProvider(string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key must be provided.", nameof(apiKey));

        _apiKey = apiKey;
    }

    public Task AuthenticateRequestAsync(
        RequestInformation request,
        Dictionary<string, object>? additionalAuthenticationContext = null,
        CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        // Add the API key to the request headers
        if (!request.Headers.ContainsKey("Ocp-Apim-Subscription-Key"))
        {
            request.Headers.Add("Ocp-Apim-Subscription-Key", _apiKey);
            // Do not log the API key for security reasons
        }

        return Task.CompletedTask;
    }
}
