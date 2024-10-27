using Cosmos.Copilot.Models;
using Microsoft.ML.Tokenizers;
using Microsoft.Extensions.Logging; // Added for logging
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AITrailblazer.net.Services;

namespace Cosmos.Copilot.Services
{
    public class ChatService
    {
        private readonly CosmosDbService _cosmosDbService;
        private readonly SemanticKernelService _semanticKernelService;
        private readonly int _maxConversationTokens;
        private readonly double _cacheSimilarityScore;
        private readonly int _productMaxResults;
        private readonly int _emailMaxResults;
        private readonly AzureOpenAIHandler _azureOpenAIHandler; // Injected AzureOpenAIHandler

        private readonly ILogger<ChatService> _logger; // Logger instance

        public ChatService(
            CosmosDbService cosmosDbService,
            SemanticKernelService semanticKernelService,
            AzureOpenAIHandler azureOpenAIHandler,
            string maxConversationTokens,
            string cacheSimilarityScore,
            string productMaxResults,
            ILogger<ChatService> logger) // Injected logger
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
            _semanticKernelService = semanticKernelService ?? throw new ArgumentNullException(nameof(semanticKernelService));
            _azureOpenAIHandler = azureOpenAIHandler ?? throw new ArgumentNullException(nameof(azureOpenAIHandler));
      
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (!Int32.TryParse(maxConversationTokens, out _maxConversationTokens))
            {
                _logger.LogWarning("Invalid maxConversationTokens value '{Value}'. Defaulting to 100.", maxConversationTokens);
                _maxConversationTokens = 100;
            }

            if (!Double.TryParse(cacheSimilarityScore, out _cacheSimilarityScore))
            {
                _logger.LogWarning("Invalid cacheSimilarityScore value '{Value}'. Defaulting to 0.99.", cacheSimilarityScore);
                _cacheSimilarityScore = 0.90;
            }

            if (!Int32.TryParse(productMaxResults, out _productMaxResults))
            {
                _logger.LogWarning("Invalid productMaxResults value '{Value}'. Defaulting to 10.", productMaxResults);
                _productMaxResults = 10;
            }
            _emailMaxResults = 10;

            _logger.LogInformation("ChatService initialized with MaxConversationTokens={MaxConversationTokens}, CacheSimilarityScore={CacheSimilarityScore}, ProductMaxResults={ProductMaxResults}",
                _maxConversationTokens, _cacheSimilarityScore, _productMaxResults);
        }

        /// <summary>
        /// Initializes the ChatService by loading product data.
        /// </summary>
        public async Task InitializeAsync()
        {
            _logger.LogInformation("Initializing ChatService: Loading product data.");
            try
            {
                //await _cosmosDbService.LoadProductDataAsync();
                _logger.LogInformation("Product data loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load product data during initialization.");
                throw;
            }
        }

        /// <summary>
        /// Test method for ChatService.
        /// </summary>
        public async Task TestAsync()
        {
            _logger.LogInformation("Executing TestAsync method.");
            try
            {
                await _cosmosDbService.TestAsync();
                _logger.LogInformation("TestAsync executed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during TestAsync execution.");
                throw;
            }
        }

