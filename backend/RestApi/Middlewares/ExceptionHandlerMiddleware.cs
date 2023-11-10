using RestApi.Exceptions;
using RestApi.Exceptions.Common;

namespace RestApi.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly List<IExceptionHandler> _exceptionHandlers = new()
    {
        new NotFoundExceptionHandler(),
        new InternalServerErrorExceptionHandler(),
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var handler = _exceptionHandlers.Single(handler => ex.GetType() == handler.ExceptionType);
            await handler.HandleException(context, ex);
        }
    }
}