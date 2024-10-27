using System.Text.Json.Serialization;
using Microsoft.TypeChat.Schema;

namespace AITrailblazer.net.Models;

/// <summary>
/// Represents a structured response for a list of news articles retrieved from a search.
/// </summary>
[Comment("Defines the structure of NewsResponseStructured for handling a list of news articles.")]
public class NewsResponseStructured
{
    public NewsResponseStructured()
    {
        Name = "NewsResponseStructured"; // Hardcoded name for recognition
    }

    [JsonPropertyName("name")]
    [Comment("The hardcoded name of this structured response.")]
    public string Name { get; }

    [JsonPropertyName("totalResults")]
    [Comment("The total number of results returned from the search.")]
    public int TotalResults { get; set; }

    [JsonPropertyName("articles")]
    [Comment("A list of news articles matching the search query.")]
    public NewsArticleStructured[] Articles { get; set; }

}

/// <summary>
/// Represents a news article retrieved from the search.
/// </summary>
[Comment("Represents the structure of a news article retrieved from the search.")]
public class NewsArticleStructured
{
    [JsonPropertyName("name")]
    [Comment("The name of the news article.")]
    public string Name { get; set; }

    [JsonPropertyName("url")]
    [Comment("The URL link to the full news article.")]
    public string Url { get; set; }

    [JsonPropertyName("description")]
    [Comment("A short description of the article content.")]
    public string Description { get; set; }

    [JsonPropertyName("thumbnailUrl")]
    [Comment("The URL to the thumbnail image of the article, if available.")]
    public string ThumbnailUrl { get; set; }

    [JsonPropertyName("datePublished")]
    [Comment("The date and time when the article was published.")]
    public string DatePublished { get; set; }

    [JsonPropertyName("source")]
    [Comment("The name of the provider or source of the article (e.g., Forbes, BBC).")]
    public string Source { get; set; }

    [JsonPropertyName("category")]
    [Comment("The category of the article (e.g., Technology, Business).")]
    public string Category { get; set; }
}
