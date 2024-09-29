using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using aitrailblazer.net.Services;
using aitrailblazer.net.Models;
using Microsoft.Extensions.Logging;

namespace aitrailblazer.net.Services
{
    /// <summary>
    /// Provides functionality to execute chat sessions between a copywriter, an editor, and an art director.
    /// </summary>
    public class KernelFunctionStrategies
    {
        private readonly KernelFunctionStrategyService _kernelFunctionStrategyService;
        private readonly ILogger<KernelFunctionStrategies> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="KernelFunctionStrategies"/> class.
        /// </summary>
        /// <param name="kernelFunctionStrategyService">Service used to execute the chat interactions.</param>
        /// <param name="logger">Logger instance for logging information and errors.</param>
        public KernelFunctionStrategies(KernelFunctionStrategyService kernelFunctionStrategyService, ILogger<KernelFunctionStrategies> logger)
        {
            _kernelFunctionStrategyService = kernelFunctionStrategyService;
            _logger = logger;
        }

        /// <summary>
        /// Executes a chat session between a copywriter and an art director based on provided inputs.
        /// </summary>
        /// <param name="initialUserInput">The initial concept or input from the user.</param>
        /// <param name="writerSettings">Settings for the copywriter agent.</param>
        /// <param name="reviewerSettings">Settings for the reviewer (art director) agent.</param>
        /// <param name="terminationPrompt">The prompt used to determine when the chat should terminate (optional).</param>
        /// <param name="maxIterations">Maximum number of iterations (turns) allowed in the chat (optional, default is 10).</param>
        /// <returns>A JSON string representing the chat messages or an error message.</returns>
        public async Task<string> ExecuteAgentChatWriterReviewerAsync(
            string initialUserInput,
            ChatAgentSettings writerSettings,
            ChatAgentSettings reviewerSettings,
            string terminationPrompt = null,
            int maxIterations = 10)
        {
            try
            {
                var validationMessage = ValidateInput(initialUserInput);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    return validationMessage;
                }

                terminationPrompt = SetDefaultTerminationPrompt(terminationPrompt);

                var agentsConfig = CreateAgentsConfig(writerSettings, reviewerSettings);

                _logger.LogInformation("Starting chat session between writer and reviewer.");

                string messages = await _kernelFunctionStrategyService.ExecuteChatWriterReviewerAsync(
                    input: initialUserInput,
                    agentsConfig: agentsConfig,
                    terminationPrompt: terminationPrompt,
                    maxIterations: maxIterations);

                _logger.LogInformation("Chat session completed successfully.");

                return messages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the chat session.");
                return "An error occurred during the chat session. Please try again.";
            }
        }

        /// <summary>
        /// Executes a chat session between a copywriter, an editor, and an art director based on provided inputs.
        /// </summary>
        /// <param name="initialUserInput">The initial concept or input from the user.</param>
        /// <param name="writerSettings">Settings for the copywriter agent.</param>
        /// <param name="editorSettings">Settings for the editor agent.</param>
        /// <param name="reviewerSettings">Settings for the reviewer (art director) agent.</param>
        /// <param name="terminationPrompt">The prompt used to determine when the chat should terminate (optional).</param>
        /// <param name="maxIterations">Maximum number of iterations (turns) allowed in the chat (optional, default is 10).</param>
        /// <returns>A JSON string representing the chat messages or an error message.</returns>
        public async Task<string> ExecuteAgentChatWriterEditorReviewerAsync(
            string initialUserInput,
            ChatAgentSettings writerSettings,
            ChatAgentSettings editorSettings,
            ChatAgentSettings reviewerSettings,
            string terminationPrompt = null,
            int maxIterations = 10)
        {
            try
            {
                var validationMessage = ValidateInput(initialUserInput);
                if (!string.IsNullOrEmpty(validationMessage))
                {
                    return validationMessage;
                }

                terminationPrompt = SetDefaultTerminationPrompt(terminationPrompt);

                var agentsConfig = CreateAgentsConfig(writerSettings, editorSettings, reviewerSettings);

                _logger.LogInformation("Starting chat session between writer, editor, and reviewer.");

                string messages = await _kernelFunctionStrategyService.ExecuteAgentChatWriterEditorReviewerAsync(
                    input: initialUserInput,
                    agentsConfig: agentsConfig,
                    terminationPrompt: terminationPrompt,
                    maxIterations: maxIterations);

                _logger.LogInformation("Chat session completed successfully.");

                return messages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the chat session.");
                return "An error occurred during the chat session. Please try again.";
            }
        }

        /// <summary>
        /// Validates the initial user input.
        /// </summary>
        /// <param name="input">The initial user input.</param>
        /// <returns>A message indicating validation result, or null if valid.</returns>
        private static string ValidateInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "Initial user input cannot be empty.";
            }
            return null;
        }

        /// <summary>
        /// Sets the default termination prompt if not provided.
        /// </summary>
        /// <param name="terminationPrompt">The termination prompt.</param>
        /// <returns>The default or provided termination prompt.</returns>
        private static string SetDefaultTerminationPrompt(string terminationPrompt)
        {
            return terminationPrompt ?? """
                Determine if the copy has been approved. If so, respond with a single word: yes

                History:
                {{$history}}
                """;
        }

        /// <summary>
        /// Creates the agents configuration based on the provided settings.
        /// </summary>
        /// <param name="settings">An array of ChatAgentSettings for the agents.</param>
        /// <returns>A list of ChatAgentConfig objects.</returns>
        private static List<ChatAgentConfig> CreateAgentsConfig(params ChatAgentSettings[] settings)
        {
            var agentsConfig = new List<ChatAgentConfig>();

            for (int i = 0; i < settings.Length; i++)
            {
                var setting = settings[i];
                agentsConfig.Add(new ChatAgentConfig
                {
                    Name = setting.Name,
                    Instructions = setting.Instructions,
                    IsReviewer = (i == settings.Length - 1), // The last setting is assumed to be the reviewer
                    Temperature = setting.Temperature,
                    TopP = setting.TopP,
                    MaxTokens = setting.MaxTokens
                });
            }

            return agentsConfig;
        }

    }
}
