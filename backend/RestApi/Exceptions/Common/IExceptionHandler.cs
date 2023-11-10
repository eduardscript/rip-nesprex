namespace RestApi.Exceptions.Common;

public interface IExceptionHandler<TException>
    where TException : Exception
{
    Task HandleException(HttpContext context, TException exception);
}
