using aitrailblazer.net.Models;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.TemplateEngine;
using Microsoft.SemanticKernel.PromptTemplates.Handlebars;


#pragma warning disable SKEXP0110

namespace aitrailblazer.net.Services
{
    /// <summary>
    /// Service responsible for executing chat interactions between agents using Microsoft Semantic Kernel.
    /// </summary>
    public class KernelFunctionStrategyService
    {
        private readonly KernelService _kernelService;
        private readonly ILogger<KernelFunctionStrategyService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="KernelFunctionStrategyService"/> class.
        /// </summary>
        /// <param name="kernelService">The service used to create kernels for agent interactions.</param>
        /// <param name="logger">The logger used to log information.</param>
        public KernelFunctionStrategyService(KernelService kernelService, ILogger<KernelFunctionStrategyService> logger)
        {
            _kernelService = kernelService ?? throw new ArgumentNullException(nameof(kernelService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Executes a chat interaction between a writer and a reviewer agent.
        /// </summary>
        /// <param name="input">The initial input provided by the user.</param>
        /// <param name="agentsConfig">The list of agent configurations defining their roles and instructions.</param>
        /// <param name="terminationPrompt">The prompt used to determine when the chat should end.</param>
        /// <param name="maxIterations">The maximum number of iterations (turns) allowed in the chat.</param>
        /// <returns>A string representing all chat messages accumulated during the interaction.</returns>
        public async Task<string> ExecuteChatWriterReviewerAsync(
            string input,
            List<ChatAgentConfig> agentsConfig,
            string terminationPrompt,
            int maxIterations)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return ReturnErrorMessage("Input cannot be null or empty.");
            }

            if (agentsConfig == null || !agentsConfig.Any())
            {
                return ReturnErrorMessage("Agent configurations must be provided.");
            }

            try
            {
                // Create agents using their specific configurations
                var agents = CreateAgents(agentsConfig);
                if (agents == null || !agents.Any())
                {
                    return ReturnErrorMessage("Failed to create agents.");
                }

                // Get the writer and reviewer agents
                var agentWriter = agents.First();
                var reviewerConfig = agentsConfig.FirstOrDefault(c => c.IsReviewer);
                var agentReviewer = agents.LastOrDefault(agent => agent.Name == reviewerConfig?.Name);

                if (agentReviewer == null)
                {
                    return ReturnErrorMessage("Reviewer agent not found.");
                }

                // Define the termination function using the provided prompt
                var terminationFunction = KernelFunctionFactory.CreateFromPrompt(terminationPrompt);

                // Create a chat for agent interaction
                var chat = new AgentGroupChat(agentWriter, agentReviewer)
                {
                    ExecutionSettings = new()
                    {
                        TerminationStrategy = new KernelFunctionTerminationStrategy(terminationFunction, agentWriter.Kernel)
                        {
                            Agents = new List<Agent> { agentReviewer },
                            ResultParser = result => result.GetValue<string>()?.Contains("yes", StringComparison.OrdinalIgnoreCase) ?? false,
                            HistoryVariableName = "history",
                            MaximumIterations = maxIterations
                        }
                    }
                };

                // Invoke chat and capture messages
                var accumulatedMessages = new List<object>();
                chat.AddChatMessage(new ChatMessageContent(AuthorRole.User, input));

                await foreach (var content in chat.InvokeAsync())
                {
                    var message = new
                    {
                        Author = content.AuthorName ?? "*",
                        Content = content.Content
                    };
                    accumulatedMessages.Add(message);
                }

                return SerializeToJson(accumulatedMessages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during the chat interaction.");
                return ReturnErrorMessage("An unexpected error occurred. Please try again later.");
            }
        }

        /// <summary>
        /// Executes a chat interaction between a writer, an editor, and a reviewer agent.
        /// </summary>
        /// <param name="input">The initial input provided by the user.</param>
        /// <param name="agentsConfig">The list of agent configurations defining their roles and instructions.</param>
        /// <param name="terminationPrompt">The prompt used to determine when the chat should end.</param>
        /// <param name="maxIterations">The maximum number of iterations (turns) allowed in the chat.</param>
        /// <returns>A string representing all chat messages accumulated during the interaction.</returns>
        public async Task<string> ExecuteAgentChatWriterEditorReviewerAsync(
            string input,
            List<ChatAgentConfig> agentsConfig,
            string terminationPrompt,
            int maxIterations)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return ReturnErrorMessage("Input cannot be null or empty.");
            }

            if (agentsConfig == null || agentsConfig.Count < 3)
            {
                return ReturnErrorMessage("At least three agent configurations must be provided.");
            }

            try
            {
                // Create agents using their specific configurations
                var agents = CreateAgents(agentsConfig);
                if (agents == null || agents.Count < 3)
                {
                    return ReturnErrorMessage("Failed to create all required agents.");
                }

                // Get the writer, editor, and reviewer agents
                var agentWriter = agents[0];
                var agentEditor = agents[1];
                var reviewerConfig = agentsConfig.FirstOrDefault(c => c.IsReviewer);
                var agentReviewer = agents.LastOrDefault(agent => agent.Name == reviewerConfig?.Name);

                if (agentReviewer == null)
                {
                    return ReturnErrorMessage("Reviewer agent not found.");
                }

                // Define the termination function using the provided prompt
                var terminationFunction = KernelFunctionFactory.CreateFromPrompt(terminationPrompt);

                // Create a chat for agent interaction
                var chat = new AgentGroupChat(agentWriter, agentEditor, agentReviewer)
                {
                    ExecutionSettings = new()
                    {
                        TerminationStrategy = new KernelFunctionTerminationStrategy(terminationFunction, agentWriter.Kernel)
                        {
                            Agents = new List<Agent> { agentReviewer },
                            ResultParser = result => result.GetValue<string>()?.Contains("yes", StringComparison.OrdinalIgnoreCase) ?? false,
                            HistoryVariableName = "history",
                            MaximumIterations = maxIterations
                        }
                    }
                };

                // Invoke chat and capture messages
                var accumulatedMessages = new List<object>();
                chat.AddChatMessage(new ChatMessageContent(AuthorRole.User, input));

                await foreach (var content in chat.InvokeAsync())
                {
                    var message = new
                    {
                        Author = content.AuthorName ?? "*",
                        Content = content.Content
                    };
                    accumulatedMessages.Add(message);
                }

                return SerializeToJson(accumulatedMessages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during the chat interaction.");
                return ReturnErrorMessage("An unexpected error occurred. Please try again later.");
            }
        }

        /// <summary>
        /// Creates a list of chat completion agents based on the provided agent configurations.
        /// </summary>
        /// <param name="agentsConfig">The list of agent configurations defining their roles and instructions.</param>
        /// <returns>A list of chat completion agents.</returns>
        private List<ChatCompletionAgent> CreateAgents(List<ChatAgentConfig> agentsConfig)
        {
            var agents = new List<ChatCompletionAgent>();

            foreach (var config in agentsConfig)
            {
                try
                {
                    var arguments = CreateKernelArguments(config.Temperature, config.TopP, config.MaxTokens);
                    var kernelBuilder = _kernelService.CreateKernelBuilder("gpt-4o", config.MaxTokens);
                    var kernel = kernelBuilder.Build();

                    var agent = new ChatCompletionAgent
                    {
                        Instructions = config.Instructions,
                        Name = config.Name,
                        Arguments = arguments,
                        Kernel = kernel
                    };

                    //agent.Kernel.Plugins.AddFromType<TimeInformation>();
                    agents.Add(agent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create agent: {AgentName}", config.Name);
                }
            }

            _logger.LogInformation("Created {Count} agents", agents.Count);
            return agents;
        }

        /// <summary>
        /// Creates kernel arguments for an agent based on provided settings.
        /// </summary>
        /// <param name="temperature">The temperature setting.</param>
        /// <param name="topP">The top-p setting.</param>
        /// <param name="maxTokens">The maximum number of tokens.</param>
        /// <param name="seed">The seed for deterministic behavior (optional).</param>
        /// <returns>The created kernel arguments.</returns>
        private KernelArguments CreateKernelArguments(double temperature, double topP, int maxTokens, int seed = 356)
        {
            var executionSettings = new OpenAIPromptExecutionSettings
            {
                Temperature = temperature,
                TopP = topP,
                MaxTokens = maxTokens,
                ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
            };

            return new KernelArguments(executionSettings);
        }

        /// <summary>
        /// Serializes an object to a JSON string using Newtonsoft.Json.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <returns>A JSON string representation of the data.</returns>
        private string SerializeToJson(object data)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            return JsonConvert.SerializeObject(data, jsonSettings);
        }

        /// <summary>
        /// Returns a JSON error message using Newtonsoft.Json.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns>A JSON string representing the error.</returns>
        private string ReturnErrorMessage(string message)
        {
            var errorObject = new { Error = message };
            return JsonConvert.SerializeObject(errorObject, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
