/// <summary>
/// Structure to hold the settings for a chat agent.
/// </summary>
public struct ChatAgentSettings
{
    public string Name { get; set; }
    
    public string Instructions { get; set; }
    public double Temperature { get; set; }
    public double TopP { get; set; }
    public int MaxTokens { get; set; }
}

