using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RestApi.Exceptions.Common;

namespace RestApi.Exceptions;

public class NotFoundExceptionHandler : IExceptionHandler<NotFoundException>
{
    public async Task HandleException(HttpContext context, NotFoundException exception)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "NotFound",
            Detail = exception.Message,
            Status = (int)HttpStatusCode.NotFound,
        };

        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}