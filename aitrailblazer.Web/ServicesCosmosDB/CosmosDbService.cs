using Azure.Core;
using Azure.Identity;
using Cosmos.Copilot.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos.Serialization.HybridRow.Schemas;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
using Container = Microsoft.Azure.Cosmos.Container;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;
using Microsoft.Azure.Cosmos.Linq;

namespace Cosmos.Copilot.Services;

/// <summary>
/// Service to access Azure Cosmos DB for NoSQL.
/// </summary>
public class CosmosDbService
{
    private readonly Container _chatContainer;
    private readonly Container _cacheContainer;
    private readonly Container _organizerContainer;

    private readonly Container _productContainer;
    private readonly string _productDataSourceURI;
    private readonly ILogger<CosmosDbService> _logger;

    /// <summary>
    /// Creates a new instance of the service.
    /// </summary>
    /// <param name="endpoint">Endpoint URI.</param>
    /// <param name="databaseName">Name of the database to access.</param>
    /// <param name="chatContainerName">Name of the chat container to access.</param>
    /// <param name="cacheContainerName">Name of the cache container to access.</param>
    /// <param name="productContainerName">Name of the product container to access.</param>
    /// <param name="productDataSourceURI">URI to the product data source.</param>
    /// <param name="logger">Logger instance for logging.</param>
    /// <exception cref="ArgumentNullException">Thrown when any required parameter is null or empty.</exception>
    /// <remarks>
    /// This constructor will validate credentials and create a service client instance.
    /// </remarks>
    public CosmosDbService(
        string endpoint,
        string databaseName,
        string chatContainerName,
        string cacheContainerName,
        string organizerContainerName,
        string productContainerName,
        string productDataSourceURI,
        ILogger<CosmosDbService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogInformation("Initializing CosmosDbService.");

        ArgumentNullException.ThrowIfNullOrEmpty(endpoint, nameof(endpoint));
        ArgumentNullException.ThrowIfNullOrEmpty(databaseName, nameof(databaseName));
        ArgumentNullException.ThrowIfNullOrEmpty(chatContainerName, nameof(chatContainerName));
        ArgumentNullException.ThrowIfNullOrEmpty(cacheContainerName, nameof(cacheContainerName));
        ArgumentNullException.ThrowIfNullOrEmpty(organizerContainerName);
        ArgumentNullException.ThrowIfNullOrEmpty(productContainerName, nameof(productContainerName));
        ArgumentNullException.ThrowIfNullOrEmpty(productDataSourceURI, nameof(productDataSourceURI));

        _productDataSourceURI = productDataSourceURI;

        CosmosSerializationOptions options = new()
        {
            PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        };

        try
        {
            _logger.LogInformation("Creating CosmosClient with endpoint: {Endpoint}", endpoint);
            TokenCredential credential = new DefaultAzureCredential();
            CosmosClient client = new CosmosClientBuilder(endpoint, credential)
                .WithSerializerOptions(options)
                .Build();

            _logger.LogInformation("Retrieving database: {DatabaseName}", databaseName);
            Database database = client.GetDatabase(databaseName) ?? throw new ArgumentException("Database not found.");

            _logger.LogInformation("Retrieving containers: {ChatContainer}, {CacheContainer}, {OrganizerContainer}, {ProductContainer}", chatContainerName, cacheContainerName, organizerContainerName, productContainerName);
            _chatContainer = database.GetContainer(chatContainerName) ?? throw new ArgumentException("Chat container not found.");
            _cacheContainer = database.GetContainer(cacheContainerName) ?? throw new ArgumentException("Cache container not found.");
            _organizerContainer = database.GetContainer(organizerContainerName) ?? throw new ArgumentException("Cache container not found.");
            _productContainer = database.GetContainer(productContainerName) ?? throw new ArgumentException("Product container not found.");

            _logger.LogInformation("CosmosDbService initialized successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error initializing CosmosDbService.");
            throw;
        }
    }
    /// <summary>
    /// Helper function to generate a hierarchical partition key based on tenantId, userId, and categoryId.
    /// All parameters are required and will be included in the partition key, even if they are empty strings.
    /// </summary>
    /// <param name="tenantId">Id of Tenant.</param>
    /// <param name="userId">Id of User.</param>
    /// <param name="categoryId">Category Id of the item.</param>
    /// <returns>Newly created partition key.</returns>
    private static PartitionKey GetPK(string tenantId, string userId, string sessionId = null)
    {
        var partitionKeyBuilder = new PartitionKeyBuilder()
            .Add(tenantId ?? string.Empty)   // Add tenantId, defaulting to empty if null
            .Add(userId ?? string.Empty);    // Add userId, defaulting to empty if null

        // Only add sessionId if it is not null or empty
        if (!string.IsNullOrEmpty(sessionId))
        {
            partitionKeyBuilder.Add(sessionId);
        }

        return partitionKeyBuilder.Build();
    }


