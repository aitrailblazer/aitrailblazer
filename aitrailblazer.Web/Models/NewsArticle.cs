namespace aitrailblazer.net.Models
{
    public class NewsArticle
    {
        // Name of the news article
        public string Name { get; set; }
        
        // URL link to the full news article
        public string Url { get; set; }
        
        // Short description of the article content
        public string Description { get; set; }
        
        // URL to the thumbnail image of the article, if available
        public string ThumbnailUrl { get; set; }
        
        // Date and time the article was published
        public string DatePublished { get; set; }
        
        // Name of the provider or source of the article (e.g., Forbes, BBC)
        public string Source { get; set; }
        
        // Category of the article (e.g., Technology, Business)
        public string Category { get; set; }
    }
    
    public class NewsResponse
    {
        // The original query made by the user
        public string Query { get; set; }
        
        // Total number of news articles retrieved
        public int TotalResults { get; set; }
        
        // List of news articles retrieved from the search
        public List<NewsArticle> Articles { get; set; }
        
        // The date the news was fetched, for logging and reference purposes
        //public DateTime FetchedAt { get; set; }
    }
}
