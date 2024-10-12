namespace aitrailblazer.net.Models
{
    public class NewsArticle
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string ThumbnailUrl { get; set; }
    }
       
    public class NewsResponse
    {
        public string Query { get; set; }
        public int TotalResults { get; set; }
        public List<NewsArticle> Articles { get; set; }
    }
}
