public class TokenCounts
{
    public int OutputTokens { get; init; }
    public int InputTokens { get; init; }
    public int TotalTokens { get; init; }

    public TokenCounts(int outputTokens, int inputTokens, int totalTokens)
    {
        OutputTokens = outputTokens;
        InputTokens = inputTokens;
        TotalTokens = totalTokens;
    }
}
