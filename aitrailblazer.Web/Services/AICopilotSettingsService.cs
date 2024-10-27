namespace AITrailblazer.net.Services
{

    public class AICopilotSettingsService
    {
        public string? WritingStyleVal { get; set; }
        public string? AudienceLevelVal { get; set; }
        public string? ResponseLengthVal { get; set; } = "1024";
        public string? CreativeAdjustmentsVal { get; set; } = "Focused";
        public string? RelationSettingsVal { get; set; }
        public string? ResponseStyleVal { get; set; }
    }
        
}
