namespace AITrailblazer.net.Services
{
    public class ParametersAzureService
    {


        public required string TenantId { get; set; }
        public required string Authority { get; set; }

        public required string ClientSecret { get; set; }

        public required string ClientId { get; set; }

        //public required string KernelMemoryServiceEndpoint{ get; set; }
        //public required string KernelMemoryServiceApiKey { get; set; }
        public required string StorageAccountName { get; set; }

        public required string StorageConnectionString { get; set; }
        public required string StorageContainerName { get; set; }

        public required string GITHUB_TOKEN { get; set; }

        //public required string GOOGLE_APPLICATION_CREDENTIALS_STR { get; set; }
        //public required string GOOGLE_PROJECT_ID { get; set; }
        //public required string GOOGLE_LOCATION_ID { get; set; }
        //public required string GOOGLE_API_ENDPOINT { get; set; }
        //public required string GOOGLE_MODEL_ID { get; set; }
        //public required string GOOGLE_API_KEY { get; set; }
        public required string AzureOpenAIEndpoint02 { get; set; }
        public required string AzureOpenAIKey02 { get; set; }

        public required string AzureOpenAIEndpoint03 { get; set; }
        public required string AzureOpenAIKey03 { get; set; }
        public required string AzureOpenAIModelName01 { get; set; }

        public required string AzureOpenAIModelName02 { get; set; }

        public required string AzureOpenAIModelName03 { get; set; }
        public required string AzureSearchApiKey { get; set; }
        public required string AzureSearchEndpoint { get; set; }

        public required string AzureOpenAIMaxCompletionTokens { get; set; }
        public required string AzureEmbeddingsModelName03 { get; set; }
        public required int AzureEmbeddingsdDimensions { get; set; }
        public required string PhiEndpoint { get; set; }
        public required string PhiKey { get; set; }

        public required string CodestralEndpoint { get; set; }
        public required string CodestralKey { get; set; }
        public required string CohereCommandREndpoint { get; set; }
        public required string CohereCommandRKey { get; set; }
        public required string MaxConversationTokens { get; set; }
        public required string MaxEmbeddingTokens { get; set; }

        public required string AzureOpenAIDALLEEndpoint01 { get; set; }
        public required string AzureOpenAIDALLEKey01 { get; set; }
        public required string AzureOpenAIDALLEModelName01 { get; set; }

        public required string BingSearchApiKey { get; set; }
        //public required string ApplicationInsightsConnectionString { get; set; }
    }
}