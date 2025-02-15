using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

public class BlobStorageManager
{
    private readonly string _storageConnectionString;
    private readonly string _containerName;

    public BlobStorageManager(string storageConnectionString,
    string containerName)
    {
        _storageConnectionString = storageConnectionString;
        _containerName = containerName;
    }

    public async Task CreateContainerAsync()
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
        BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(_containerName);
        Console.WriteLine($"A container named '{_containerName}' has been created.");
    }

    public async Task UploadBlobAsync(string id, string directory, string fileName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

        string destinationDirectory = $"{id}-{directory}";
        string destinationFileName = $"{fileName}/{fileName}";
        var destination = $"{destinationDirectory}/{destinationFileName}";

        BlobClient blobClient = containerClient.GetBlobClient(destination);
        bool blobExists = await blobClient.ExistsAsync();

        if (blobExists)
        {
            Console.WriteLine($"Blob '{destination}' exists. Deleting it before uploading.");
            await blobClient.DeleteAsync();
        }

        string localPath = "./data/";
        string localFilePath = Path.Combine(localPath, fileName);

        if (!File.Exists(localFilePath))
        {
            Console.WriteLine($"Local file '{localFilePath}' does not exist. Please create the file before uploading.");
            return;
        }

        Console.WriteLine($"Uploading to Blob storage as blob:\n\t {blobClient.Uri}\n");

        using (FileStream uploadFileStream = File.OpenRead(localFilePath))
        {
            await blobClient.UploadAsync(uploadFileStream);
            uploadFileStream.Close();
        }

        Console.WriteLine("\nThe file was uploaded.");
    }
    public async Task UploadBlobFromContentAsync(
        string id,
        string directory,
        string filePath,
        string fileName)
    {
        try
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(stream);
            var fileContent = await reader.ReadToEndAsync();

            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            string destinationDirectory = $"{id}-{directory}";
            string destinationFileName = $"{fileName}/{fileName}";
            var destination = $"{destinationDirectory}/{destinationFileName}";
            Console.WriteLine($"UploadBlobFromContentAsync destination:\n\t {destination}\n");

            BlobClient blobClient = containerClient.GetBlobClient(destination);

            // Check if the blob already exists
            bool blobExists = await blobClient.ExistsAsync();
            if (blobExists)
            {
                Console.WriteLine($"Blob '{destination}' already exists. Deleting it before uploading.");
                await blobClient.DeleteAsync();
            }

            Console.WriteLine($"UploadBlobFromContentAsync Uploading to Blob storage as blob:\n\t {blobClient.Uri}\n");

            using var uploadStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent));
            await blobClient.UploadAsync(uploadStream, overwrite: true);

            Console.WriteLine("\nUploadBlobFromContentAsync The file content was uploaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading blob from content: {ex.Message}");
        }
    }

    //  asapmemory-aitrailblazer-net / 8f22704e-0396-4263-84a7-63310d3f39e7-Sessions / ChatSession-20241009-205531-Creating Calendar Events from Emails A Step by Step Guide
    //                                 8f22704e-0396-4263-84a7-63310d3f39e7-Sessions / ChatSession-20241009-205531-Creating Calendar Events from Emails A Step by Step Guide/Request-20241009-205531-Writing-AIWritingAssistant-create step by step for creating.json

    public async Task UploadStringToBlobAsync(
        string tenantId,
        string userId,
        string directory,
        string blobName,
        string fileName,
        string content)
    {
        try
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Construct the destination path including tenantId
            string destination = $"{tenantId}/{userId}/{directory}/{blobName}/{fileName}";
            Console.WriteLine($"UploadStringToBlobAsync destination:\n\t {destination}\n");

            BlobClient blobClient = containerClient.GetBlobClient(destination);

            Console.WriteLine($"UploadStringToBlobAsync Uploading to Blob storage as blob:\n\t {blobClient.Uri}\n");

            using var uploadStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
            await blobClient.UploadAsync(uploadStream, overwrite: true);

            Console.WriteLine("\nUploadStringToBlobAsync The string content was uploaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading string to blob: {ex.Message}");
        }
    }

    public async Task UploadFileAsync(string blobName, Stream fileStream)
    {
        try
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            Console.WriteLine($"Uploading to Blob storage as blob:\n\t {blobClient.Uri}\n");

            await blobClient.UploadAsync(fileStream, overwrite: true);

            Console.WriteLine("\nThe file was uploaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading file '{blobName}': {ex.Message}");
        }
    }

    public async Task UploadBlobFromStreamAsync(
       string tenantID,
       string userId,
       string directory,
       string blobName,
       string fileName,
       Stream fileStream)
    {
        try
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Construct the destination path with tenantID and userId
            string destination = $"{tenantID}/{userId}/{directory}/{blobName}/{fileName}";
            Console.WriteLine($"UploadBlobFromStreamAsync destination:\n\t {destination}\n");

            BlobClient blobClient = containerClient.GetBlobClient(destination);

            // Check if the blob already exists
            bool blobExists = await blobClient.ExistsAsync();
            if (blobExists)
            {
                Console.WriteLine($"Blob '{destination}' already exists. Deleting it before uploading.");
                await blobClient.DeleteAsync();
            }

            Console.WriteLine($"UploadBlobFromStreamAsync Uploading to Blob storage as blob:\n\t {blobClient.Uri}\n");

            // Upload the file stream to Blob storage
            await blobClient.UploadAsync(fileStream, overwrite: true);

            Console.WriteLine("\nUploadBlobFromStreamAsync The file stream was uploaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading blob from stream: {ex.Message}");
        }
    }

    public async Task DeleteBlobDirectoryAsync(string directoryPrefix)
    {
        try
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // 54a15dce-218d-4889-842f-4709a86704ed-Sessions/ChatSession-20240903-034647-No Code AI Solutions for Efficient Problem Solving

            // 54a15dce-218d-4889-842f-4709a86704ed-Writing/ChatSession-20240903-034647-No Code AI Solutions for Efficient Problem Solving/
            Console.WriteLine($"Deleting blobs in directory: {directoryPrefix}");

            bool blobsFound = false;

            // List all blobs under the specified directory prefix
            await foreach (var blobItem in containerClient.GetBlobsByHierarchyAsync(prefix: directoryPrefix, delimiter: "/"))
            {
                if (blobItem.IsBlob)
                {
                    blobsFound = true;
                    BlobClient blobClient = containerClient.GetBlobClient(blobItem.Blob.Name);
                    Console.WriteLine($"Found blob: {blobItem.Blob.Name}");

                    // Delete the blob along with its snapshots
                    await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                    Console.WriteLine($"Deleted blob: {blobItem.Blob.Name}");
                }
            }

            if (!blobsFound)
            {
                Console.WriteLine("No blobs found with the specified prefix.");
            }
            else
            {
                Console.WriteLine($"Deleted all blobs in directory: {directoryPrefix}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting blob directory: {ex.Message}");
        }
    }


    public async Task DeleteBlobItemsAsync(
        string tenantId,
        string userId,
        string directory,
        string blobName,
        string requestFileName,
        string responseFileName)
    {
        try
        {
            var blobServiceClient = new BlobServiceClient(_storageConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Construct the full paths for the request and response blobs, including tenantId
            string requestBlobPath = $"{tenantId}/{userId}/{directory}/{blobName}/{requestFileName}";
            string responseBlobPath = $"{tenantId}/{userId}/{directory}/{blobName}/{responseFileName}";

            // Delete the request blob
            var requestBlobClient = containerClient.GetBlobClient(requestBlobPath);
            await requestBlobClient.DeleteIfExistsAsync();
            Console.WriteLine($"Deleted request blob: {requestBlobPath}");

            // Delete the response blob
            var responseBlobClient = containerClient.GetBlobClient(responseBlobPath);
            await responseBlobClient.DeleteIfExistsAsync();
            Console.WriteLine($"Deleted response blob: {responseBlobPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting blobs: {ex.Message}");
        }
    }


    public async Task<List<string>> ListSessionsAsync(
       string userId,
       string directory)
    {
        var sessionTitles = new List<string>();

        try
        {
            Console.WriteLine($"ListSessionsAsync started for user: {userId}, directory: {directory}");

            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            //  54a15dce-218d-4889-842f-4709a86704ed-Sessions
            // ChatSession-20240903-010923-AI Driven Automation for Business Workflows
            string prefix = $"{userId}-{directory}/ChatSession-";
            Console.WriteLine($"Prefix used for listing blobs: {prefix}");

            await foreach (var blobItem in containerClient.GetBlobsByHierarchyAsync(
                prefix: prefix,
                delimiter: "/"))
            {
                if (blobItem.IsPrefix)
                {
                    Console.WriteLine($"Processing prefix item: {blobItem.Prefix}");


                    // ChatSession-20240822-052903-Streamlining Customer Requirements and Accelerating Code Creation with GenAI
                    // Extract the session title from the blobItem.Prefix
                    string sessionTitle = blobItem.Prefix
                        .Replace($"{userId}-{directory}/ChatSession-", "")
                        .TrimEnd('/');

                    Console.WriteLine($"Extracted session title: {sessionTitle}");

                    sessionTitles.Add(sessionTitle);
                }
                else if (blobItem.Blob != null)
                {
                    Console.WriteLine($"Processing blob item: {blobItem.Blob.Name}");
                }
            }

            Console.WriteLine($"Total sessions found: {sessionTitles.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error listing sessions: {ex.Message}");
        }

        return sessionTitles;
    }

    // 2e58c7ce-9814-4e3d-9e88-467669ba3f5c/
    // 8f22704e-0396-4263-84a7-63310d3f39e7/
    // Documents/
    // Default/
    // Las_Vegas_Brochure.pdf#page=1
    public async Task<string> ReadBlobContentByPathAsync(string blobPath)
    {
        try
        {
            // Sanitize the path by removing any fragment starting with '#'
            string sanitizedPath = blobPath.Split('#')[0];
            Console.WriteLine($"Sanitized blob path: {sanitizedPath}");

            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            Console.WriteLine($"Attempting to read blob at path: {sanitizedPath}");

            BlobClient blobClient = containerClient.GetBlobClient(sanitizedPath);

            if (await blobClient.ExistsAsync())
            {
                using var blobStream = await blobClient.OpenReadAsync();
                using var reader = new StreamReader(blobStream);
                return await reader.ReadToEndAsync();
            }
            else
            {
                Console.WriteLine($"Blob does not exist at path: {sanitizedPath}");
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading blob content: {ex.Message}");
            return string.Empty;
        }
    }
    public async Task<string> GetBlobSasUrlAsync(string blobPath, int expirationMinutes = 30)
    {
        try
        {
            // Sanitize the path
            string sanitizedPath = blobPath.Split('#')[0];
            string extension = Path.GetExtension(sanitizedPath).ToLowerInvariant();

            // Determine the Content-Type
            string contentType = extension switch
            {
                ".pdf" => "application/pdf",
                ".txt" => "text/plain",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".doc" or ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" or ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".ppt" or ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".html" or ".htm" => "text/html",
                ".json" => "application/json",
                ".xml" => "application/xml",
                _ => "application/octet-stream" // Default fallback
            };

            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            BlobClient blobClient = containerClient.GetBlobClient(sanitizedPath);

            if (await blobClient.ExistsAsync())
            {
                // Create the SAS URL
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = _containerName,
                    BlobName = sanitizedPath,
                    Resource = "b",
                    ExpiresOn = DateTime.UtcNow.AddMinutes(expirationMinutes),
                    ContentType = contentType,
                    ContentDisposition = $"inline; filename=\"{Path.GetFileName(sanitizedPath)}\""
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);

                // Append Content-Disposition and Content-Type to force inline view
                string contentDisposition = "inline; filename=" + Uri.EscapeDataString(Path.GetFileName(sanitizedPath));
                return $"{sasUri}&response-content-disposition={Uri.EscapeDataString(contentDisposition)}&response-content-type={Uri.EscapeDataString(contentType)}";
            }
            else
            {
                Console.WriteLine($"Blob does not exist: {sanitizedPath}");
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating SAS URL: {ex.Message}");
            return string.Empty;
        }
    }

    public async Task<string> ReadBlobContentAsync(
        string tenantId,
        string userId,
        string directory,
        string blobName,
        string fileName)
    {
        try
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Construct the full path including tenantId
            string destination = $"{tenantId}/{userId}/{directory}/{blobName}/{fileName}";

            Console.WriteLine($"Attempting to read blob at path: {destination}");

            BlobClient blobClient = containerClient.GetBlobClient(destination);

            if (await blobClient.ExistsAsync())
            {
                using var blobStream = await blobClient.OpenReadAsync();
                using var reader = new StreamReader(blobStream);
                return await reader.ReadToEndAsync();
            }
            else
            {
                Console.WriteLine($"Blob does not exist at path: {destination}");
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading blob content: {ex.Message}");
            return string.Empty;
        }
    }


    // Method to list blobs in a specified directory
    public async Task<IEnumerable<BlobItem>> ListBlobsAsync(string directory)
    {
        var blobServiceClient = new BlobServiceClient(_storageConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

        var blobs = new List<BlobItem>();

        await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: directory))
        {
            // Log each blob's name and other properties if needed
            Console.WriteLine($"Blob found: Name = {blobItem.Name}, Size = {blobItem.Properties.ContentLength ?? 0} bytes");

            blobs.Add(blobItem);
        }

        // Log total count of blobs found
        Console.WriteLine($"Total blobs found in directory '{directory}': {blobs.Count}");

        return blobs;
    }

    public async Task<bool> CheckBlobExistsAsync(string blobName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(_storageConnectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        return await blobClient.ExistsAsync();
    }
}
