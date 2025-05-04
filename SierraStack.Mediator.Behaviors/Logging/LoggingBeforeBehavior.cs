using SierraStack.Mediator.Pipeline;
using Microsoft.Extensions.Logging;

namespace SierraStack.Mediator.Behaviors.Logging;

/// <summary>
/// Logs before a request starts.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public class LoggingBeforeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Logger for logging before the request is handled.
    /// </summary>
    private readonly ILogger<LoggingBeforeBehavior<TRequest, TResponse>> _logger;
    
    /// <summary>
    /// Parameterized constructor for the LoggingBehavior class.
    /// </summary>
    /// <param name="logger">The logger instance used for logging.</param>
    public LoggingBeforeBehavior(ILogger<LoggingBeforeBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        _logger.LogDebug("→ Before handling: {RequestType}", typeof(TRequest).Name);
        return await next();
    }
}