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
            var handlerType = GetExceptionHandlerType(ex.GetType());
            var handlerInstance = Activator.CreateInstance(handlerType)!;
            var handlerExceptionMethod = handlerType.GetMethod("HandleException");

            await (Task)handlerExceptionMethod!.Invoke(handlerInstance, new object[] { context, ex })!;
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