using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq;
using System.Text;
using System;
using System.Web;

using System.Threading.Tasks;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.TemplateEngine;
using Microsoft.SemanticKernel.Plugins.OpenApi;
using Microsoft.SemanticKernel.Plugins.OpenApi;
using Microsoft.SemanticKernel.Plugins.OpenApi.Extensions;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Data;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using System.ComponentModel;

using Microsoft.OpenApi.Extensions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using aitrailblazer.net.Utilities;
using Microsoft.Extensions.Logging;
using Kernel = Microsoft.SemanticKernel.Kernel;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;
using Fluid.Ast;
using System.Text.RegularExpressions;
using System.Diagnostics;
//using CognitiveServices.Sdk.News;
//using CognitiveServices.Sdk.News.Search;
//using CognitiveServices.Sdk.News.Trendingtopics;
//using CognitiveServices.Sdk.Models;
using Newtonsoft.Json;
using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
//using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace AITrailblazer.net.Services
{

    public class SKHandler
    {
        private readonly KernelService _kernelService;
             private readonly ILogger<AzureOpenAIHandler> _logger;


        public SKHandler(
            KernelService kernelService,
            ILogger<AzureOpenAIHandler> logger)
        {

            _kernelService = kernelService;
            _logger = logger;

        }


        private Stopwatch _timer;

        public async Task<string> SKTestAsync(string input)
        {
            int maxTokens = 1024;

            string modelId = "gpt-4o"; // Ensure this is the correct model ID
            IKernelBuilder kernelBuilder = _kernelService.CreateKernelBuilder(modelId, maxTokens);
            Kernel kernel = kernelBuilder.Build();

            // Create a semantic function
            var prompt = "Tell me a joke about {{$input}}";
            var jokeFunction = kernel.CreateFunctionFromPrompt(prompt);

            try
            {
                // Run the function
                var result = await kernel.InvokeAsync(jokeFunction, new() { ["input"] = input });
                var searchResult = result.GetValue<string>();

                _logger.LogInformation($"SKTestAsync Generated search Result: {searchResult}");

                return searchResult;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, $"AzureOpenAIHandler SKTestAsync Argument error: {ex.Message}");
                return "An error occurred with the arguments. Please check your input and try again.";
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"AzureOpenAIHandler SKTestAsync Invalid operation: {ex.Message}");
                return "An invalid operation occurred during function execution. Please try again.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AzureOpenAIHandler SKTestAsync Unexpected error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner Exception Details");
                }
                return "An unexpected error occurred during execution. Please try again.";
            }
        }
    }
 
}