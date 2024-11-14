// TokenUsageParser.cs
using OpenAI.Chat;
using Microsoft.SemanticKernel;
using AITrailblazer.net.Services;

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
                    completionTokens: usage.OutputTokenCount,
                    promptTokens: usage.InputTokenCount,
                    totalTokens: usage.TotalTokenCount)
                : null;
        }
    }
}
