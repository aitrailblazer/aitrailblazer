// TokenCounts.cs
namespace AITrailblazer.net.Services
{
    public class TokenCounts
    {
        public int CompletionTokens { get; init; }
        public int PromptTokens { get; init; }
        public int TotalTokens { get; init; }

        public TokenCounts(int completionTokens, int promptTokens, int totalTokens)
        {
            CompletionTokens = completionTokens;
            PromptTokens = promptTokens;
            TotalTokens = totalTokens;
        }
    }
}