    /// <summary>
    /// Inserts a new SessionChat into Cosmos DB.
    /// </summary>
    /// <param name="tenantId">Tenant identifier.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="session">SessionChat object to insert.</param>
    /// <returns>The inserted SessionChat object.</returns>
    public async Task InsertSessionChatAsync(
        string tenantId,
        string userId,
        SessionChat session)
    {
        _logger.LogInformation("Inserting new session with sessionId: {sessionId}", session.SessionId);
        try
        {
            PartitionKey partitionKey = GetPK(tenantId, userId, session.SessionId); ;
            // Avoid logging the entire partition key to protect sensitive information
            _logger.LogDebug("PartitionKey constructed for insertion.");

            await _chatContainer.CreateItemAsync<SessionChat>(
                item: session,
                partitionKey: partitionKey
            );

            _logger.LogInformation("Inserted session with sessionId: {sessionId}", session.SessionId);
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            _logger.LogError(ex, "A session with sessionId {sessionId} already exists.", session.SessionId);
            throw new InvalidOperationException($"A session with sessionId '{session.SessionId}' already exists.");
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            _logger.LogError(ex, "Bad request while inserting session with sessionId {sessionId}.", session.SessionId);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting session with sessionId: {sessionId}", session.SessionId);
            throw;
        }
    }

    /// <summary>
    /// Inserts a new Message into Cosmos DB.
    /// </summary>
    /// <param name="tenantId">Tenant identifier.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="sessionId">Session Chat identifier.</param>
    /// <param name="title">Title of the session.</param>
    /// <param name="prompt">The prompt or input content of the message.</param>
    /// <param name="promptTokens">Number of tokens in the prompt.</param>
    /// <param name="userInput">The user input content of the message.</param>
    /// <param name="userInputTokens">Number of tokens in the user input.</param>
    /// <param name="output">The completion or response content of the message.</param>
    /// <param name="outputTokens">Number of tokens in the output.</param>
    /// <param name="cacheHit">Indicates whether the response was retrieved from the cache.</param>
    /// <param name="masterTextSetting">Master text setting for the message.</param>
    /// <param name="chatSetting">Chat setting for the message.</param>
    /// <param name="writingStyleVal">Writing style setting for the message.</param>
    /// <param name="audienceLevelVal">Audience level setting for the message.</param>
    /// <param name="responseLengthVal">Response length setting for the message.</param>
    /// <param name="creativeAdjustmentsVal">Creative adjustments setting for the message.</param>
    /// <param name="relationSettingsVal">Relation settings for the message.</param>
    /// <param name="responseStyleVal">Response style setting for the message.</param>
    /// <returns>The inserted Message object.</returns>
    public async Task<Message> InsertMessageAsync(
        string tenantId,
        string userId,
        string sessionId,
        string title,
        string prompt,
        int promptTokens,
        string userInput,
        int userInputTokens,
        string output,
        int outputTokens,
        bool cacheHit,
        string masterTextSetting,
        string writingStyleVal,
        string audienceLevelVal,
        string responseLengthVal,
        string creativeAdjustmentsVal,
        string relationSettingsVal,
        string responseStyleVal)
    {
        _logger.LogInformation("Inserting new message with Id: {MessageId}", $"Message-{sessionId}-{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Guid.NewGuid().ToString("N").Substring(0, 4)}");

        try
        {
            var message = new Message(
                tenantId: tenantId,
                userId: userId,
                sessionId: sessionId,
                title: title,
                prompt: prompt,
                promptTokens: promptTokens,
                userInput: userInput,
                userInputTokens: userInputTokens,
                output: output,
                outputTokens: outputTokens,
                cacheHit: cacheHit,
                masterTextSetting: masterTextSetting,
                writingStyleVal: writingStyleVal,
                audienceLevelVal: audienceLevelVal,
                responseLengthVal: responseLengthVal,
                creativeAdjustmentsVal: creativeAdjustmentsVal,
                relationSettingsVal: relationSettingsVal,
                responseStyleVal: responseStyleVal
            );

            PartitionKey partitionKey = GetPK(tenantId, userId, sessionId);
            _logger.LogDebug("PartitionKey constructed for message insertion.");

            ItemResponse<Message> response = await _chatContainer.CreateItemAsync<Message>(
                item: message,
                partitionKey: partitionKey
            );

            _logger.LogInformation("Inserted message with Id: {MessageId}", response.Resource.Id);
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
        {
            _logger.LogError(ex, "A message with Id {MessageId} already exists.", $"Message-{sessionId}-{DateTime.UtcNow:yyyyMMdd-HHmmss}-{Guid.NewGuid().ToString("N").Substring(0, 4)}");
            throw new InvalidOperationException($"A message with Id '{ex.ActivityId}' already exists.", ex);
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            _logger.LogError(ex, "Bad request while inserting message.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inserting message.");
            throw;
        }
    }
    public async Task<Message> GetMessageByIdAsync(string messageId)
    {
        try
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id AND c.type = @type")
                .WithParameter("@id", messageId)
                .WithParameter("@type", nameof(Message));

            var iterator = _chatContainer.GetItemQueryIterator<Message>(query);

            if (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                var message = response.FirstOrDefault();
                if (message != null)
                {
                    _logger.LogInformation("Message retrieved with ID: {MessageId}", message.Id);
                    return message;
                }
            }

            _logger.LogWarning("No message found with ID: {MessageId}", messageId);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving message with ID: {MessageId}", messageId);
            throw;
        }
    }

