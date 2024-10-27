namespace AITrailblazer.net.Models
{
    public class ChatMessageM
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        // Add an IconName property to store the name of the icon as a string
        public string IconName { get; set; }
    }
}