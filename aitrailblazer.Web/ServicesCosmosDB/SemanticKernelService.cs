using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Cosmos.Copilot.Models;
using Microsoft.SemanticKernel.Embeddings;
using Azure.AI.OpenAI;
using Azure.Core;
using Azure.Identity;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

using AITrailblazer.net.Services;

namespace Cosmos.Copilot.Services;

/// <summary>
/// Semantic Kernel implementation for Azure OpenAI.
/// </summary>
public class SemanticKernelService
{
    //Semantic Kernel
    readonly Kernel kernel;


    /// <summary>
    /// System prompt to send with user prompts as a Retail AI Assistant for chat session
    /// </summary>
    private readonly string _systemPromptRetailAssistant = @"
        You are an intelligent assistant for the Cosmic Works Bike Company. 
        You are designed to provide helpful answers to user questions about 
        bike products and accessories provided in JSON format below.

        Instructions:
        - Only answer questions related to the information provided below,
        - Don't reference any product data not provided below.
        - If you're unsure of an answer, you can say ""I don't know"" or ""I'm not sure"" and recommend users search themselves.

        Text of relevant information:";


    /// <summary>
    /// System prompt to guide the model as an email assistant with specific output formatting
    /// </summary>
private readonly string _systemPromptEmail = @"
You are an intelligent assistant designed to analyze and summarize email content. 
Use the provided email context below to generate an accurate and concise summary. Follow these instructions:

- Extract key details such as the subject, main points (key takeaways or actionable items), and a web link if available.
- Do not include unrelated information or make assumptions beyond the context provided.
- If no relevant information is available, respond with: ""I could not find sufficient information in the email context.""

Format the response clearly as follows:
- **Subject**: {Subject}
- **Key Points**: {A brief summary of the main points or actionable items from the email}

Email context is provided below:
";


    /// <summary>
    /// System prompt to guide the model as a knowledge base assistant with specific context and formatting.
    /// </summary>
    private readonly string _systemPromptKnowledgeBase = @"
You are an intelligent assistant designed to extract relevant and concise information from a knowledge base context.
Use the provided knowledge base context below to answer accurately and concisely. Follow these instructions:

- Extract key details such as title, description, and reference link.
- Do not include unrelated information or make assumptions beyond the context provided.
- If no relevant answer exists, respond with: ""I could not find an answer in the knowledge base.""

Format the response clearly as follows:
- **Title**: {Title}
- **Content Summary**: {A summary of the content, including key points or highlights}

Knowledge base context is provided below:
";


    /// <summary>    
    /// System prompt to send with user prompts to instruct the model for summarization
    /// </summary>
    private readonly string _summarizePrompt = @"
        Summarize this text. One to three words maximum length. 
        Plain text only. No punctuation, markup or tags.";

    /// <summary>
    /// Creates a new instance of the Semantic Kernel.
    /// </summary>
    /// <param name="endpoint">Endpoint URI.</param>
    /// <param name="completionDeploymentName">Name of the deployed Azure OpenAI completion model.</param>
    /// <param name="embeddingDeploymentName">Name of the deployed Azure OpenAI embedding model.</param>
    /// <exception cref="ArgumentNullException">Thrown when endpoint, key, or modelName is either null or empty.</exception>
    /// <remarks>
    /// This constructor will validate credentials and create a Semantic Kernel instance.
    /// </remarks>
    private readonly ILogger<SemanticKernelService> _logger;

