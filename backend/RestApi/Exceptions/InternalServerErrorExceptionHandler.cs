using System.Net;
using System.Text.Json;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using RestApi.Exceptions.Common;

namespace RestApi.Exceptions;

public class InternalServerErrorExceptionHandler : IExceptionHandler
{
    public Type ExceptionType { get; } = typeof(Exception);

    public async Task HandleException(HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "InternalServerError",
            Detail = "Something went wrong",
            Status = (int)HttpStatusCode.InternalServerError,
        };

        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}