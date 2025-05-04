using SierraStack.Mediator.Pipeline;
using Microsoft.Extensions.Logging;

namespace SierraStack.Mediator.Behaviors.Logging;

/// <summary>
/// Logs after a request is handled.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public class LoggingAfterBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Logger for logging after the request is handled.
    /// </summary>
    private readonly ILogger<LoggingAfterBehavior<TRequest, TResponse>> _logger;
    
    /// <summary>
    /// Parameterized constructor for the LoggingBehavior class.
    /// </summary>
    /// <param name="logger">The logger instance used for logging.</param>
    public LoggingAfterBehavior(ILogger<LoggingAfterBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var response = await next();
        _logger.LogDebug("✔ After handling: {RequestType}", typeof(TRequest).Name);
        return response;
    }
}