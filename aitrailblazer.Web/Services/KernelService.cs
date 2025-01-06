
namespace AITrailblazer.net.Services
{
#pragma warning disable CS8602 // memory is initialized before usage
#pragma warning disable CS0162 // unreachable code is managed via boolean settings

    using Microsoft.SemanticKernel;
    using Microsoft.SemanticKernel.Connectors.AzureAIInference;
    using Microsoft.TypeChat;



    public class KernelService
    {
        private readonly ParametersAzureService _parametersAzureService;

        public KernelService(ParametersAzureService parametersAzureService)
        {
            _parametersAzureService = parametersAzureService;
        }

        public OpenAIConfig CreateOpenAIConfig(string modelId)
        {

            OpenAIConfig config = new OpenAIConfig();

            config.Azure = true;
            config.Endpoint = _parametersAzureService.AzureOpenAIEndpoint03;
            config.ApiKey = _parametersAzureService.AzureOpenAIKey03;
            config.Model = modelId;

            return config;

        }
        public IKernelBuilder CreateKernelBuilderPhi(string modelId, int maxTokens)
        {
            string endpointPhi = _parametersAzureService.PhiEndpoint;
            string apiKeyPhi = _parametersAzureService.PhiKey;
            //string modelId = "phi-3-5-moe-instruct";
            // Create HttpClient with custom headers and timeout
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("My-Custom-Header", "My Custom Value");
            httpClient.Timeout = TimeSpan.FromSeconds(300);  // Set NetworkTimeout to 30 seconds

            IKernelBuilder kernelPhi = Kernel.CreateBuilder()
            .AddAzureAIInferenceChatCompletion(
                endpoint: new Uri(endpointPhi),
                apiKey: apiKeyPhi,
                modelId: modelId,
                httpClient: httpClient);
            return kernelPhi;

        }
        public IKernelBuilder CreateKernelBuilderCohere(string modelId, int maxTokens)
        {
            string endpoint = _parametersAzureService.CohereCommandREndpoint;
            string apiKey = _parametersAzureService.CohereCommandRKey;

            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("My-Custom-Header", "My Custom Value");
            httpClient.Timeout = TimeSpan.FromSeconds(300);  // Set NetworkTimeout to 30 seconds

            IKernelBuilder kernel = Kernel.CreateBuilder()
            .AddAzureAIInferenceChatCompletion(
                endpoint: new Uri(endpoint),
                apiKey: apiKey,
                modelId: modelId,
                httpClient: httpClient);
            return kernel;

        }

        public IKernelBuilder CreateKernelBuilder(string modelId, int maxTokens)
        {
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

            string deploymentName;
            string apiKey;
            string endpoint;
            int embeddingsdDimensions;

            //string modelId;  gpt-4o-mini gpt-4o


            deploymentName = modelId;
            // Convert the string endpoint to a Uri
            Uri endpointUri = new Uri(_parametersAzureService.AzureOpenAIEndpoint03);
            endpoint = _parametersAzureService.AzureOpenAIEndpoint03;

            apiKey = _parametersAzureService.AzureOpenAIKey03;
            embeddingsdDimensions = _parametersAzureService.AzureEmbeddingsdDimensions;

            modelId = modelId;
            //Console.WriteLine($"Deployment Name: {deploymentName}");
            //Console.WriteLine($"Endpoint: {endpoint}");
            // https://aitrailblazereastus2.openai.azure.com/openai/deployments/gpt-4o-mini/chat/completions?api-version=2024-08-01-preview
            //Console.WriteLine($"endpointUri: {endpointUri}");

            //Console.WriteLine($"API Key: {apiKey}");
            //Console.WriteLine($"Model ID: {modelId}");

            // Create HttpClient with custom headers and timeout
            var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("My-Custom-Header", "My Custom Value");
            httpClient.Timeout = TimeSpan.FromSeconds(300);  // Set NetworkTimeout to 30 seconds


            kernelBuilder.AddAzureOpenAIChatCompletion(
                deploymentName: deploymentName,
                endpoint: endpoint,
                apiKey: apiKey,
                modelId: modelId, // Optional name of the underlying model if the deployment name doesn't match the model name
                                  //serviceId: "YOUR_SERVICE_ID", // Optional; for targeting specific services within Semantic Kernel
                httpClient: httpClient // Optional; if not provided, the HttpClient from the kernel will be used
                );
            kernelBuilder.AddAzureOpenAITextEmbeddingGeneration(
                deploymentName: "text-embedding-3-large",
                endpoint: endpoint,
                apiKey: apiKey,
                modelId: "text-embedding-3-large", // Optional name of the underlying model if the deployment name doesn't match the model name
                                                   //serviceId: "YOUR_SERVICE_ID", // Optional; for targeting specific services within Semantic Kernel
                dimensions: embeddingsdDimensions,
                httpClient: httpClient // Optional; if not provided, the HttpClient from the kernel will be used
                );

            //kernelBuilder.AddAzureAIInferenceChatCompletion(
            //    endpoint: endpointUri,
            //    apiKey: apiKey,
            //    modelId: modelId,
            //    serviceId: "AzureOpenAIChat",
            //    httpClient: new HttpClient());


            return kernelBuilder;
        }

    }
}
