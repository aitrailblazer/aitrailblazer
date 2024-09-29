public class ChatAgentConfig
{
    public string Name { get; set; }
    public string Instructions { get; set; }
    public bool IsReviewer { get; set; }
    public double Temperature { get; set; } // Agent-specific temperature
    public double TopP { get; set; }        // Agent-specific topP
    public int MaxTokens { get; set; }      // Agent-specific maxTokens
}