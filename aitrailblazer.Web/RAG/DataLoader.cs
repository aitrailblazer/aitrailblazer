// Copyright (c) Microsoft. All rights reserved.

using System.Net;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Embeddings;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using PartitionKey = Microsoft.Azure.Cosmos.PartitionKey;
using Cosmos.Copilot.Models;

using Cosmos.Copilot.Services;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;

namespace VectorStoreRAG;

/// <summary>
/// Class that loads text from a PDF file into a vector store.
/// </summary>
/// <typeparam name="TKey">The type of the data model key.</typeparam>
internal sealed class DataLoader<TKey>(
    IVectorStoreRecordCollection<TKey, TextSnippet<TKey>> vectorStoreRecordCollection,
    ITextEmbeddingGenerationService textEmbeddingGenerationService,
    CosmosDbService cosmosDbService,
    IChatCompletionService chatCompletionService,
    ILogger<DataLoader<TKey>> logger) : IDataLoader where TKey : notnull
{
    private readonly IVectorStoreRecordCollection<TKey, TextSnippet<TKey>> _vectorStoreRecordCollection = vectorStoreRecordCollection;
    private readonly ITextEmbeddingGenerationService _textEmbeddingGenerationService = textEmbeddingGenerationService;
    private readonly CosmosDbService _cosmosDbService = cosmosDbService;
    private readonly ILogger<DataLoader<TKey>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <inheritdoc/>
    public async Task LoadPdf(
       string tenantId,
       string userId,
       string fileName,
       string directory,
       string blobName,
       string memoryKey,
       Stream fileStream,
       int batchSize,
       int betweenBatchDelayInMs,
       CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting PDF loading process for file: {FileName}", fileName);

        // Ensure the vector store collection exists.
        await _vectorStoreRecordCollection.CreateCollectionIfNotExistsAsync(cancellationToken).ConfigureAwait(false);

        // Extract sections from the PDF stream.
        var sections = LoadTextAndImagesFromStream(fileStream, cancellationToken);
        var batches = sections.Chunk(batchSize);
        int counter = 1;

        foreach (var batch in batches)
        {
            foreach (var content in batch)
            {
                try
                {
                    if (content.Text == null && content.Image != null)
                    {
                        // Convert image to text.
                        content.Text = await ConvertImageToTextWithRetryAsync(
                            chatCompletionService,
                            content.Image.Value,
                            cancellationToken
                        ).ConfigureAwait(false);
                    }

                    if (string.IsNullOrWhiteSpace(content.Text))
                    {
                        _logger.LogWarning("Skipped empty content for page {PageNumber}.", content.PageNumber);
                        continue;
                    }

                    // Generate a unique key for the KnowledgeBaseItem.
                    string uniqueKey = $"{memoryKey}-page{content.PageNumber}-{counter:D5}";
                    string destination = $"{tenantId}/{userId}/{directory}/{blobName}/{fileName}";
                    string categoryId = "Document";
                    var vectors = (await GenerateEmbeddingsWithRetryAsync(
                        textEmbeddingGenerationService,
                        content.Text!,
                        cancellationToken: cancellationToken).ConfigureAwait(false)).ToArray();

                    // Create the KnowledgeBaseItem.
                    var knowledgeBaseItem = new KnowledgeBaseItem(
                        uniqueKey, // Assigning uniqueKey as the Id
                        tenantId: tenantId,
                        userId: userId,
                        categoryId: categoryId, // Use file name as the category
                        title: $"Page {content.PageNumber}",
                        content: content.Text!,
                        referenceDescription: $"{fileName}#page={content.PageNumber}",
                        referenceLink: $"{destination}#page={content.PageNumber}",
                        vectors: vectors // Skipping embeddings generation for now
                    );


                    _logger.LogInformation("Upserting knowledge base item with Id {UniqueKey}.", uniqueKey);

                    // Upsert the KnowledgeBaseItem into Cosmos DB.
                    await _cosmosDbService.UpsertKnowledgeBaseItemAsync(
                        tenantId, 
                        userId, 
                        categoryId, 
                        knowledgeBaseItem);
                    _logger.LogInformation("Successfully upserted item with Id {uniqueKey}.", uniqueKey);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while processing page {PageNumber} .", content.PageNumber);
                    // Continue to the next content in the batch.
                }

                counter++;
            }

            // Delay between batch processing to avoid overloading the system.
            _logger.LogInformation("Completed batch processing. Waiting for {Delay} ms before next batch.", betweenBatchDelayInMs);
            await Task.Delay(betweenBatchDelayInMs, cancellationToken).ConfigureAwait(false);
        }

        _logger.LogInformation("Completed PDF loading process for file: {FileName}", fileName);
    }

    public async Task LoadPdfOld(
           string tenantId,
           string userId,
           string fileName,
           string directory,
           string blobName,
           string memoryKey,
           Stream fileStream,
           int batchSize,
           int betweenBatchDelayInMs,
           CancellationToken cancellationToken)
    {
        // Create the collection if it doesn't exist.
        await vectorStoreRecordCollection.CreateCollectionIfNotExistsAsync(cancellationToken).ConfigureAwait(false);

        // Load the text and images from the PDF stream and split them into batches.
        var sections = LoadTextAndImagesFromStream(fileStream, cancellationToken);
        var batches = sections.Chunk(batchSize);
        int counter = 1; // Initialize the counter

        // Process each batch of content items.
        foreach (var batch in batches)
        {
            // Convert any images to text.
            var textContentTasks = batch.Select(async content =>
            {
                if (content.Text != null)
                {
                    return content;
                }

                var textFromImage = await ConvertImageToTextWithRetryAsync(
                    chatCompletionService,
                    content.Image!.Value,
                    cancellationToken).ConfigureAwait(false);

                return new RawContent { Text = textFromImage, PageNumber = content.PageNumber };
            });

            var textContent = await Task.WhenAll(textContentTasks).ConfigureAwait(false);

            // Extract the file name
            //string fileName = Path.GetFileName(filePath);

            // Map each paragraph to a TextSnippet and generate an embedding for it.
            var recordTasks = textContent.Select(async content =>
            {
                // Generate a unique key using the tenantID, userID, file name, page number, and text content
                string uniqueKey = $"{memoryKey}-page{content.PageNumber}-{counter.ToString("D5")}";

                string destination = $"{tenantId}/{userId}/{directory}/{blobName}/{fileName}";

                return new TextSnippet<TKey>
                {
                    Key = (TKey)(object)uniqueKey, // Cast uniqueKey to TKey
                    Text = content.Text,
                    // ReferenceDescription": "semantic-kernel.pdf#page=1
                    ReferenceDescription = $"{fileName}#page={content.PageNumber}",
                    //ReferenceLink = $"{new Uri(Path.GetFullPath(filePath)).AbsoluteUri}#page={content.PageNumber}",
                    // "ReferenceLink": "2e58c7ce-9814-4e3d-9e88-467669ba3f5c/8f22704e-0396-4263-84a7-63310d3f39e7/Documents/Default/semantickernelpdf#page=3",
                    ReferenceLink = $"{destination}#page={content.PageNumber}",
                    TextEmbedding = await GenerateEmbeddingsWithRetryAsync(
                        textEmbeddingGenerationService,
                        content.Text!,
                        cancellationToken: cancellationToken).ConfigureAwait(false)
                };
            });

            // Upsert the records into the vector store.
            var records = await Task.WhenAll(recordTasks).ConfigureAwait(false);
            var upsertedKeys = vectorStoreRecordCollection.UpsertBatchAsync(records, cancellationToken: cancellationToken);
            await foreach (var key in upsertedKeys.ConfigureAwait(false))
            {
                Console.WriteLine($"Upserted record '{key}' into VectorDB");
            }

            await Task.Delay(betweenBatchDelayInMs, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Reads the text and images from each page in the provided PDF stream.
    /// </summary>
    private static IEnumerable<RawContent> LoadTextAndImagesFromStream(Stream pdfStream, CancellationToken cancellationToken)
    {
        using (var document = PdfDocument.Open(pdfStream))
        {
            foreach (var page in document.GetPages())
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }

                foreach (var image in page.GetImages())
                {
                    if (image.TryGetPng(out var png))
                    {
                        yield return new RawContent { Image = png, PageNumber = page.Number };
                    }
                }

                var blocks = DefaultPageSegmenter.Instance.GetBlocks(page.GetWords());
                foreach (var block in blocks)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        yield break;
                    }

                    yield return new RawContent { Text = block.Text, PageNumber = page.Number };
                }
            }
        }
    }

    /// <summary>
    /// Retries converting image to text with the chat completion service.
    /// </summary>
    private static async Task<string> ConvertImageToTextWithRetryAsync(
         IChatCompletionService chatCompletionService,
         ReadOnlyMemory<byte> imageBytes,
         CancellationToken cancellationToken)
    {
        var tries = 0;

        while (true)
        {
            try
            {
                var chatHistory = new ChatHistory();
                chatHistory.AddUserMessage([
                    new TextContent("What’s in this image?"),
                    new ImageContent(imageBytes, "image/png"),
                ]);
                var result = await chatCompletionService.GetChatMessageContentsAsync(chatHistory, cancellationToken: cancellationToken).ConfigureAwait(false);
                return string.Join("\n", result.Select(x => x.Content));
            }
            catch (HttpOperationException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests)
            {
                tries++;

                if (tries < 3)
                {
                    Console.WriteLine($"Failed to generate text from image. Error: {ex}");
                    Console.WriteLine("Retrying text to image conversion...");
                    await Task.Delay(10_000, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    throw;
                }
            }
        }
    }

    /// <summary>
    /// Add a simple retry mechanism to embedding generation.
    /// </summary>
    /// <param name="textEmbeddingGenerationService">The embedding generation service.</param>
    /// <param name="text">The text to generate the embedding for.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
    /// <returns>The generated embedding.</returns>
    private static async Task<ReadOnlyMemory<float>> GenerateEmbeddingsWithRetryAsync(
        ITextEmbeddingGenerationService textEmbeddingGenerationService,
        string text,
        CancellationToken cancellationToken)
    {
        var tries = 0;

        while (true)
        {
            try
            {
                return await textEmbeddingGenerationService.GenerateEmbeddingAsync(text, cancellationToken: cancellationToken).ConfigureAwait(false);
            }
            catch (HttpOperationException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests)
            {
                tries++;

                if (tries < 3)
                {
                    Console.WriteLine($"Failed to generate embedding. Error: {ex}");
                    Console.WriteLine("Retrying embedding generation...");
                    await Task.Delay(10_000, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    throw;
                }
            }
        }
    }


    /// <summary>
    /// Represents the raw content (text or image) of a PDF page.
    /// </summary>
    private sealed class RawContent
    {
        public string? Text { get; set; }
        public ReadOnlyMemory<byte>? Image { get; set; }
        public int PageNumber { get; set; }
    }
}
