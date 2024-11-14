using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmos.Copilot.Services;
using Cosmos.Copilot.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace CosmosContainers.ApiService
{
    /// <summary>
    /// Extension methods for mapping API endpoints for chat-related operations.
    /// </summary>
    public static class ApiEndpoints
    {
        /// <summary>
        /// Maps chat thread and message-related API endpoints.
        /// </summary>
        /// <param name="app">The web application instance.</param>
        /// <returns>The configured web application.</returns>
        public static WebApplication MapItemsApi(this WebApplication app)
        {
            // Endpoint to create a new chat thread
            app.MapPost("/chats/{tenantId}/{userId}/threads", 
            [SwaggerOperation(
                Summary = "Creates a new chat thread.",
                Description = "Creates a new chat thread for a specified tenant and user.",
                OperationId = "CreateNewChatthread"
            )]
            [SwaggerResponse(StatusCodes.Status201Created, "The newly created chat thread.", typeof(ThreadChat))]
            [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to create a new chat thread.")]
            async (
                [SwaggerParameter("The unique identifier of the tenant.")] string tenantId,
                [SwaggerParameter("The unique identifier of the user.")] string userId,
                [SwaggerParameter("The title of the chat thread.")] string title,
                ChatService chatService) =>
            {
                try
                {
                    var newthread = await chatService.CreateNewThreadChatAsync(tenantId, userId, title);
                    return Results.Created($"/chats/{tenantId}/{userId}/threads/{newthread.ThreadId}", newthread);
                }
                catch
                {
                    return Results.Problem("Failed to create a new chat thread.", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("CreateNewChatthread")
            .WithDescription("Creates a new chat thread for a specified tenant and user.")
            .Produces<ThreadChat>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status500InternalServerError);

            // Endpoint to rename an existing chat thread
            app.MapPut("/chats/{tenantId}/{userId}/threads/{threadId}/rename", 
            [SwaggerOperation(
                Summary = "Renames an existing chat thread.",
                Description = "Renames an existing chat thread for a specified tenant and user.",
                OperationId = "RenameChatthread"
            )]
            [SwaggerResponse(StatusCodes.Status204NoContent, "The chat thread was successfully renamed.")]
            [SwaggerResponse(StatusCodes.Status404NotFound, "The specified chat thread was not found.")]
            [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to rename the chat thread.")]
            async (
                [SwaggerParameter("The unique identifier of the tenant.")] string tenantId,
                [SwaggerParameter("The unique identifier of the user.")] string userId,
                [SwaggerParameter("The unique identifier of the thread to be renamed.")] string threadId,
                [SwaggerParameter("The new name for the chat thread.")] string newChatThreadName,
                ChatService chatService) =>
            {
                try
                {
                    await chatService.RenameChatThreadAsync(tenantId, userId, threadId, newChatThreadName);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { Message = $"Chat thread with threadId={threadId} not found." });
                }
                catch
                {
                    return Results.Problem("Failed to rename the chat thread.", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("RenameChatthread")
            .WithDescription("Renames an existing chat thread for a specified tenant and user.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
            
            // Endpoint to retrieve all chat threads for a specified tenant and user
            app.MapGet("/chats/{tenantId}/{userId}/threads", 
            [SwaggerOperation(
                Summary = "Retrieves all chat threads.",
                Description = "Retrieves all chat threads for a specified tenant and user.",
                OperationId = "GetAllChatthreads"
            )]
            [SwaggerResponse(StatusCodes.Status200OK, "A list of chat threads.", typeof(List<ThreadChat>))]
            [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to retrieve chat threads.")]
            async (
                [SwaggerParameter("The unique identifier of the tenant.")] string tenantId,
                [SwaggerParameter("The unique identifier of the user.")] string userId,
                ChatService chatService) =>
            {
                try
                {
                    var threads = await chatService.GetAllChatThreadsAsync(tenantId, userId);
                    var sanitizedthreads = threads.Select(thread => thread.GetSanitizedCopy()).ToList();

                    return Results.Ok(sanitizedthreads);
                }
                catch
                {
                    return Results.Problem("Failed to retrieve chat threads.", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("GetAllChatthreads")
            .WithDescription("Retrieves all chat threads for a specified tenant and user.")
            .Produces<List<ThreadChat>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);

            // Endpoint to delete a chat thread
            app.MapDelete("/chats/{tenantId}/{userId}/threads/{threadId}", 
            [SwaggerOperation(
                Summary = "Deletes a chat thread.",
                Description = "Deletes an existing chat thread and its associated messages for a specified tenant and user.",
                OperationId = "DeleteChatthread"
            )]
            [SwaggerResponse(StatusCodes.Status204NoContent, "The chat thread and its messages were successfully deleted.")]
            [SwaggerResponse(StatusCodes.Status404NotFound, "The specified chat thread was not found.")]
            [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to delete the chat thread.")]
            async (
                [SwaggerParameter("The unique identifier of the tenant.")] string tenantId,
                [SwaggerParameter("The unique identifier of the user.")] string userId,
                [SwaggerParameter("The unique identifier of the chat thread to delete.")] string threadId,
                ChatService chatService) =>
            {
                try
                {
                    await chatService.DeleteChatThreadAsync(tenantId, userId, threadId);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { Message = $"Chat thread with threadId={threadId} not found." });
                }
                catch
                {
                    return Results.Problem("Failed to delete the chat thread.", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("DeleteChatthread")
            .WithDescription("Deletes an existing chat thread and its associated messages for a specified tenant and user.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            // Endpoint to retrieve messages for a specific chat thread
            app.MapGet("/chats/{tenantId}/{userId}/threads/{threadId}/messages", 
            [SwaggerOperation(
                Summary = "Retrieves messages for a chat thread.",
                Description = "Retrieves all messages for a specified chat thread.",
                OperationId = "GetChatthreadMessages"
            )]
            [SwaggerResponse(StatusCodes.Status200OK, "A list of messages.", typeof(List<Message>))]
            [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to retrieve chat messages.")]
            async (
                [SwaggerParameter("The unique identifier of the tenant.")] string tenantId,
                [SwaggerParameter("The unique identifier of the user.")] string userId,
                [SwaggerParameter("The unique identifier of the chat thread.")] string threadId,
                ChatService chatService) =>
            {
                try
                {
                    var messages = await chatService.GetChatThreadMessagesAsync(tenantId, userId, threadId);
                    var sanitizedMessages = messages.Select(message => message.GetSanitizedCopy()).ToList();

                    return Results.Ok(sanitizedMessages);
                }
                catch
                {
                    return Results.Problem("Failed to retrieve chat messages.", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("GetChatthreadMessages")
            .WithDescription("Retrieves all messages for a specified chat thread.")
            .Produces<List<Message>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status500InternalServerError);
            
            // Endpoint to delete a specific message in a chat thread
            app.MapDelete("/chats/{tenantId}/{userId}/threads/{threadId}/messages/{messageId}", 
            [SwaggerOperation(
                Summary = "Deletes a specific message in a chat thread.",
                Description = "Deletes a specific message within a chat thread for a specified tenant and user.",
                OperationId = "DeleteMessage"
            )]
            [SwaggerResponse(StatusCodes.Status204NoContent, "The message was successfully deleted.")]
            [SwaggerResponse(StatusCodes.Status404NotFound, "The specified message or thread was not found.")]
            [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to delete the message.")]
            async (
                [SwaggerParameter("The unique identifier of the tenant.")] string tenantId,
                [SwaggerParameter("The unique identifier of the user.")] string userId,
                [SwaggerParameter("The unique identifier of the chat thread.")] string threadId,
                [SwaggerParameter("The unique identifier of the message to delete.")] string messageId,
                ChatService chatService) =>
            {
                try
                {
                    await chatService.DeleteMessageAsync(tenantId, userId, threadId, messageId);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException)
                {
                    return Results.NotFound(new { Message = $"Message with ID={messageId} not found in thread {threadId}." });
                }
                catch
                {
                    return Results.Problem("Failed to delete the message.", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("DeleteMessage")
            .WithDescription("Deletes a specific message within a chat thread for a specified tenant and user.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            // Endpoint to upsert a thread and message
            app.MapPost("/chats/{tenantId}/{userId}/threads/{threadId}/messages", 
            [SwaggerOperation(
                Summary = "Upserts a thread and message.",
                Description = "Upserts a thread and message for a specified tenant, user, and thread ID.",
                OperationId = "UpsertthreadAndMessage"
            )]
            [SwaggerResponse(StatusCodes.Status200OK, "thread and message upserted successfully.")]
            [SwaggerResponse(StatusCodes.Status404NotFound, "The specified thread was not found.")]
            [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to upsert thread and message.")]
            async (
                [SwaggerParameter("The unique identifier of the tenant.")] string tenantId,
                [SwaggerParameter("The unique identifier of the user.")] string userId,
                [SwaggerParameter("The unique identifier of the thread.")] string threadId,
                Message chatMessage,
                ChatService chatService) =>
            {
                try
                {
                    await chatService.UpsertThreadAndMessageAsync(tenantId, userId, threadId, chatMessage);
                    return Results.Ok(new { Message = "thread and message upserted successfully." });
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(new { Error = ex.Message });
                }
                catch
                {
                    return Results.Problem("Failed to upsert thread and message.", statusCode: StatusCodes.Status500InternalServerError);
                }
            })
            .WithName("UpsertthreadAndMessage")
            .WithDescription("Upserts a thread and message for a specified tenant, user, and thread ID.")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

            return app;
        }
    }
}
