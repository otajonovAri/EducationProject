using EducationApp.Application.ErrorLogs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace EducationApp.Application.Middleware;

public class GlobalException(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        string message = "Sorry , Internal Server Error";
        int status = (int)HttpStatusCode.InternalServerError;
        string title = "Error";

        try
        {
            await next(context);

            // To Many Request
            if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
            {
                title = "Warning";
                message = "To Many Request";
                status = (int)StatusCodes.Status429TooManyRequests;

                await ModifyHeader(context, title, message, status);
            }

            // Is UnAuthorized => 401
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                title = "alert";
                message = "You are not authorized to access.";

                await ModifyHeader(context, title, message, status);
            }

            // Is Forbidden => 403
            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                title = "Out of Access";
                message = "You are not allowed/requierd to access";
                status = StatusCodes.Status403Forbidden;

                await ModifyHeader(context, title, message, status);
            }
        }
        catch (Exception e)
        {
            // Log Write Console , Debugger , File
            LogException.LogExceptions(e);

            // Is Time 
            if (e is TaskCanceledException || e is TimeoutException)
            {
                title = "Out of Time";
                message = "Request timeout... try again...";
                status = StatusCodes.Status408RequestTimeout;
            }

            await ModifyHeader(context, title, message, status);
        }
    }

    private async Task ModifyHeader(HttpContext context, string title, string message, int status)
    {
        // Display scary - free message to client
        context.Response.ContentType = "Application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
        {
            Detail = message,
            Status = status,
            Title = title
        }), CancellationToken.None);
        return;
    }
}