    /// <summary>
    /// Gets a list of all current chat sessions.
    /// </summary>
    /// <param name="tenantId">Id of Tenant.</param>
    /// <param name="userId">Id of User.</param>
    /// <returns>List of distinct chat session items.</returns>
    public async Task<List<SessionChat>> GetSessionsAsync(string tenantId, string userId)
    {
        _logger.LogInformation("Retrieving sessions for Tenant ID: {TenantId}, User ID: {UserId}", tenantId, userId);

        PartitionKey partitionKey = GetPK(tenantId, userId);

        QueryDefinition query = new QueryDefinition("SELECT * FROM c WHERE c.type = @type")
            .WithParameter("@type", nameof(SessionChat));

        FeedIterator<SessionChat> response = _chatContainer.GetItemQueryIterator<SessionChat>(
            query,
            requestOptions: new QueryRequestOptions { PartitionKey = partitionKey }
        );

        List<SessionChat> output = new();
        while (response.HasMoreResults)
        {
            FeedResponse<SessionChat> results = await response.ReadNextAsync();
            output.AddRange(results);
            _logger.LogInformation("Retrieved {Count} sessions in current batch.", results.Count);
        }

        _logger.LogInformation("Total sessions retrieved: {TotalCount}", output.Count);
        return output;
    }


    /// <summary>
    /// Gets a list of all current chat messages for a specified session identifier.
    /// </summary>
    /// <param name="tenantId">Id of Tenant.</param>
    /// <param name="userId">Id of User.</param>
    /// <param name="sessionId">Chat session identifier used to filter messages.</param>
    /// <returns>List of chat message items for the specified session.</returns>
    public async Task<List<Message>> GetSessionMessagesAsync(
        string tenantId,
        string userId,
        string sessionId)
    {
        _logger.LogInformation("Retrieving messages for Session ID: {sessionId}", sessionId);
        PartitionKey partitionKey = GetPK(tenantId, userId, sessionId);

        QueryDefinition query = new QueryDefinition(
                "SELECT * FROM c WHERE c.sessionId = @sessionId AND c.type = @type")
            .WithParameter("@sessionId", sessionId)
            .WithParameter("@type", nameof(Message));

        FeedIterator<Message> results = _chatContainer.GetItemQueryIterator<Message>(
            query,
            requestOptions: new QueryRequestOptions { PartitionKey = partitionKey }
        );

        List<Message> output = new();

        try
        {
            while (results.HasMoreResults)
            {
                FeedResponse<Message> response = await results.ReadNextAsync();
                output.AddRange(response);
                _logger.LogInformation("Retrieved {Count} messages in current batch for Session ID: {sessionId}", response.Count, sessionId);
            }

            _logger.LogInformation("Total messages retrieved for Session ID {sessionId}: {TotalCount}", sessionId, output.Count);
            return output;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving messages for Session ID: {sessionId}", sessionId);
            throw;
        }
    }