        public async Task UpsertEmailMessageAsync(EmailMessage email)
        {
            _logger.LogInformation("Upserting email message for TenantId={TenantId}, UserId={UserId}.", email.TenantId, email.UserId);

            try
            {

                // Generate AI Clear Note for the email
                email.KeyPoints = await _azureOpenAIHandler.GenerateAIKeyPointsWizardAsync(email.BodyContentText, email.Subject);
                _logger.LogInformation($"Email summary (KeyPoints) generated.{email.KeyPoints}");

                email.BodyContent = ""; // Clear the body content to save space
                // Serialize EmailMessage to JSON for embedding generation
                var serializedEmail = JsonConvert.SerializeObject(email);

                // Generate embedding vectors from serialized JSON
                var vectors = await _semanticKernelService.GetEmbeddingsAsync(serializedEmail);
                email.Vectors = vectors;

                // Upsert the email message with key points and vectors in Cosmos DB
                await _cosmosDbService.UpsertEmailMessageAsync(
                    email.TenantId,
                    email.UserId,
                    email.CategoryIds,
                    email.Subject,
                    email
                );

                _logger.LogInformation("Email message upserted with Id={EmailId}.", email.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upsert email message for TenantId={TenantId}, UserId={UserId}.", email.TenantId, email.UserId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves all chat sessions for a user.
        /// </summary>
        public async Task<List<SessionChat>> GetAllChatSessionsAsync(string tenantId, string userId)
        {
            _logger.LogInformation("Retrieving all chat sessions for TenantId={TenantId}, UserId={UserId}.", tenantId, userId);
            try
            {
                var sessions = await _cosmosDbService.GetSessionsAsync(tenantId, userId);
                _logger.LogInformation("Retrieved {Count} chat sessions.", sessions.Count);
                return sessions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve chat sessions for TenantId={TenantId}, UserId={UserId}.", tenantId, userId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves chat messages for a specific session.
        /// </summary>
        public async Task<List<Message>> GetChatSessionMessagesAsync(string tenantId, string userId, string? sessionId)
        {
            _logger.LogInformation("Retrieving chat messages for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                var messages = await _cosmosDbService.GetSessionMessagesAsync(tenantId, userId, sessionId);
                _logger.LogInformation("Retrieved {Count} messages for SessionId={SessionId}.", messages.Count, sessionId);
                return messages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve chat messages for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
                throw;
            }
        }

        /// <summary>
        /// Creates a new chat session for a user.
        /// </summary>
        public async Task<SessionChat> CreateNewChatSessionAsync(string tenantId, string userId)
        {
            _logger.LogInformation("Creating new chat session for TenantId={TenantId}, UserId={UserId}.", tenantId, userId);
            try
            {
                var session = new SessionChat(tenantId, userId);
                await _cosmosDbService.InsertSessionAsync(tenantId, userId, session);
                _logger.LogInformation("New chat session created with SessionId={SessionId}.", session.SessionId);
                return session;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create new chat session for TenantId={TenantId}, UserId={UserId}.", tenantId, userId);
                throw;
            }
        }

        /// <summary>
        /// Renames an existing chat session.
        /// </summary>
        public async Task RenameChatSessionAsync(string tenantId, string userId, string? sessionId, string newChatSessionName)
        {
            _logger.LogInformation("Renaming chat session SessionId={SessionId} to '{NewName}' for TenantId={TenantId}, UserId={UserId}.", sessionId, newChatSessionName, tenantId, userId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                var session = await _cosmosDbService.GetSessionAsync(tenantId, userId, sessionId);
                session.Name = newChatSessionName;
                await _cosmosDbService.UpdateSessionAsync(tenantId, userId, session);
                _logger.LogInformation("Chat session SessionId={SessionId} renamed to '{NewName}'.", sessionId, newChatSessionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to rename chat session SessionId={SessionId} for TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a chat session and its messages.
        /// </summary>
        public async Task DeleteChatSessionAsync(string tenantId, string userId, string? sessionId)
        {
            _logger.LogInformation("Deleting chat session SessionId={SessionId} for TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                await _cosmosDbService.DeleteSessionAndMessagesAsync(tenantId, userId, sessionId);
                _logger.LogInformation("Chat session SessionId={SessionId} and its messages deleted successfully.", sessionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete chat session SessionId={SessionId} for TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a chat completion based on user prompt.
        /// </summary>
        public async Task<Message> GetChatCompletionAsync(
            string tenantId, 
            string userId, 
            string? sessionId, 
            string promptText)
        {
            _logger.LogInformation("Generating chat completion for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                // Create a message object for the new User Prompt and calculate the tokens for the prompt
                var chatMessage = await CreateChatMessageAsync(tenantId, userId, sessionId, promptText);
                _logger.LogDebug("Chat message created with MessageId={MessageId}.", chatMessage.Id);

                // Grab context window from the conversation history up to the maximum configured tokens
                var contextWindow = await GetChatSessionContextWindow(tenantId, userId, sessionId);
                _logger.LogDebug("Context window retrieved with {Count} messages.", contextWindow.Count);

                // Perform a cache search to see if this prompt has already been used in the same context window as this conversation
                var (cachePrompts, cacheVectors, cacheResponse) = await GetCacheAsync(contextWindow);
                _logger.LogDebug("Cache search completed. CacheHit={CacheHit}.", !string.IsNullOrEmpty(cacheResponse));

                if (!string.IsNullOrEmpty(cacheResponse))
                {
                    _logger.LogInformation("Cache hit found for the prompt. Using cached completion.");
                    chatMessage.CacheHit = true;
                    chatMessage.Completion = cacheResponse;
                    chatMessage.CompletionTokens = 0;

                    // Persist the prompt/completion, update the session tokens
                    await UpdateSessionAndMessage(tenantId, userId, sessionId, chatMessage);
                    _logger.LogInformation("Chat message persisted with cached completion.");
                    return chatMessage;
                }
                else
                {
                    _logger.LogInformation("Cache miss. Generating new completion using Semantic Kernel.");

                    // Generate embeddings for the user prompt
                    var promptVectors = await _semanticKernelService.GetEmbeddingsAsync(promptText);
                    _logger.LogDebug("Embeddings generated for the prompt.");

                    // Perform RAG (Retrieval-Augmented Generation) pattern completions
                    var products = await _cosmosDbService.SearchProductsAsync(promptVectors, _productMaxResults);
                    _logger.LogDebug("Retrieved {Count} products for RAG completion.", products.Count);

                    (chatMessage.Completion, chatMessage.CompletionTokens) = await _semanticKernelService.GetRagCompletionAsync(
                        sessionId, 
                        contextWindow, 
                        products);
                    _logger.LogInformation("Completion generated using Semantic Kernel.");

                    // Cache the prompts in the current context window and their vectors with the generated completion
                    await CachePutAsync(cachePrompts, cacheVectors, chatMessage.Completion);
                    _logger.LogInformation("Cached the new completion.");
                }

                // Persist the prompt/completion, update the session tokens
                await UpdateSessionAndMessage(tenantId, userId, sessionId, chatMessage);
                _logger.LogInformation("Chat message persisted with new completion.");

                return chatMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating chat completion for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves a completion based on a user prompt for an email context, with optional categoryId and context window.
        /// </summary>
        public async Task<(string completion, string? subject)> GetEmailCompletionAsync(
            string tenantId, 
            string userId, 
            string categoryId, 
            string promptText, 
            List<EmailMessage>? contextWindow = null)
        {
            // Adjusted logging syntax to remove argument issues
            _logger.LogInformation("Generating email completion for TenantId={TenantId}, UserId={UserId}, CategoryId={CategoryId}.", tenantId, userId, categoryId ?? "None");

            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);

                // Initialize contextWindow as an empty list if not provided
                contextWindow ??= new List<EmailMessage>();

                // Generate embeddings for the user prompt
                float[] promptVectors = await _semanticKernelService.GetEmbeddingsAsync(promptText);
                _logger.LogDebug("Embeddings generated for the prompt.");

                // Search for similar email messages using embeddings
                var similarEmails = await _cosmosDbService.SearchEmailsAsync(promptVectors, tenantId, userId, categoryId, _emailMaxResults);
                _logger.LogDebug("Retrieved {Count} similar emails for RAG completion.", similarEmails.Count);
                _logger.LogInformation($"SearchEmailsAsync {similarEmails}",similarEmails);

                // Extract the subject of the top email, if available
                string? subject = similarEmails.FirstOrDefault()?.Subject;

                // Cretae summary for the email
                (string generatedCompletion, int tokens) = await _semanticKernelService.GetRagEmailCompletionAsync(
                    categoryId: categoryId ?? "", // Use "general" if categoryId is null
                    contextWindow: contextWindow, 
                    contextData: similarEmails,
                    useChatHistory: false);

                _logger.LogInformation("Completion generated using Semantic Kernel.");

                return (generatedCompletion, subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while generating email completion for TenantId={TenantId}, UserId={UserId}, CategoryId={CategoryId}.", tenantId, userId, categoryId ?? "None");
                throw;
            }
        }

        /// <summary>
        /// Retrieves the context window for the current conversation.
        /// </summary>
        private async Task<List<Message>> GetChatSessionContextWindow(string tenantId, string userId, string sessionId)
        {
            _logger.LogDebug("Fetching context window for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                int tokensUsed = 0;
                var allMessages = await _cosmosDbService.GetSessionMessagesAsync(tenantId, userId, sessionId);
                var contextWindow = new List<Message>();

                // Start at the end of the list and work backwards
                for (int i = allMessages.Count - 1; i >= 0; i--)
                {
                    tokensUsed += allMessages[i].PromptTokens + allMessages[i].CompletionTokens;

                    if (tokensUsed > _maxConversationTokens)
                        break;

                    contextWindow.Add(allMessages[i]);
                }

                // Invert the chat messages to put back into chronological order 
                contextWindow = contextWindow.Reverse<Message>().ToList();
                _logger.LogDebug("Context window prepared with {Count} messages.", contextWindow.Count);
                return contextWindow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve context window for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
                throw;
            }
        }

        /// <summary>
        /// Summarizes the chat session to generate a relevant name.
        /// </summary>
        public async Task<string> SummarizeChatSessionNameAsync(string tenantId, string userId, string? sessionId)
        {
            _logger.LogInformation("Summarizing chat session name for SessionId={SessionId}, TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                // Get the messages for the session
                var messages = await _cosmosDbService.GetSessionMessagesAsync(tenantId, userId, sessionId);
                _logger.LogDebug("Retrieved {Count} messages for summarization.", messages.Count);

                // Create a conversation string from the messages
                var conversationText = string.Join(" ", messages.Select(m => $"{m.Prompt} {m.Completion}"));
                _logger.LogDebug("Conversation text prepared for summarization.");

                // Send to OpenAI to summarize the conversation
                var completionText = await _semanticKernelService.SummarizeConversationAsync(conversationText);
                _logger.LogInformation("Summarization completed with summary: '{Summary}'.", completionText);

                await RenameChatSessionAsync(tenantId, userId, sessionId, completionText);
                _logger.LogInformation("Chat session renamed based on summarization.");

                return completionText;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to summarize chat session name for SessionId={SessionId}, TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
                throw;
            }
        }

        /// <summary>
        /// Creates a new chat message with the user prompt.
        /// </summary>
        private async Task<Message> CreateChatMessageAsync(string tenantId, string userId, string sessionId, string promptText)
        {
            _logger.LogDebug("Creating chat message for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}, Prompt='{Prompt}'.", tenantId, userId, sessionId, promptText);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                // Calculate tokens for the user prompt message.
                int promptTokens = GetTokens(promptText);
                _logger.LogDebug("Prompt tokens calculated: {PromptTokens}.", promptTokens);

                // Create a new message object.
                var chatMessage = new Message(tenantId, userId, sessionId, promptTokens, promptText, "");

                // Insert the message into Cosmos DB
                await _cosmosDbService.InsertMessageAsync(tenantId, userId, chatMessage);
                _logger.LogInformation("Chat message inserted with MessageId={MessageId}.", chatMessage.Id);

                return chatMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create chat message for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
                throw;
            }
        }

        /// <summary>
        /// Updates the chat session and message in the database.
        /// </summary>
        private async Task UpdateSessionAndMessage(string tenantId, string userId, string sessionId, Message chatMessage)
        {
            _logger.LogDebug("Updating session and message for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}, MessageId={MessageId}.", tenantId, userId, sessionId, chatMessage.Id);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                // Update the tokens used in the session
                var session = await _cosmosDbService.GetSessionAsync(tenantId, userId, sessionId);
                session.Tokens += chatMessage.PromptTokens + chatMessage.CompletionTokens;
                _logger.LogDebug("Session tokens updated. New total tokens: {Tokens}.", session.Tokens);

                // Insert new message and Update session in a transaction
                await _cosmosDbService.UpsertSessionBatchAsync(tenantId, userId, session, chatMessage);
                _logger.LogInformation("Session and message updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update session and message for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}, MessageId={MessageId}.", tenantId, userId, sessionId, chatMessage.Id);
                throw;
            }
        }

        /// <summary>
        /// Calculates the number of tokens in the user prompt.
        /// </summary>
        private int GetTokens(string userPrompt)
        {
            // Placeholder implementation for token counting.
            // Implement actual token counting logic as needed.
            _logger.LogDebug("Calculating tokens for prompt: '{Prompt}'.", userPrompt);
            // Tokenizer _tokenizer = Tokenizer.CreateTiktokenForModel("gpt-3.5-turbo");
            // return _tokenizer.CountTokens(userPrompt);
            return 0;
        }

        /// <summary>
        /// Retrieves cached response if available.
        /// </summary>
        private async Task<(string cachePrompts, float[] cacheVectors, string cacheResponse)> GetCacheAsync(List<Message> contextWindow)
        {
            _logger.LogDebug("Performing cache search with context window containing {Count} messages.", contextWindow.Count);
            try
            {
                // Grab the user prompts for the context window
                var prompts = string.Join(Environment.NewLine, contextWindow.Select(m => m.Prompt));
                _logger.LogDebug("Aggregated prompts for cache search: {Prompts}.", prompts);

                // Get the embeddings for the user prompts
                var vectors = await _semanticKernelService.GetEmbeddingsAsync(prompts);
                _logger.LogDebug("Embeddings generated for cache search.");

                // Check the cache for similar vectors
                var response = await _cosmosDbService.GetCacheAsync(vectors, _cacheSimilarityScore);
                _logger.LogDebug("Cache search completed. CacheResponse='{CacheResponse}'.", response);

                return (prompts, vectors, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to perform cache search.");
                throw;
            }
        }

        /// <summary>
        /// Caches the generated completion.
        /// </summary>
        private async Task CachePutAsync(string cachePrompts, float[] cacheVectors, string generatedCompletion)
        {
            _logger.LogDebug("Caching completion for prompts: '{Prompts}'.", cachePrompts);
            try
            {
                // Include the user prompts text to view. They are not used in the cache search.
                var cacheItem = new CacheItem(cacheVectors, cachePrompts, generatedCompletion);

                // Put the prompts, vectors and completion into the cache
                await _cosmosDbService.CachePutAsync(cacheItem);
                _logger.LogInformation("Completion cached successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to cache the completion.");
                throw;
            }
        }

        /// <summary>
        /// Clears the semantic cache.
        /// </summary>
        public async Task ClearCacheAsync()
        {
            _logger.LogInformation("Clearing the semantic cache.");
            try
            {
                await _cosmosDbService.CacheClearAsync();
                _logger.LogInformation("Semantic cache cleared successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to clear the semantic cache.");
                throw;
            }
        }
    }
}
