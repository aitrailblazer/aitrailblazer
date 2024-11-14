using Cosmos.Copilot.Models;
using Microsoft.ML.Tokenizers;
using Microsoft.Extensions.Logging; // Added for logging
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        /// Retrieves all chat Threads for a user.
        /// </summary>
        public async Task<List<ThreadChat>> GetAllChatThreadsAsync(string tenantId, string userId)
        {
            _logger.LogInformation("Retrieving all chat Threads for TenantId={TenantId}, UserId={UserId}.", tenantId, userId);
            try
            {
                var Threads = await _cosmosDbService.GetThreadsAsync(tenantId, userId);
                _logger.LogInformation("Retrieved {Count} chat Threads.", Threads.Count);
                return Threads;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve chat Threads for TenantId={TenantId}, UserId={UserId}.", tenantId, userId);
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
        /// Retrieves chat messages for a specific Thread.
        /// </summary>
        public async Task<List<Message>> GetChatThreadMessagesAsync(
            string tenantId,
            string userId,
            string? ThreadId)
        {
             _logger.LogInformation("Retrieving chat messages for TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}.", tenantId, userId, ThreadId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(ThreadId);

                var messages = await _cosmosDbService.GetThreadMessagesAsync(
                    tenantId,
                    userId,
                    ThreadId);
                _logger.LogInformation("GetChatThreadMessagesAsync Retrieved {Count} messages for ThreadId={ThreadId}.", messages.Count, ThreadId);
                return messages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve chat messages for TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}.", tenantId, userId, ThreadId);
                throw;
            }
        }

        /// <summary>
        /// Creates a new chat Thread.
        /// </summary>
        /// <param name="tenantId">Tenant identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="title">Title of the Thread.</param>
        /// <returns>The created ThreadChat object.</returns>
        public async Task<ThreadChat> CreateNewThreadChatAsync(
            string tenantId,
            string userId,
            string title)
        {
            _logger.LogInformation("CreateNewThreadChatAsync started for TenantId={TenantId}, UserId={UserId}, Title={Title}.", tenantId, userId, title);

            try
            {
                // Create a new ThreadChat instance, which generates a unique Id
                var Thread = new ThreadChat(
                    tenantId: tenantId,
                    userId: userId,
                    title: title
                );
                // Ensure that the Thread timestamp is fully initialized
                Thread.TimeStamp = DateTime.UtcNow;

                _logger.LogDebug("CreateNewThreadChatAsync ThreadChat instance created with Id={ThreadId}.", Thread.ThreadId);

                // Insert the new Thread into Cosmos DB
                await _cosmosDbService.InsertThreadChatAsync(tenantId, userId, Thread);
                _logger.LogInformation("CreateNewThreadChatAsync: New chat Thread successfully inserted into Cosmos DB. ThreadId={ThreadId}, TenantId={TenantId}, UserId={UserId}, Title={Title}.",
                    Thread.ThreadId, tenantId, userId, title);

                return Thread;
            }
            catch (CosmosException cosmosEx)
            {
                // Handle specific Cosmos DB exceptions if necessary
                _logger.LogError(cosmosEx, "Cosmos DB error while creating new chat Thread. TenantId={TenantId}, UserId={UserId}, Title={Title}. StatusCode={StatusCode}, Message={Message}.",
                    tenantId, userId, title, cosmosEx.StatusCode, cosmosEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while creating new chat Thread. TenantId={TenantId}, UserId={UserId}, Title={Title}.",
                    tenantId, userId, title);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing ThreadChat in Cosmos DB.
        /// </summary>
        /// <param name="Thread">ThreadChat object to update.</param>
        /// <param name="tenantId">Tenant identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="title">Title of the Thread.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateThreadAsync(
            PartitionKey partitionKey,
            ThreadChat Thread)
        {
            _logger.LogInformation("Updating Thread with ID: {Id}", Thread.Id);
            try
            {
                await _cosmosDbService.UpdateThreadAsync(
                    partitionKey,
                    Thread
                );
                _logger.LogInformation("Updated Thread with ID: {Id}.", Thread.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Thread with ID: {Id}.", Thread.Id);
                throw;
            }
        }
        /// <summary>
        /// Renames an existing chat Thread.
        /// </summary>
        public async Task RenameChatThreadAsync(
            string tenantId,
            string userId,
            string ThreadId,
            string newChatThreadName)
        {
            PartitionKey partitionKey = CosmosDbService.GetPK(tenantId, userId, ThreadId);

            _logger.LogInformation("Renaming chat Thread ThreadId={ThreadId} to '{NewName}' for TenantId={TenantId}, UserId={UserId}.", ThreadId, newChatThreadName, tenantId, userId);

            try
            {
                // Validate input parameters
                ArgumentNullException.ThrowIfNull(tenantId, nameof(tenantId));
                ArgumentNullException.ThrowIfNull(userId, nameof(userId));
                ArgumentNullException.ThrowIfNull(ThreadId, nameof(ThreadId));
                ArgumentNullException.ThrowIfNull(newChatThreadName, nameof(newChatThreadName));

                // Retrieve the existing Thread
                var Thread = await _cosmosDbService.GetThreadAsync(
                    tenantId,
                    userId,
                    ThreadId);

                if (Thread == null)
                {
                    _logger.LogWarning("Chat Thread with ThreadId={ThreadId} not found for TenantId={TenantId}, UserId={UserId}.", ThreadId, tenantId, userId);
                    throw new KeyNotFoundException($"Chat Thread with ThreadId={ThreadId} not found.");
                }

                // Use the Rename method to update the Thread name
                Thread.Rename(newChatThreadName);

                // Update the Thread in the database
                await _cosmosDbService.UpdateThreadAsync(
                    partitionKey,
                    Thread
                );

                _logger.LogInformation("Chat Thread ThreadId={ThreadId} renamed to '{NewName}'.", ThreadId, newChatThreadName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to rename chat Thread ThreadId={ThreadId} for TenantId={TenantId}, UserId={UserId}.", ThreadId, tenantId, userId);
                throw;
            }
        }

        /// <summary>
        /// Deletes a specific message within a chat Thread.
        /// </summary>
        /// <param name="tenantId">Tenant identifier.</param>
        /// <param name="userId">User identifier.</param>
        /// <param name="ThreadId">Thread Chat identifier.</param>
        /// <param name="messageId">Message identifier to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteMessageAsync(string tenantId, string userId, string ThreadId, string messageId)
        {
            _logger.LogInformation("Deleting message with ID: {MessageId} in Thread: {ThreadId} for TenantId={TenantId}, UserId={UserId}.", messageId, ThreadId, tenantId, userId);
            try
            {
                // Validate input parameters
                ArgumentNullException.ThrowIfNull(tenantId, nameof(tenantId));
                ArgumentNullException.ThrowIfNull(userId, nameof(userId));
                ArgumentNullException.ThrowIfNull(ThreadId, nameof(ThreadId));
                ArgumentNullException.ThrowIfNull(messageId, nameof(messageId));

                // Call the CosmosDbService to delete the message
                await _cosmosDbService.DeleteMessageAsync(tenantId, userId, ThreadId, messageId);

                _logger.LogInformation("Message with ID: {MessageId} deleted successfully in Thread: {ThreadId}.", messageId, ThreadId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete message with ID: {MessageId} in Thread: {ThreadId} for TenantId={TenantId}, UserId={UserId}.", messageId, ThreadId, tenantId, userId);
                throw;
            }
        }


        /// <summary>
        /// Deletes a chat Thread and its messages.
        /// </summary>
        public async Task DeleteChatThreadAsync(string tenantId, string userId, string ThreadId)
        {
            _logger.LogInformation("Deleting chat Thread ThreadId={ThreadId} for TenantId={TenantId}, UserId={UserId}.", ThreadId, tenantId, userId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(ThreadId);

                await _cosmosDbService.DeleteThreadAndMessagesAsync(tenantId, userId, ThreadId);
                _logger.LogInformation("Chat Thread ThreadId={ThreadId} and its messages deleted successfully.", ThreadId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete chat Thread ThreadId={ThreadId} for TenantId={TenantId}, UserId={UserId}.", ThreadId, tenantId, userId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves the context window for the current conversation.
        /// </summary>
        /// 
        public async Task<List<Message>> GetChatThreadContextWindow(string tenantId, string userId, string ThreadId)
        {
            _logger.LogInformation("GetChatThreadContextWindow: Fetching context window for TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}.", tenantId, userId, ThreadId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(ThreadId);

                int tokensUsed = 0;
                var allMessages = await _cosmosDbService.GetThreadMessagesAsync(
                    tenantId,
                    userId,
                    ThreadId);
                var contextWindow = new List<Message>();
                _logger.LogInformation("GetChatThreadContextWindow Retrieved {Count} messages for ThreadId={ThreadId}.", allMessages.Count, ThreadId);

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
                _logger.LogInformation("GetChatThreadContextWindow: Context window prepared with {Count} messages.", contextWindow.Count);
                return contextWindow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetChatThreadContextWindow: Failed to retrieve context window for TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}.", tenantId, userId, ThreadId);
                throw;
            }
        }



        public async Task<ThreadChat> GetThreadAsync(
           string tenantId,
           string userId,
           string threadId)
        {
            _logger.LogInformation("Retrieving Thread with ID: {Id} for TenantId={threadId}, UserId={UserId}.", threadId, tenantId, userId);
            try
            {
                // Call the non-generic GetThreadAsync method from CosmosDbService
                ThreadChat Thread = await _cosmosDbService.GetThreadAsync(tenantId, userId, threadId);

                if (Thread == null)
                {
                    _logger.LogWarning("Thread with ID {Id} does not exist.", threadId);
                    return null;
                }

                _logger.LogInformation("Retrieved Thread with ID: {Id}.", Thread.Id);
                return Thread;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Thread with ID: {Id} for TenantId={TenantId}, UserId={UserId}.", threadId, tenantId, userId);
                throw;
            }
        }

        public async Task UpsertThreadAndMessageAsync(
           string tenantId,
           string userId,
           string threadId,
           Message chatMessage)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var correlationId = Guid.NewGuid();

            _logger.LogDebug("UpsertThreadAndMessageAsync started. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}, MessageId={MessageId}.",
                correlationId, tenantId, userId, threadId, chatMessage.Id);

            try
            {
                // Validate input parameters
                ArgumentNullException.ThrowIfNull(tenantId, nameof(tenantId));
                ArgumentNullException.ThrowIfNull(userId, nameof(userId));
                ArgumentNullException.ThrowIfNull(threadId, nameof(threadId));
                ArgumentNullException.ThrowIfNull(chatMessage, nameof(chatMessage));

                // Retrieve the current Thread from the database
                _logger.LogDebug("Retrieving Thread from Cosmos DB. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}.",
                    correlationId, tenantId, userId, threadId);

                var Thread = await _cosmosDbService.GetThreadAsync(
                    tenantId,
                    userId,
                    threadId);

                if (Thread == null)
                {
                    _logger.LogWarning("Thread not found. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}.",
                        correlationId, tenantId, userId, threadId);
                    throw new KeyNotFoundException($"Thread with ID {threadId} does not exist.");
                }

                // Set the Thread timestamp first
                //Thread.TimeStamp = DateTime.UtcNow;

                // Set the message timestamp slightly later
                chatMessage.TimeStamp = Thread.TimeStamp.AddMilliseconds(1);

                // Add the message to the Thread
                Thread.AddMessage(chatMessage);

                _logger.LogDebug("Thread tokens updated. CorrelationId={CorrelationId}, ThreadId={ThreadId}, NewTotalTokens={NewTotalTokens}.",
                    correlationId, threadId, Thread.TotalTokenCount);

                // Prepare items for batch upsert
                var itemsToUpsert = new object[]
                {
                    Thread,      // Upsert the updated Thread
                    chatMessage   // Upsert the new message
                };

                _logger.LogDebug("Performing transactional batch upsert. CorrelationId={CorrelationId}, ThreadId={ThreadId}, MessageId={MessageId}.",
                    correlationId, threadId, chatMessage.Id);

                await _cosmosDbService.UpsertThreadBatchAsync(
                    tenantId,
                    userId,
                    threadId,
                    itemsToUpsert
                );

                _logger.LogInformation("Thread and message upserted successfully. CorrelationId={CorrelationId}, ThreadId={ThreadId}, MessageId={MessageId}, ElapsedTimeMs={ElapsedTimeMs}.",
                    correlationId, threadId, chatMessage.Id, stopwatch.ElapsedMilliseconds);
            }
            catch (CosmosException cosmosEx)
            {
                stopwatch.Stop();
                _logger.LogError(cosmosEx, "Cosmos DB error while upserting Thread and message. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}, MessageId={MessageId}, StatusCode={StatusCode}, Message={Message}.",
                    correlationId, tenantId, userId, threadId, chatMessage.Id, cosmosEx.StatusCode, cosmosEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                _logger.LogError(ex, "Unexpected error while upserting Thread and message. CorrelationId={CorrelationId}, TenantId={TenantId}, UserId={UserId}, ThreadId={ThreadId}, MessageId={MessageId}, ElapsedTimeMs={ElapsedTimeMs}.",
                    correlationId, tenantId, userId, threadId, chatMessage.Id, stopwatch.ElapsedMilliseconds);
                throw;
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogDebug("UpsertThreadAndMessageAsync completed. CorrelationId={CorrelationId}, ElapsedTimeMs={ElapsedTimeMs}.",
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
        /// Summarizes the chat Thread to generate a relevant name.
        /// </summary>
        public async Task<string> SummarizeChatThreadNameAsync(string tenantId, string userId, string? ThreadId)
        {
            _logger.LogInformation("Summarizing chat Thread name for ThreadId={ThreadId}, TenantId={TenantId}, UserId={UserId}.", ThreadId, tenantId, userId);
            try
            {
                ArgumentNullException.ThrowIfNull(tenantId);
                ArgumentNullException.ThrowIfNull(userId);
                ArgumentNullException.ThrowIfNull(ThreadId);

                // Get the messages for the Thread
                var messages = await _cosmosDbService.GetThreadMessagesAsync(tenantId, userId, ThreadId);
                _logger.LogDebug("Retrieved {Count} messages for summarization.", messages.Count);

                // Create a conversation string from the messages
                var conversationText = string.Join(" ", messages.Select(m => $"{m.Prompt} {m.Output}"));
                _logger.LogDebug("Conversation text prepared for summarization.");

                // Send to OpenAI to summarize the conversation
                var completionText = await _semanticKernelService.SummarizeConversationAsync(conversationText);
                _logger.LogInformation("Summarization completed with summary: '{Summary}'.", completionText);

                await RenameChatThreadAsync(tenantId, userId, ThreadId, completionText);
                _logger.LogInformation("Chat Thread renamed based on summarization.");

                return completionText;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to summarize chat Thread name for ThreadId={ThreadId}, TenantId={TenantId}, UserId={UserId}.", ThreadId, tenantId, userId);
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
