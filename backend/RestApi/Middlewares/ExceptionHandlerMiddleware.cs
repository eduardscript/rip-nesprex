using System.Reflection;
using RestApi.Exceptions;
using RestApi.Exceptions.Common;

namespace RestApi.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var exceptionHandlerType = GetExceptionHandlerType(ex.GetType());
            
            dynamic handler = Activator.CreateInstance(exceptionHandlerType)!;
            handler.HandleException(context, (dynamic)ex);
        }
    }
    
    private static Type GetExceptionHandlerType(Type exceptionType)
    {
        return Assembly.GetAssembly(typeof(ExceptionHandlerMiddleware))!
            .GetTypes()
            .First(IsExceptionHandlerType(exceptionType));
        
        static Func<Type, bool> IsExceptionHandlerType(Type exceptionType)
        {
            return type =>
                type.IsClass &&
                type.GetInterfaces().Any(IsGenericExceptionHandlerForType(exceptionType));
        }

        static Func<Type, bool> IsGenericExceptionHandlerForType(Type exceptionType)
        {
            return interfaceType =>
                interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(IExceptionHandler<>) &&
                interfaceType.GetGenericArguments()[0] == exceptionType;
        }
    }
}