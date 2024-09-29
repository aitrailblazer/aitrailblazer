public class AgentSettings
{
    // Writer properties
    public string WriterRoleName { get; set; }
    public string WriterRoleDescription { get; set; }
    public string WriterInstructions { get; set; }
    public double WriterTemperature { get; set; }
    public double WriterTopP { get; set; }
    
    // Editor properties
    public string EditorRoleName { get; set; }
    public string EditorRoleDescription { get; set; }
    public string EditorInstructions { get; set; }
    public double EditorTemperature { get; set; }
    public double EditorTopP { get; set; }

    // Reviewer properties
    public string ReviewerRoleName { get; set; }
    public string ReviewerRoleDescription { get; set; }
    public string ReviewerInstructions { get; set; }
    public double ReviewerTemperature { get; set; }
    public double ReviewerTopP { get; set; }

    // Common properties
    public string TerminationPrompt { get; set; }
    public int MaxTokens { get; set; }
}
