namespace AITrailblazer.net.Models
{
    public class ChatCompletionParameters
    {
        public string CurrentSessionId { get; set; } = string.Empty;
        public string CurrentUserIdentityID { get; set; } = string.Empty;
        public string CollectionName { get; set; } = string.Empty;
        public string FeatureFriendlyNameCurrent { get; set; } = string.Empty;
        public string FeatureNameProject { get; set; } = string.Empty;
        public string FeatureNameTemp { get; set; } = string.Empty;
        public string FeatureName1 { get; set; } = string.Empty;
        public string FeatureName2 { get; set; } = string.Empty;
        public string FeatureName3 { get; set; } = string.Empty;
        public string UserInput { get; set; } = string.Empty;
        public string ResponseLengthVal { get; set; } = string.Empty;
        public string CreativeAdjustmentsVal { get; set; } = string.Empty;
        public float ValueTemperature { get; set; }
        public float TopP { get; set; }
        public string TopicType { get; set; } = string.Empty;
        public string BackdropTypeDescription { get; set; } = string.Empty;
        public string PhotostyleTypeDescription { get; set; } = string.Empty;
        public string PhotoshotTypeDescription { get; set; } = string.Empty;
        public string LightingTypeDescription { get; set; } = string.Empty;
        public string WritingStyleVal { get; set; } = string.Empty;
        public string AudienceLevelVal { get; set; } = string.Empty;
        public string RelationSettingsVal { get; set; } = string.Empty;
        public string ResponseStyleVal { get; set; } = string.Empty;
        public string PluginDir { get; set; } = string.Empty;
        public string CallFunctionTemp { get; set; } = string.Empty;

        public string MasterTextSetting { get; set; } = string.Empty;
        public string ChatSetting { get; set; } = string.Empty;
        public bool WebSearchUse { get; set; }
        public bool UseVectorSearch { get; set; }
        public string ImageBase64 { get; set; } = string.Empty;
    }


    public class ChatCompletionSettingsRepository
    {
        private readonly Dictionary<string, ChatCompletionParameters> _settings;

        public ChatCompletionSettingsRepository()
        {
            _settings = new Dictionary<string, ChatCompletionParameters>
            {
                ["BasicConfigurationFocused"] = new ChatCompletionParameters
                {
                    CurrentSessionId = string.Empty,
                    CurrentUserIdentityID = string.Empty,
                    CollectionName = string.Empty,

                    FeatureFriendlyNameCurrent = "AIClearNote",
                    FeatureNameProject = "AIClearNote",
                    FeatureNameTemp = "AIClearNote",

                    FeatureName1 = "",
                    FeatureName2 = "",
                    FeatureName3 = "",
                    UserInput = "",
                    TopicType = "",

                    BackdropTypeDescription = "",
                    PhotostyleTypeDescription = "",
                    PhotoshotTypeDescription = "",
                    LightingTypeDescription = "",

                    ResponseLengthVal = "Medium",
                    CreativeAdjustmentsVal = "Focused",
                    ValueTemperature = 0.1F,
                    TopP = 0.5F,
                    WritingStyleVal = "Technical",
                    AudienceLevelVal = "Intermediate",
                    RelationSettingsVal = "Downward",
                    ResponseStyleVal = "",
                    CallFunctionTemp = "AIClearNote",
                    MasterTextSetting = "Content",
                    ChatSetting = "OneShot",
                    WebSearchUse = false,
                    UseVectorSearch = false,
                    ImageBase64 = ""

                }
            };
        }
        public ChatCompletionParameters GetSettings(string name) => _settings.ContainsKey(name) ? _settings[name] : null;
    }

}