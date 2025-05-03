using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace SierraStack.Mediator.Pipeline;

internal static class PipelineExecutor
{
    public static Task<TResponse> ExecuteDynamic<TResponse>(
        IServiceProvider provider,
        object request,
        CancellationToken cancellationToken,
        Func<object, CancellationToken, Task<TResponse>> finalHandler)
    {
        var requestType = request.GetType();
        var method = typeof(PipelineExecutor)
            .GetMethod(nameof(ExecuteGeneric), BindingFlags.NonPublic | BindingFlags.Static)!
            .MakeGenericMethod(requestType, typeof(TResponse));

        return (Task<TResponse>)method.Invoke(null, [provider, request, cancellationToken, finalHandler])!;
    }

    private static Task<TResponse> ExecuteGeneric<TRequest, TResponse>(
        IServiceProvider provider,
        object request,
        CancellationToken cancellationToken,
        Func<object, CancellationToken, Task<TResponse>> finalHandler)
    {
        var behaviors = provider
            .GetServices<IPipelineBehavior<TRequest, TResponse>>()
            .Reverse()
            .ToList();

        RequestHandlerDelegate<TResponse> next = () => finalHandler((TRequest)request, cancellationToken);

        foreach (var behavior in behaviors)
        {
            var current = next;
            next = () => behavior.HandleAsync((TRequest)request, cancellationToken, current);
        }

        return next();
    }
}