    /// <summary>
    /// Updates an existing SessionChat in Cosmos DB.
    /// </summary>
    /// <param name="session">SessionChat object to update.</param>
    /// <param name="partitionKey">Partition key value.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdateSessionAsync(string partitionKey, SessionChat session)
    {
        _logger.LogInformation("Updating session with ID: {sessionId}", session.Id);
        try
        {
            await _chatContainer.ReplaceItemAsync<SessionChat>(
                item: session,
                id: session.Id,
                partitionKey: new PartitionKey(partitionKey)
            );

            _logger.LogInformation("Updated session with ID: {sessionId}.", session.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating session with ID: {sessionId}.", session.Id);
            throw;
        }
    }

    /// <summary>
    /// Retrieves an existing chat session.
    /// </summary>
    /// <param name="tenantId">Id of Tenant.</param>
    /// <param name="userId">Id of User.</param>
    /// <param name="sessionId">Chat session id for the session to return.</param>
    /// <returns>Get chat session item to rename.</returns>
    public async Task<SessionChat> GetSessionAsync(
        string tenantId,
        string userId,
        string sessionId)
    {
        _logger.LogInformation("Retrieving session with sessionId: {sessionId}", sessionId);
        PartitionKey partitionKey = GetPK(tenantId, userId, sessionId);

        try
        {
            ItemResponse<SessionChat> response = await _chatContainer.ReadItemAsync<SessionChat>(
                id: sessionId,
                partitionKey: partitionKey
            );

            _logger.LogInformation("Retrieved session with sessionId: {sessionId}", sessionId);
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Session with sessionId {sessionId} not found.", sessionId);
            throw new KeyNotFoundException($"Session with ID {sessionId} not found.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving session with sessionId: {sessionId}", sessionId);
            throw;
        }
    }

    /// <summary>
    /// Batch upserts chat session and messages within a single transactional batch.
    /// </summary>
    /// <param name="tenantId">Tenant identifier.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="sessionId">Session Chat identifier.</param>
    /// <param name="itemsToUpsert">Array of SessionChat and Message objects to upsert.</param>
    public async Task UpsertSessionBatchAsync(
        string tenantId,
        string userId,
        string sessionId,
        params object[] itemsToUpsert)
    {
        if (itemsToUpsert == null || itemsToUpsert.Length == 0)
        {
            _logger.LogWarning("No items provided to UpsertSessionBatchAsync.");
            throw new ArgumentException("At least one item must be provided.", nameof(itemsToUpsert));
        }

        _logger.LogInformation("Starting UpsertSessionBatchAsync with {Count} items.", itemsToUpsert.Length);

        // Ensure all items share the same partition key
        if (itemsToUpsert.Select(m => GetsessionId(m)).Distinct().Count() > 1)
        {
            _logger.LogError("All items must have the same partition key.");
            throw new ArgumentException("All items must have the same partition key.");
        }

        PartitionKey partitionKey = GetPK(tenantId, userId, sessionId);
        TransactionalBatch batch = _chatContainer.CreateTransactionalBatch(partitionKey);

        foreach (var item in itemsToUpsert)
        {
            batch.UpsertItem(item: item);
        }

        try
        {
            TransactionalBatchResponse response = await batch.ExecuteAsync();
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("UpsertSessionBatchAsync completed successfully.");
            }
            else
            {
                _logger.LogError("UpsertSessionBatchAsync failed with status code: {StatusCode}. Reason: {ReasonPhrase}", response.StatusCode, response.ErrorMessage);
                throw new CosmosException("Batch upsert failed.", response.StatusCode, 0, response.ErrorMessage, 0);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing batch upsert in UpsertSessionBatchAsync.");
            throw;
        }
    }
    /// <summary>
    /// Helper function to extract sessionId from an object.
    /// </summary>
    /// <param name="item">The object to extract from.</param>
    /// <returns>sessionId if available; otherwise, string.Empty.</returns>
    private string GetsessionId(object item)
    {
        return item switch
        {
            SessionChat session => session.SessionId,
            Message message => message.SessionId,
            _ => string.Empty
        };
    }
    /// <summary>
    /// Batch deletes an existing chat session and all related messages.
    /// </summary>
    /// <param name="tenantId">Id of Tenant.</param>
    /// <param name="userId">Id of User.</param>
    /// <param name="sessionId">Chat session identifier used to flag messages and sessions for deletion.</param>
    public async Task DeleteSessionAndMessagesAsync(string tenantId, string userId, string sessionId)
    {
        _logger.LogInformation("Deleting session and messages for Session ID: {sessionId}", sessionId);
        PartitionKey partitionKey = GetPK(tenantId, userId, sessionId);

        QueryDefinition query = new QueryDefinition("SELECT VALUE c.id FROM c WHERE c.sessionId = @sessionId")
                .WithParameter("@sessionId", sessionId);

        FeedIterator<string> response = _chatContainer.GetItemQueryIterator<string>(query);

        TransactionalBatch batch = _chatContainer.CreateTransactionalBatch(partitionKey);

        try
        {
            while (response.HasMoreResults)
            {
                FeedResponse<string> results = await response.ReadNextAsync();
                foreach (var itemId in results)
                {
                    batch.DeleteItem(id: itemId);
                    _logger.LogDebug("Added DeleteItem to batch for ID: {ItemId}", itemId);
                }
            }

            TransactionalBatchResponse batchResponse = await batch.ExecuteAsync();
            if (batchResponse.IsSuccessStatusCode)
            {
                _logger.LogInformation("Deleted session and all related messages for Session ID: {sessionId}", sessionId);
            }
            else
            {
                _logger.LogError("Failed to delete session and messages. Status Code: {StatusCode}", batchResponse.StatusCode);
                throw new CosmosException("Batch delete failed.", batchResponse.StatusCode, 0, "", 0);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting session and messages for Session ID: {sessionId}", sessionId);
            throw;
        }
    }
    /// <summary>
    /// Deletes a specific message within a session.
    /// </summary>
    /// <param name="tenantId">Tenant identifier.</param>
    /// <param name="userId">User identifier.</param>
    /// <param name="sessionId">Session Chat identifier.</param>
    /// <param name="messageId">Message identifier to delete.</param>
    public async Task DeleteMessageAsync(string tenantId, string userId, string sessionId, string messageId)
    {
        _logger.LogInformation("Deleting message with ID: {MessageId} in session: {SessionId}", messageId, sessionId);
        PartitionKey partitionKey = GetPK(tenantId, userId, sessionId);

        try
        {
            // Delete the message item in the container based on ID and partition key
            await _chatContainer.DeleteItemAsync<Message>(messageId, partitionKey);
            _logger.LogInformation("Deleted message with ID: {MessageId} in session: {SessionId}", messageId, sessionId);
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _logger.LogWarning("Message with ID: {MessageId} not found in session: {SessionId}", messageId, sessionId);
            throw new KeyNotFoundException($"Message with ID {messageId} not found in session {sessionId}.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting message with ID: {MessageId} in session: {SessionId}", messageId, sessionId);
            throw;
        }
    }

    /// <summary>
    /// Test method to run a sample query.
    /// </summary>
    public async Task TestAsync()
    {
        _logger.LogInformation("Starting TestAsync method.");

        var query = "SELECT * FROM c WHERE c.partitionKey = 'Andersen'";

        _logger.LogInformation("Running query: {Query}", query);

        try
        {
            QueryDefinition queryDefinition = new QueryDefinition(query);
            FeedIterator<Family> queryResultSetIterator = _chatContainer.GetItemQueryIterator<Family>(queryDefinition);

            List<Family> families = new List<Family>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Family family in currentResultSet)
                {
                    families.Add(family);
                    _logger.LogInformation("Read family: {Family}", family);
                }
            }

            _logger.LogInformation("TestAsync completed successfully with {Count} families retrieved.", families.Count);
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, "CosmosException occurred in TestAsync.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred in TestAsync.");
            throw;
        }
    }

    /// <summary>
    /// Loads product data from the data source URI into Cosmos DB.
    /// </summary>
    public async Task LoadProductDataAsync()
    {
        _logger.LogInformation("Starting LoadProductDataAsync method.");

        try
        {
            // Check if the product already exists
            Product? item = null;
            try
            {
                _logger.LogInformation("Attempting to read product with ID: {ProductId}", "027D0B9A-F9D9-4C96-8213-C8546C4AAE71");
                item = await _productContainer.ReadItemAsync<Product>(
                    id: "027D0B9A-F9D9-4C96-8213-C8546C4AAE71",
                    partitionKey: new PartitionKey("26C74104-40BC-4541-8EF5-9892F7F03D72"));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Product not found: {Message}", ex.Message);
            }

            if (item is null)
            {
                _logger.LogInformation("Product not found. Loading from data source URI: {URI}", _productDataSourceURI);
                string json = "";
                string jsonFilePath = _productDataSourceURI; // URI to the vectorized product JSON file
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(jsonFilePath);
                if (response.IsSuccessStatusCode)
                {
                    json = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation("Successfully fetched product data from URI.");
                }
                else
                {
                    _logger.LogError("Failed to fetch product data. Status Code: {StatusCode}", response.StatusCode);
                    throw new HttpRequestException($"Failed to fetch product data. Status Code: {response.StatusCode}");
                }

                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(json)!;
                _logger.LogInformation("Deserialized {Count} products from JSON.", products.Count);

                foreach (var product in products)
                {
                    try
                    {
                        await InsertProductAsync(product);
                        _logger.LogInformation("Inserted product: {ProductName}", product.name);
                    }
                    catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        _logger.LogWarning("Conflict inserting product {ProductName}: {Message}", product.name, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error inserting product {ProductName}.", product.name);
                        throw;
                    }
                }
            }
            else
            {
                _logger.LogInformation("Product already exists. Skipping load.");
            }

            _logger.LogInformation("LoadProductDataAsync completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in LoadProductDataAsync.");
            throw;
        }
    }

    public async Task UpsertEmailMessageAsync(
        string tenantId,
        string userId,
        List<string> categoryIds,
        string subject,
        EmailMessage email)
    {
        _logger.LogInformation("Upserting email message with subject: {Subject}", subject);

        // Generate a partition key using the first category as the primary identifier
        PartitionKey partitionKey = GetPK(tenantId, userId, categoryIds.FirstOrDefault());

        try
        {
            // Upsert the email message into the specified container
            ItemResponse<EmailMessage> emailResponse = await _organizerContainer.UpsertItemAsync(email, partitionKey);
            _logger.LogInformation("Upserted Email: {EmailId} (Subject: {Subject})", emailResponse.Resource.Id, emailResponse.Resource.Subject);
        }
        catch (CosmosException ex)
        {
            _logger.LogError(ex, "Cosmos DB Exception for Email ID {EmailId}", email.Id);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while upserting email with subject: {Subject}", subject);
            throw;
        }
    }

    public async Task QueryEmailMessagesAsync(
        string tenantId,
        string userId,
        string categoryId = null)
    {
        // Define the SQL query string with parameters
        string queryStatement = "SELECT * FROM c WHERE c.tenantId = @tenantId AND c.userId = @userId AND c.type = @type";

        if (!string.IsNullOrEmpty(categoryId))
        {
            queryStatement += " AND c.categoryId = @categoryId";
        }

        // Set up a query definition with parameters for tenantId, userId, and type
        QueryDefinition query = new QueryDefinition(queryStatement)
            .WithParameter("@tenantId", tenantId)
            .WithParameter("@userId", userId)
            .WithParameter("@type", "EmailMessage");

        if (!string.IsNullOrEmpty(categoryId))
        {
            query.WithParameter("@categoryId", categoryId);
        }

        // Decide whether to use a partition key based on categoryId's availability
        QueryRequestOptions requestOptions = new QueryRequestOptions();
        if (!string.IsNullOrEmpty(categoryId))
        {
            requestOptions.PartitionKey = GetPK(tenantId, userId, categoryId);
        }
        // Initialize a FeedIterator to paginate through the query results
        using FeedIterator<EmailMessage> feed = _organizerContainer.GetItemQueryIterator<EmailMessage>(
            query, requestOptions: requestOptions);

        // Total request charge accumulator
        double totalRequestCharge = 0;

        Console.WriteLine($"[Start Query]: {queryStatement}");

        // Iterate over pages of results
        while (feed.HasMoreResults)
        {
            FeedResponse<EmailMessage> page = await feed.ReadNextAsync();
            totalRequestCharge += page.RequestCharge;

            foreach (EmailMessage email in page)
            {
                Console.WriteLine($"[Returned item]:\t{email.Id}\t(Subject: {email.Subject})");
                Console.WriteLine($"[Returned item]:\t{email.Id}\t(BodyContentText: {email.BodyContentText})");
            }
        }

        // Log total RU charge for the query
        Console.WriteLine($"[Query metrics]: (Total RUs: {totalRequestCharge})");
    }

    public async Task<List<EmailMessage>> SearchEmailsAsync(
        float[] vectors,
        string tenantId,
        string userId,
        string categoryId,
        int emailMaxResults)
    {

        emailMaxResults = 1;
        _logger.LogInformation("Searching for similar emails with max results: {MaxResults} for TenantId={TenantId}, UserId={UserId}, and CategoryId={CategoryId}", emailMaxResults, tenantId, userId, categoryId ?? "None");

        List<EmailMessage> results = new();

        // Construct SQL query with optional categoryId filtering
        string queryText = $"""
        SELECT 
            TOP @maxResults
            c.id, c.tenantId, c.userId, c.subject, c.bodyContentText, c.categoryId, 
            c.keypoints, c.conversationId, c.webLink, c.categoryIds,
            VectorDistance(c.vectors, @vectors) as similarityScore
        FROM c 
        WHERE c.type = 'EmailMessage' AND c.tenantId = @tenantId AND c.userId = @userId
        """;

        if (!string.IsNullOrEmpty(categoryId))
        {
            queryText += " AND c.categoryId = @categoryId";
        }

        queryText += " ORDER BY VectorDistance(c.vectors, @vectors)";

        // Set up a query definition with parameters for tenantId, userId, categoryId, and vectors
        var queryDef = new QueryDefinition(queryText)
            .WithParameter("@maxResults", emailMaxResults)
            .WithParameter("@vectors", vectors)
            .WithParameter("@tenantId", tenantId)
            .WithParameter("@userId", userId);

        if (!string.IsNullOrEmpty(categoryId))
        {
            queryDef = queryDef.WithParameter("@categoryId", categoryId);
        }

        using FeedIterator<EmailMessage> resultSet = _organizerContainer.GetItemQueryIterator<EmailMessage>(queryDef);

        try
        {
            while (resultSet.HasMoreResults)
            {
                FeedResponse<EmailMessage> response = await resultSet.ReadNextAsync();

                foreach (var email in response)
                {
                    _logger.LogInformation("Email ID: {EmailId}, Subject: {Subject}, ConversationId: {ConversationId}, WebLink: {WebLink}, KeyPoints: {KeyPoints}",
                        email.Id, email.Subject, email.ConversationId ?? "N/A", email.WebLink ?? "N/A", email.KeyPoints ?? "N/A");

                    // Convert the email object to JSON and log it to see all fields
                    string emailJson = JsonConvert.SerializeObject(email, Formatting.Indented);
                    _logger.LogInformation("Email JSON: {EmailJson}", emailJson);

                    results.Add(email);
                }

                _logger.LogInformation("Retrieved {Count} emails in current batch.", response.Count);
            }

            _logger.LogInformation("SearchEmailsAsync found {TotalCount} similar emails.", results.Count);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for similar emails.");
            throw;
        }
    }


    /// <summary>
    /// Upserts a new product.
    /// </summary>
    /// <param name="product">Product item to create or update.</param>
    /// <returns>Newly created or updated product item.</returns>
    public async Task<Product> InsertProductAsync(Product product)
    {
        _logger.LogInformation("Upserting product with ID: {ProductId}", product.id);
        PartitionKey partitionKey = new(product.categoryId);

        try
        {
            Product upsertedProduct = await _productContainer.CreateItemAsync<Product>(
                item: product,
                partitionKey: partitionKey
            );

            _logger.LogInformation("Upserted product with ID: {ProductId}", upsertedProduct.id);
            return upsertedProduct;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error upserting product with ID: {ProductId}", product.id);
            throw;
        }
    }

    /// <summary>
    /// Delete a product.
    /// </summary>
    /// <param name="product">Product item to delete.</param>
    public async Task DeleteProductAsync(Product product)
    {
        _logger.LogInformation("Deleting product with ID: {ProductId}", product.id);
        PartitionKey partitionKey = new(product.categoryId);

        try
        {
            await _productContainer.DeleteItemAsync<Product>(
                id: product.id,
                partitionKey: partitionKey
            );

            _logger.LogInformation("Deleted product with ID: {ProductId}", product.id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product with ID: {ProductId}", product.id);
            throw;
        }
    }

    /// <summary>
    /// Search vectors for similar products.
    /// </summary>
    /// <param name="vectors">Vectors to search against.</param>
    /// <param name="productMaxResults">Maximum number of products to return.</param>
    /// <returns>List of similar product items.</returns>
    public async Task<List<Product>> SearchProductsAsync(float[] vectors, int productMaxResults)
    {
        _logger.LogInformation("Searching for similar products with max results: {MaxResults}", productMaxResults);
        List<Product> results = new();

        string queryText = $"""
            SELECT 
                TOP @maxResults
                c.categoryName, c.sku, c.name, c.description, c.price, c.tags, VectorDistance(c.vectors, @vectors) as similarityScore
            FROM c 
            ORDER BY VectorDistance(c.vectors, @vectors)
            """;

        var queryDef = new QueryDefinition(query: queryText)
            .WithParameter("@maxResults", productMaxResults)
            .WithParameter("@vectors", vectors);

        using FeedIterator<Product> resultSet = _productContainer.GetItemQueryIterator<Product>(queryDef);

        try
        {
            while (resultSet.HasMoreResults)
            {
                FeedResponse<Product> response = await resultSet.ReadNextAsync();
                results.AddRange(response);
                _logger.LogInformation("Retrieved {Count} products in current batch.", response.Count);
            }

            _logger.LogInformation("SearchProductsAsync found {TotalCount} similar products.", results.Count);
            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching for similar products.");
            throw;
        }
    }


    /// <summary>
    /// Find a cache item based on vector similarity.
    /// </summary>
    /// <param name="vectors">Vectors to perform the semantic search in the cache.</param>
    /// <param name="similarityScore">Minimum similarity score to consider.</param>
    /// <returns>Completion text from the cache if found; otherwise, an empty string.</returns>
    public async Task<string> GetCacheAsync(float[] vectors, double similarityScore)
    {
        _logger.LogInformation("Searching cache with similarity score > {SimilarityScore}", similarityScore);
        string cacheResponse = "";

        string queryText = $"""
            SELECT TOP 1 c.prompt, c.output, VectorDistance(c.vectors, @vectors) as similarityScore
            FROM c  
            WHERE VectorDistance(c.vectors, @vectors) > @similarityScore 
            ORDER BY VectorDistance(c.vectors, @vectors)
            """;

        var queryDef = new QueryDefinition(query: queryText)
            .WithParameter("@vectors", vectors)
            .WithParameter("@similarityScore", similarityScore);

        using FeedIterator<CacheItem> resultSet = _cacheContainer.GetItemQueryIterator<CacheItem>(queryDef);

        try
        {
            while (resultSet.HasMoreResults)
            {
                FeedResponse<CacheItem> response = await resultSet.ReadNextAsync();

                foreach (CacheItem item in response)
                {
                    cacheResponse = item.Output;
                    _logger.LogInformation("Cache hit for similarity score: {SimilarityScore}", similarityScore);
                    return cacheResponse;
                }
            }

            _logger.LogInformation("No cache items found with similarity score > {SimilarityScore}", similarityScore);
            return cacheResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching cache.");
            throw;
        }
    }

    /// <summary>
    /// Add a new cache item.
    /// </summary>
    /// <param name="cacheItem">Cache item to add or update.</param>
    public async Task CachePutAsync(CacheItem cacheItem)
    {
        _logger.LogInformation("Adding or updating cache item with ID: {CacheItemId}", cacheItem.Id);

        try
        {
            await _cacheContainer.UpsertItemAsync<CacheItem>(item: cacheItem);
            _logger.LogInformation("Upserted cache item with ID: {CacheItemId}", cacheItem.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error upserting cache item with ID: {CacheItemId}", cacheItem.Id);
            throw;
        }
    }

    /// <summary>
    /// Remove a cache item using its vectors.
    /// </summary>
    /// <param name="vectors">Vectors used to perform the semantic search. Similarity Score is set to 0.99 for exact match.</param>
    public async Task CacheRemoveAsync(float[] vectors)
    {
        double similarityScore = 0.99;
        _logger.LogInformation("Removing cache item with vectors having similarity score > {SimilarityScore}", similarityScore);

        string queryText = $"""
            SELECT TOP 1 c.id
            FROM c  
            WHERE VectorDistance(c.vectors, @vectors) > @similarityScore 
            ORDER BY VectorDistance(c.vectors, @vectors)
            """;

        var queryDef = new QueryDefinition(query: queryText)
            .WithParameter("@vectors", vectors)
            .WithParameter("@similarityScore", similarityScore);

        using FeedIterator<CacheItem> resultSet = _cacheContainer.GetItemQueryIterator<CacheItem>(queryDef);

        try
        {
            while (resultSet.HasMoreResults)
            {
                FeedResponse<CacheItem> response = await resultSet.ReadNextAsync();

                foreach (CacheItem item in response)
                {
                    await _cacheContainer.DeleteItemAsync<CacheItem>(
                        partitionKey: new PartitionKey(item.Id),
                        id: item.Id
                    );
                    _logger.LogInformation("Deleted cache item with ID: {CacheItemId}", item.Id);
                    return;
                }
            }

            _logger.LogWarning("No cache item found to remove with the specified vectors.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing cache item with vectors.");
            throw;
        }
    }

    /// <summary>
    /// Clear the cache of all cache items.
    /// </summary>
    public async Task CacheClearAsync()
    {
        _logger.LogInformation("Clearing all cache items.");

        string queryText = "SELECT c.id FROM c";

        var queryDef = new QueryDefinition(query: queryText);

        using FeedIterator<CacheItem> resultSet = _cacheContainer.GetItemQueryIterator<CacheItem>(queryDef);

        try
        {
            while (resultSet.HasMoreResults)
            {
                FeedResponse<CacheItem> response = await resultSet.ReadNextAsync();

                foreach (CacheItem item in response)
                {
                    await _cacheContainer.DeleteItemAsync<CacheItem>(
                        partitionKey: new PartitionKey(item.Id),
                        id: item.Id
                    );
                    _logger.LogDebug("Deleted cache item with ID: {CacheItemId}", item.Id);
                }
            }

            _logger.LogInformation("All cache items have been cleared.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error clearing cache items.");
            throw;
        }
    }
}
