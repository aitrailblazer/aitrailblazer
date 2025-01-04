using OpenAI.Chat;
using Microsoft.SemanticKernel;

namespace AITrailblazer.net.Services
{
    public static class TokenUsageParser
    {
        // Method to parse token counts from chat message content metadata.
        public static TokenCounts? ParseTokenCounts(Microsoft.SemanticKernel.ChatMessageContent chatMessageContent)
        {
            var usage = chatMessageContent.Metadata?["Usage"] as ChatTokenUsage;

            return usage != null
                ? new TokenCounts(
                    outputTokens: usage.OutputTokenCount,
                    inputTokens: usage.InputTokenCount,
                    totalTokens: usage.TotalTokenCount)
                : null;
        }
    }
}
