using Cosmos.Copilot.Models;
using Microsoft.ML.Tokenizers;
using Microsoft.Extensions.Logging; // Added for logging
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AITrailblazer.net.Services;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;
using Microsoft.Azure.Cosmos;
using System.Diagnostics;

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

        private readonly ILogger<ChatService> _logger; // Logger instance

        public ChatService(
            CosmosDbService cosmosDbService,
            SemanticKernelService semanticKernelService,
            string maxConversationTokens,
            string cacheSimilarityScore,
            string productMaxResults,
            ILogger<ChatService> logger) // Injected logger
        {
            _cosmosDbService = cosmosDbService ?? throw new ArgumentNullException(nameof(cosmosDbService));
            _semanticKernelService = semanticKernelService ?? throw new ArgumentNullException(nameof(semanticKernelService));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            //if (!Int32.TryParse(maxConversationTokens, out _maxConversationTokens))
            //{
            //    _logger.LogWarning("Invalid maxConversationTokens value '{Value}'. Defaulting to 100.", maxConversationTokens);
            //    _maxConversationTokens = 100;
            //}
            _maxConversationTokens = 4096;

            //if (!Double.TryParse(cacheSimilarityScore, out _cacheSimilarityScore))
            //{
            //    _logger.LogWarning("Invalid cacheSimilarityScore value '{Value}'. Defaulting to 0.99.", cacheSimilarityScore);
            //    _cacheSimilarityScore = 0.90;
            //}
            _cacheSimilarityScore = 0.90;
            //if (!Int32.TryParse(productMaxResults, out _productMaxResults))
            //{
            //    _logger.LogWarning("Invalid productMaxResults value '{Value}'. Defaulting to 10.", productMaxResults);
            //    _productMaxResults = 10;
            //}
            _productMaxResults = 10;
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
        /// Retrieves a single message by its unique identifier.
        /// </summary>
        /// <param name="messageId">The unique identifier of the message.</param>
        /// <returns>The Message object if found; otherwise, null.</returns>
        public async Task<Message> GetMessageByIdAsync(string messageId)
        {
            _logger.LogInformation("GetMessageByIdAsync: Retrieving message with ID: {MessageId}", messageId);
            try
            {
                return await _cosmosDbService.GetMessageByIdAsync(messageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve message with ID: {MessageId}", messageId);
                throw;
            }
        }
        /// <summary>
        /// Retrieves chat messages for a specific session.
        /// </summary>
        public async Task<List<Message>> GetChatSessionMessagesAsync(
            string tenantId,
            string userId,
            string? sessionId)
        {
            // Retrieving chat messages for 
            // TenantId=2e58c7ce-9814-4e3d-9e88-467669ba3f5c, UserId=8f22704e-0396-4263-84a7-63310d3f39e7, SessionId=ChatSession-ASAP_Semantic_Caching_vs__Google's_Gemini_Context_Caching__An_Ad-20241107-190609-3570
            // GetChatSessionContextWindow: Fetching context window for 
            // TenantId=2e58c7ce-9814-4e3d-9e88-467669ba3f5c, UserId=8f22704e-0396-4263-84a7-63310d3f39e7, SessionId=ChatSession-ASAP_Semantic_Caching_vs__Google's_Gemini_Context_Caching__An_Ad-20241107-190609-3570
            _logger.LogInformation("Retrieving chat messages for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                var messages = await _cosmosDbService.GetSessionMessagesAsync(
                    tenantId,
                    userId,
                    sessionId);
                _logger.LogInformation("GetChatSessionMessagesAsync Retrieved {Count} messages for SessionId={SessionId}.", messages.Count, sessionId);
                return messages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve chat messages for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
                throw;
            }
        }

        /// <summary>
        /// Creates a new chat session.
        /// </summary>
        /// <param name="tenantId">Tenant identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="title">Title of the session.</param>
        /// <returns>The created SessionChat object.</returns>
        public async Task<SessionChat> CreateNewSessionChatAsync(
            string tenantId,
            string userId,
            string title)
        {
            _logger.LogInformation("CreateNewSessionChatAsync started for TenantId={TenantId}, UserId={UserId}, Title={Title}.", tenantId, userId, title);

            try
            {
                // Create a new SessionChat instance, which generates a unique Id
                var session = new SessionChat(
                    tenantId: tenantId,
                    userId: userId,
                    title: title
                );
                // Ensure that the session timestamp is fully initialized
                session.TimeStamp = DateTime.UtcNow;

                _logger.LogDebug("CreateNewSessionChatAsync SessionChat instance created with Id={sessionId}.", session.SessionId);

                // Insert the new session into Cosmos DB
                await _cosmosDbService.InsertSessionChatAsync(tenantId, userId, session);
                _logger.LogInformation("CreateNewSessionChatAsync: New chat session successfully inserted into Cosmos DB. SessionId={sessionId}, TenantId={TenantId}, UserId={UserId}, Title={Title}.",
                    session.SessionId, tenantId, userId, title);

                return session;
            }
            catch (CosmosException cosmosEx)
            {
                // Handle specific Cosmos DB exceptions if necessary
                _logger.LogError(cosmosEx, "Cosmos DB error while creating new chat session. TenantId={TenantId}, UserId={UserId}, Title={Title}. StatusCode={StatusCode}, Message={Message}.",
                    tenantId, userId, title, cosmosEx.StatusCode, cosmosEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating new chat session. TenantId={TenantId}, UserId={UserId}, Title={Title}.",
                    tenantId, userId, title);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing SessionChat in Cosmos DB.
        /// </summary>
        /// <param name="session">SessionChat object to update.</param>
        /// <param name="tenantId">Tenant identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="title">Title of the session.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateSessionAsync(
            string partitionKey,
            SessionChat session)
        {
            _logger.LogInformation("Updating session with ID: {Id}", session.Id);
            try
            {
                await _cosmosDbService.UpdateSessionAsync(
                    partitionKey,
                    session
                );
                _logger.LogInformation("Updated session with ID: {Id}.", session.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating session with ID: {Id}.", session.Id);
                throw;
            }
        }
        /// <summary>
        /// Renames an existing chat session.
        /// </summary>
        public async Task RenameChatSessionAsync(
            string tenantId,
            string userId,
            string sessionId,
            string newChatSessionName)
        {
            string partitionKey = $"{tenantId}|{userId}|{sessionId}";
            _logger.LogInformation("Renaming chat session SessionId={SessionId} to '{NewName}' for TenantId={TenantId}, UserId={UserId}.", sessionId, newChatSessionName, tenantId, userId);

            try
            {
                // Validate input parameters
                ArgumentNullException.ThrowIfNull(tenantId, nameof(tenantId));
                ArgumentNullException.ThrowIfNull(userId, nameof(userId));
                ArgumentNullException.ThrowIfNull(sessionId, nameof(sessionId));
                ArgumentNullException.ThrowIfNull(newChatSessionName, nameof(newChatSessionName));

                // Retrieve the existing session
                var session = await _cosmosDbService.GetSessionAsync(
                    tenantId,
                    userId,
                    sessionId);

                if (session == null)
                {
                    _logger.LogWarning("Chat session with SessionId={SessionId} not found for TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
                    throw new KeyNotFoundException($"Chat session with SessionId={sessionId} not found.");
                }

                // Use the Rename method to update the session name
                session.Rename(newChatSessionName);

                // Update the session in the database
                await _cosmosDbService.UpdateSessionAsync(
                    partitionKey,
                    session
                );

                _logger.LogInformation("Chat session SessionId={SessionId} renamed to '{NewName}'.", sessionId, newChatSessionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to rename chat session SessionId={SessionId} for TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a specific message within a chat session.
        /// </summary>
        /// <param name="tenantId">Tenant identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="sessionId">Session Chat identifier.</param>
        /// <param name="messageId">Message identifier to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteMessageAsync(string tenantId, string userId, string sessionId, string messageId)
        {
            _logger.LogInformation("Deleting message with ID: {MessageId} in session: {SessionId} for TenantId={TenantId}, UserId={UserId}.", messageId, sessionId, tenantId, userId);
            try
            {
                // Validate input parameters
                ArgumentNullException.ThrowIfNull(tenantId, nameof(tenantId));
                ArgumentNullException.ThrowIfNull(userId, nameof(userId));
                ArgumentNullException.ThrowIfNull(sessionId, nameof(sessionId));
                ArgumentNullException.ThrowIfNull(messageId, nameof(messageId));

                // Call the CosmosDbService to delete the message
                await _cosmosDbService.DeleteMessageAsync(tenantId, userId, sessionId, messageId);

                _logger.LogInformation("Message with ID: {MessageId} deleted successfully in session: {SessionId}.", messageId, sessionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete message with ID: {MessageId} in session: {SessionId} for TenantId={TenantId}, UserId={UserId}.", messageId, sessionId, tenantId, userId);
                throw;
            }
        }


        /// <summary>
        /// Deletes a chat session and its messages.
        /// </summary>
        public async Task DeleteChatSessionAsync(string tenantId, string userId, string sessionId)
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
        /// Retrieves the context window for the current conversation.
        /// </summary>
        /// 
        public async Task<List<Message>> GetChatSessionContextWindow(string tenantId, string userId, string sessionId)
        {
            _logger.LogInformation("GetChatSessionContextWindow: Fetching context window for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(sessionId);

                int tokensUsed = 0;
                var allMessages = await _cosmosDbService.GetSessionMessagesAsync(
                    tenantId,
                    userId,
                    sessionId);
                var contextWindow = new List<Message>();
                _logger.LogInformation("GetChatSessionContextWindow Retrieved {Count} messages for SessionId={SessionId}.", allMessages.Count, sessionId);

                // Start at the end of the list and work backwards
                for (int i = allMessages.Count - 1; i >= 0; i--)
                {
                    var message = allMessages[i];
                    tokensUsed += message.TotalTokenCount;

                    if (tokensUsed > _maxConversationTokens)
                        break;

                    contextWindow.Add(message);
                }

                // Invert the chat messages to put back into chronological order 
                contextWindow.Reverse();
                _logger.LogInformation("GetChatSessionContextWindow: Context window prepared with {Count} messages.", contextWindow.Count);
                return contextWindow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetChatSessionContextWindow: Failed to retrieve context window for TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.", tenantId, userId, sessionId);
                throw;
            }
        }



        public async Task<SessionChat> GetSessionAsync(
           string tenantId,
           string userId,
           string sessionId)
        {
            _logger.LogInformation("Retrieving session with ID: {Id} for TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
            try
            {
                // Call the non-generic GetSessionAsync method from CosmosDbService
                SessionChat session = await _cosmosDbService.GetSessionAsync(tenantId, userId, sessionId);

                if (session == null)
                {
                    _logger.LogWarning("Session with ID {Id} does not exist.", sessionId);
                    return null;
                }

                _logger.LogInformation("Retrieved session with ID: {Id}.", session.Id);
                return session;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving session with ID: {Id} for TenantId={TenantId}, UserId={UserId}.", sessionId, tenantId, userId);
                throw;
            }
        }

        public async Task UpsertSessionAndMessageAsync(
           string tenantId,
           string userId,
           string sessionId,
           Message chatMessage)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var correlationId = Guid.NewGuid();

            _logger.LogDebug("UpsertSessionAndMessageAsync started. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}, MessageId={MessageId}.",
                correlationId, tenantId, userId, sessionId, chatMessage.Id);

            try
            {
                // Validate input parameters
                ArgumentNullException.ThrowIfNull(tenantId, nameof(tenantId));
                ArgumentNullException.ThrowIfNull(userId, nameof(userId));
                ArgumentNullException.ThrowIfNull(sessionId, nameof(sessionId));
                ArgumentNullException.ThrowIfNull(chatMessage, nameof(chatMessage));

                // Retrieve the current session from the database
                _logger.LogDebug("Retrieving session from Cosmos DB. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.",
                    correlationId, tenantId, userId, sessionId);

                var session = await _cosmosDbService.GetSessionAsync(
                    tenantId,
                    userId,
                    sessionId);

                if (session == null)
                {
                    _logger.LogWarning("Session not found. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}.",
                        correlationId, tenantId, userId, sessionId);
                    throw new KeyNotFoundException($"Session with ID {sessionId} does not exist.");
                }

                // Set the session timestamp first
                //session.TimeStamp = DateTime.UtcNow;

                // Set the message timestamp slightly later
                chatMessage.TimeStamp = session.TimeStamp.AddMilliseconds(1);

                // Add the message to the session
                session.AddMessage(chatMessage);

                _logger.LogDebug("Session tokens updated. CorrelationId={CorrelationId}, SessionId={SessionId}, NewTotalTokens={NewTotalTokens}.",
                    correlationId, sessionId, session.TotalTokenCount);

                // Prepare items for batch upsert
                var itemsToUpsert = new object[]
                {
                    session,      // Upsert the updated session
                    chatMessage   // Upsert the new message
                };

                _logger.LogDebug("Performing transactional batch upsert. CorrelationId={CorrelationId}, SessionId={SessionId}, MessageId={MessageId}.",
                    correlationId, sessionId, chatMessage.Id);

                await _cosmosDbService.UpsertSessionBatchAsync(
                    tenantId,
                    userId,
                    sessionId,
                    itemsToUpsert
                );

                _logger.LogInformation("Session and message upserted successfully. CorrelationId={CorrelationId}, SessionId={SessionId}, MessageId={MessageId}, ElapsedTimeMs={ElapsedTimeMs}.",
                    correlationId, sessionId, chatMessage.Id, stopwatch.ElapsedMilliseconds);
            }
            catch (CosmosException cosmosEx)
            {
                stopwatch.Stop();
                _logger.LogError(cosmosEx, "Cosmos DB error while upserting session and message. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}, MessageId={MessageId}, StatusCode={StatusCode}, Message={Message}.",
                    correlationId, tenantId, userId, sessionId, chatMessage.Id, cosmosEx.StatusCode, cosmosEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Unexpected error while upserting session and message. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, SessionId={SessionId}, MessageId={MessageId}, ElapsedTimeMs={ElapsedTimeMs}.",
                    correlationId, tenantId, userId, sessionId, chatMessage.Id, stopwatch.ElapsedMilliseconds);
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogDebug("UpsertSessionAndMessageAsync completed. CorrelationId={CorrelationId}, ElapsedTimeMs={ElapsedTimeMs}.",
                    correlationId, stopwatch.ElapsedMilliseconds);
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

        public async Task<Message> SearchClosestMessageAsync(
           string tenantId,
           string userId,
           string featureNameProject,
           string searchQuery,
           string responseLengthVal,
           string creativeAdjustmentsVal,
           string audienceLevelVal,
           string writingStyleVal,
           string relationSettingsVal,
           string responseStyleVal)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Performing semantic search for the closest message for TenantId={TenantId}, UserId={UserId}, FeatureNameProject={FeatureNameProject}.", tenantId, userId, featureNameProject);

            try
            {
                // Generate embeddings for the search query
                var queryVectors = await _semanticKernelService.GetEmbeddingsAsync(searchQuery);
                _logger.LogInformation("Embeddings generated for the search query.");

                // Define the similarity threshold (adjust as needed)
                double similarityScore = 0.9;

                // Search for the closest message with additional parameters
                var closestMessage = await _cosmosDbService.SearchClosestMessageAsync(
                    tenantId,
                    userId,
                    featureNameProject,
                    queryVectors,
                    similarityScore,
                    responseLengthVal,
                    creativeAdjustmentsVal,
                    audienceLevelVal,
                    writingStyleVal,
                    relationSettingsVal,
                    responseStyleVal);

                if (closestMessage != null)
                {
                    _logger.LogInformation("Found closest message with ID: {MessageId}.", closestMessage.Id);
                }
                else
                {
                    _logger.LogInformation("No similar message found within the threshold.");
                }

                stopwatch.Stop();
                _logger.LogInformation("Semantic search completed in {ElapsedMilliseconds} ms.", stopwatch.ElapsedMilliseconds);

                return closestMessage;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Failed to perform semantic search.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves cached response if available.
        /// </summary>
        public async Task<(string cachePrompts, float[] cacheVectors, string cacheResponse)> GetCacheAsync(List<Message> contextWindow)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("GetCacheAsync: Performing cache search with context window containing {Count} messages.", contextWindow.Count);
            try
            {
                // Grab the user prompts for the context window
                var prompts = string.Join(Environment.NewLine, contextWindow.Select(m => m.Prompt));
                _logger.LogInformation("GetCacheAsync:Aggregated prompts for cache search: {Prompts}.", prompts);

                // Get the embeddings for the user prompts
                var vectors = await _semanticKernelService.GetEmbeddingsAsync(prompts);
                _logger.LogInformation("GetCacheAsync:Embeddings generated for cache search.");

                // Check the cache for similar vectors
                var response = await _cosmosDbService.GetCacheAsync(vectors, _cacheSimilarityScore);
                _logger.LogInformation("GetCacheAsync:Cache search completed. CacheResponse='{CacheResponse}'.", response);

                stopwatch.Stop();
                _logger.LogInformation("GetCacheAsync: Completed in {ElapsedMilliseconds} ms.", stopwatch.ElapsedMilliseconds);

                return (prompts, vectors, response);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Failed to perform cache search. Elapsed time: {ElapsedMilliseconds} ms.", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }
        public async Task<(string cachePrompts, float[] cacheVectors, string cacheResponse)> GetCacheStringAsync(string contextWindow)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("GetCacheAsync: Performing cache search with context window.");

            try
            {
                // Log the context window
                _logger.LogInformation("GetCacheAsync: Context window: {ContextWindow}.", contextWindow);

                // Get the embeddings for the context window
                var vectors = await _semanticKernelService.GetEmbeddingsAsync(contextWindow);
                _logger.LogInformation("GetCacheAsync: Embeddings generated for cache search.");

                // Check the cache for similar vectors
                var response = await _cosmosDbService.GetCacheAsync(vectors, _cacheSimilarityScore);
                _logger.LogInformation("GetCacheAsync: Cache search completed. CacheResponse='{CacheResponse}'.", response);

                stopwatch.Stop();
                _logger.LogInformation("GetCacheAsync: Completed in {ElapsedMilliseconds} ms.", stopwatch.ElapsedMilliseconds);

                return (contextWindow, vectors, response);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Failed to perform cache search. Elapsed time: {ElapsedMilliseconds} ms.", stopwatch.ElapsedMilliseconds);
                throw;
            }
        }

        /// <summary>
        /// Caches the generated completion.
        /// </summary>
        public async Task CachePutAsync(
            string cachePrompts,
            float[] cacheVectors,
            string responseOutput)
        {
            _logger.LogDebug("Caching completion for prompts: '{Prompts}'.", cachePrompts);
            try
            {
                // Include the user prompts text to view. They are not used in the cache search.
                var cacheItem = new CacheItem(cacheVectors, cachePrompts, responseOutput);

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
                var similarEmails = await _cosmosDbService.SearchEmailsAsync(
                    promptVectors,
                    tenantId,
                    userId,
                    categoryId,
                    _emailMaxResults);
                _logger.LogDebug("Retrieved {Count} similar emails for RAG completion.", similarEmails.Count);
                _logger.LogInformation($"SearchEmailsAsync {similarEmails}", similarEmails);

                // Extract the subject of the top email, if available
                string? subject = similarEmails.FirstOrDefault()?.Subject;

                // Create summary for the email
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
                var conversationText = string.Join(" ", messages.Select(m => $"{m.Prompt} {m.Output}"));
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
        /// Sanitizes the prompt text to prevent logging sensitive information.
        /// </summary>
        /// <param name="promptText">The original prompt text.</param>
        /// <returns>A sanitized version of the prompt text.</returns>
        private string SanitizePromptText(string promptText)
        {
            // Implement sanitization logic as needed.
            // For example, truncate the text or remove sensitive keywords.
            // Here, we'll simply truncate to the first 100 characters for logging purposes.
            if (promptText.Length > 100)
            {
                return promptText.Substring(0, 100) + "...";
            }
            return promptText;
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


    }
}
