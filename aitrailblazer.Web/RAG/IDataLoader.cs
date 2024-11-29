﻿// Copyright (c) Microsoft. All rights reserved.

namespace VectorStoreRAG;

/// <summary>
/// Interface for loading data into a data store.
/// </summary>
internal interface IDataLoader
{
    /// <summary>
    /// Load the text from a PDF stream into the data store.
    /// </summary>
    /// <param name="pdfStream">The PDF stream to load.</param>
    /// <param name="batchSize">Maximum number of parallel threads to generate embeddings and upload records.</param>
    /// <param name="betweenBatchDelayInMs">The number of milliseconds to delay between batches to avoid throttling.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests.</param>
    /// <param name="filePath">The original file path of the PDF to preserve metadata.</param>
    /// <param name="tenantID">The tenant ID to associate with the records.</param>
    /// <param name="userID">The user ID to associate with the records.</param>
    /// <returns>An async task that completes when the loading is complete.</returns>
    Task LoadPdf(
        string tenantID, 
        string userID, 
        string fileName,
        string directory,
        string blobName, 
        string memoryKey,
        Stream fileStream, 
        int batchSize, 
        int betweenBatchDelayInMs, 
        CancellationToken cancellationToken);
}