    public SemanticKernelService(
        string endpoint,
        string completionDeploymentName,
        string embeddingDeploymentName,
        string apiKey,
        int dimensions,
        ILogger<SemanticKernelService> logger) // Add logger parameter
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger)); // Initialize logger

        kernel = Kernel.CreateBuilder()
            .AddAzureOpenAIChatCompletion(deploymentName: completionDeploymentName, endpoint: endpoint, apiKey: apiKey)
            .AddAzureOpenAITextEmbeddingGeneration(
                deploymentName: embeddingDeploymentName, 
                endpoint: endpoint, 
                apiKey: apiKey,
                dimensions: dimensions)
            .Build();
    }



    /// <summary>
    /// Generates a completion using a user prompt with chat history and vector search results to Semantic Kernel and returns the response.
    /// </summary>
    /// <param name="sessionId">Chat session identifier for the current conversation.</param>
    /// <param name="contextWindow">List of Message objects containing the context window (chat history) to send to the model.</param>
    /// <param name="products">List of Product objects containing vector search results to send to the model.</param>
    /// <returns>Generated response along with tokens used to generate it.</returns>
    public async Task<(string completion, int tokens)> GetRagCompletionAsync(
        string sessionId,
        List<Message> contextWindow,
        List<Product> products)
    {
        //Serialize List<Product> to a JSON string to send to OpenAI
        string productsString = JsonConvert.SerializeObject(products);

        var skChatHistory = new ChatHistory();
        skChatHistory.AddSystemMessage(_systemPromptRetailAssistant + productsString);


        foreach (var message in contextWindow)
        {
            skChatHistory.AddUserMessage(message.Prompt);
            if (message.Output != string.Empty)
                skChatHistory.AddAssistantMessage(message.Output);
        }

        PromptExecutionSettings settings = new()
        {
            ExtensionData = new Dictionary<string, object>()
            {
                { "Temperature", 0.2 },
                { "TopP", 0.7 },
                { "MaxTokens", 1000  }
            }
        };


        var result = await kernel.GetRequiredService<IChatCompletionService>().GetChatMessageContentAsync(skChatHistory, settings);

        //CompletionsUsage completionUsage = (CompletionsUsage)result.Metadata!["Usage"]!;

        //string completion = result.Items[0].ToString()!;
        //int tokens = completionUsage.CompletionTokens;

        var usage = result.Metadata?["Usage"];
        //var promptTokens = usage?.InputTokens ?? 0;
        var completionTokens = 0;// = usage?.OutputTokens ?? 0;

        //CompletionsUsage completionUsage = (CompletionsUsage)result.Metadata!["Usage"]!;

        string completion = result.Items[0].ToString()!;
        //int tokens = completionUsage.CompletionTokens;

        return (completion, completionTokens);
    }

    /// <summary>
    /// Generates a completion using a user prompt with optional chat history and context data, returning the response.
    /// </summary>
    /// <typeparam name="T">The type of contextual data (e.g., EmailMessage, Product).</typeparam>
    /// <param name="sessionId">Chat session identifier for the current conversation.</param>
    /// <param name="contextWindow">List of Message objects containing the context window (chat history) to send to the model.</param>
    /// <param name="contextData">List of contextual data objects (e.g., EmailMessage, Product) to provide model context.</param>
    /// <param name="useChatHistory">Flag to enable or disable including chat history.</param>
    /// <returns>Generated response along with tokens used to generate it.</returns>
    public async Task<(string completion, int tokens)> GetRagEmailCompletionAsync(
        string categoryId,
        List<EmailMessage> contextWindow,
        EmailMessage contextData,
        bool useChatHistory)
    {
        _logger.LogInformation("Generating email completion for categoryId={CategoryId}.", categoryId);

        try
        {
            // Initialize the chat history with structured context data
            var skChatHistory = new ChatHistory();

            // Create a structured context for the model
            string structuredContext = $"""
                "Subject": "{contextData.Subject}",
                "BodyContentText": "{contextData.BodyContentText}",
                "From": "{contextData.From?.Name ?? "Unknown"}",
                "To": "{string.Join(", ", contextData.ToRecipients.Select(r => r.Name))}",
                "ReceivedDateTime": "{contextData.ReceivedDateTime}",
                "WebLink": "{contextData.WebLink}"
            """;

            _logger.LogInformation("Structured context for email completion: {StructuredContext}", structuredContext);

            skChatHistory.AddSystemMessage($"{_systemPromptEmail}{structuredContext}");

            // Add chat history if enabled
            if (useChatHistory)
            {
                foreach (var email in contextWindow)
                {
                    skChatHistory.AddUserMessage($"Subject: {email.Subject}");
                    skChatHistory.AddAssistantMessage(email.BodyContentText);
                }
            }

            // Define execution settings
            PromptExecutionSettings settings = new()
            {
                ExtensionData = new Dictionary<string, object>()
                {
                    { "Temperature", 0.2 },
                    { "TopP", 0.7 },
                    { "MaxTokens", 1000 }
                }
            };

            // Generate the response using the configured kernel service
            var response = await kernel.GetRequiredService<IChatCompletionService>()
                                        .GetChatMessageContentAsync(skChatHistory, settings);

            string completion = response.Items[0].ToString()!;

            // Extract usage metrics
            var usage = response.Metadata?["Usage"];
            int completionTokens = 0;//usage != null ? Convert.ToInt32(usage["CompletionTokens"]) : 0;

            // Format the reference link
            //string formattedReference = $"\n\nReference: [{contextData.Subject}]({contextData.WebLink})";
            //completion += formattedReference;

            _logger.LogInformation("Generated response successfully with {Tokens} tokens.", completionTokens);

            return (completion, completionTokens);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating email completion.");
            throw;
        }
    }

    public async Task<(string generatedCompletion, int tokens)> GetRagKnowledgeBaseCompletionAsync<T>(
        string categoryId,
        List<KnowledgeBaseItem> contextWindow,
        KnowledgeBaseItem contextData,
        bool useChatHistory)
    {
        // Initialize the chat history with structured context data
        var skChatHistory = new ChatHistory();

        // Create a structured context for the model (exclude ReferenceDescription and ReferenceLink)
        string structuredContext = $"""
            "Title": "{contextData.Title}",
            "Content": "{contextData.Content}"
        """;

        _logger.LogInformation($"GetRagKnowledgeBaseCompletionAsync structuredContext: {structuredContext}");

        skChatHistory.AddSystemMessage($"{_systemPromptKnowledgeBase}{structuredContext}");

        // Define execution settings
        PromptExecutionSettings settings = new()
        {
            ExtensionData = new Dictionary<string, object>()
            {
                { "Temperature", 0.3 },
                { "TopP", 0.8 },
                { "MaxTokens", 1000 }
            }
        };

        try
        {
            // Generate the response using the configured kernel service
            var response = await kernel.GetRequiredService<IChatCompletionService>()
                                    .GetChatMessageContentAsync(skChatHistory, settings);

            string completion = response.Items[0].ToString()!;

            // Extract usage metrics if available
            var usage = response.Metadata?["Usage"];
            int completionTokens = 0; // usage != null ? Convert.ToInt32(usage["CompletionTokens"]) : 0;

            // Extract the page number if available
            string pageNumber = "";
            if (contextData.ReferenceLink.Contains("#page="))
            {
                var match = Regex.Match(contextData.ReferenceLink, @"#page=(\d+)");
                if (match.Success)
                {
                    pageNumber = $" (Page {match.Groups[1].Value})";
                }
            }

            // Append the reference link and page number to the completion
            string formattedReference = $"\n\nReference: [{contextData.ReferenceDescription}]({contextData.ReferenceLink}){pageNumber}";
            //"referenceLink": "2e58c7ce-9814-4e3d-9e88-467669ba3f5c/8f22704e-0396-4263-84a7-63310d3f39e7/Documents/Default/semantickernel.pdf#page=13",
            _logger.LogInformation("GetRagKnowledgeBaseCompletionAsync formattedReference ", formattedReference);

            completion += formattedReference;

            _logger.LogInformation("Generated response successfully with {Tokens} tokens.", completionTokens);

            return (completion, completionTokens);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating knowledge base completion.");
            throw;
        }
    }

    /// <summary>
    /// Generates embeddings from the deployed OpenAI embeddings model using Semantic Kernel.
    /// </summary>
    /// <param name="input">Text to send to OpenAI.</param>
    /// <returns>Array of vectors from the OpenAI embedding model deployment.</returns>
    public async Task<float[]> GetEmbeddingsAsync(string text)
    {
        var embeddings = await kernel.GetRequiredService<ITextEmbeddingGenerationService>().GenerateEmbeddingAsync(text);

        float[] embeddingsArray = embeddings.ToArray();

        return embeddingsArray;
    }

    /// <summary>
    /// Sends the existing conversation to the Semantic Kernel and returns a two word summary.
    /// </summary>
    /// <param name="sessionId">Chat session identifier for the current conversation.</param>
    /// <param name="conversationText">conversation history to send to Semantic Kernel.</param>
    /// <returns>Summarization response from the OpenAI completion model deployment.</returns>
    public async Task<string> SummarizeConversationAsync(string conversation)
    {
        //return await summarizePlugin.SummarizeConversationAsync(conversation, kernel);

        var skChatHistory = new ChatHistory();
        skChatHistory.AddSystemMessage(_summarizePrompt);
        skChatHistory.AddUserMessage(conversation);

        PromptExecutionSettings settings = new()
        {
            ExtensionData = new Dictionary<string, object>()
            {
                { "Temperature", 0.0 },
                { "TopP", 1.0 },
                { "MaxTokens", 100 }
            }
        };


        var result = await kernel.GetRequiredService<IChatCompletionService>().GetChatMessageContentAsync(skChatHistory, settings);

        string completion = result.Items[0].ToString()!;

        return completion;
    }
}
