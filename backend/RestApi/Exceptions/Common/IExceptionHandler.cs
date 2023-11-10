namespace RestApi.Exceptions.Common;

public interface IExceptionHandler
{
    public Type ExceptionType { get; }

    Task HandleException(HttpContext context, Exception exception);
}
