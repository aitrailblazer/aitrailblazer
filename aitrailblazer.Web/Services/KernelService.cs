namespace aitrailblazer.net.Services
{
#pragma warning disable CS8602 // memory is initialized before usage
#pragma warning disable CS0162 // unreachable code is managed via boolean settings

    using Microsoft.SemanticKernel;

    public class KernelService
    {
        private readonly ParametersAzureService _parametersAzureService;

        public KernelService(ParametersAzureService parametersAzureService)
        {
            _parametersAzureService = parametersAzureService;
        }

        public IKernelBuilder CreateKernelBuilder(string modelId, int maxTokens)
        {
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

            string deploymentName;
            string endpoint;
            string apiKey;
            //string modelId;  gpt-4o-mini gpt-4o
            

            deploymentName = modelId;
            endpoint = _parametersAzureService.AzureOpenAIEndpoint03;
            apiKey = _parametersAzureService.AzureOpenAIKey03;
            modelId = modelId;
            //Console.WriteLine($"Deployment Name: {deploymentName}");
            //Console.WriteLine($"Endpoint: {endpoint}");
            //Console.WriteLine($"API Key: {apiKey}");
            //Console.WriteLine($"Model ID: {modelId}");

            kernelBuilder.AddAzureOpenAIChatCompletion(
                deploymentName: deploymentName,
                endpoint: endpoint,
                apiKey: apiKey,
                modelId: modelId);

            return kernelBuilder;
        }

        // var memory = new MemoryWebClient("http://127.0.0.1:9001"); // <== URL where the web service is running

        //public IKernelMemory GetMemoryClient()
        //{
        //    string endpoint = _parametersAzureService.KernelMemoryServiceEndpoint;
        //    string? apiKey = _parametersAzureService.KernelMemoryServiceApiKey;
        //    return new MemoryWebClient(endpoint, apiKey: apiKey);
        //}

    }
}
