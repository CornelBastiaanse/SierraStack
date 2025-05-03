using System.Diagnostics;
using SierraStack.Mediator.Pipeline;

namespace SierraStack.Mediator.Sample.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        Debug.WriteLine($"[LOG] Handling {typeof(TRequest).Name}");
        var response = await next();
        Debug.WriteLine($"[LOG] Handled {typeof(TRequest).Name}");
        return response;
    }
}