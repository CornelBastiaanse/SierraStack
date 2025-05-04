using SierraStack.Mediator.Pipeline;
using Microsoft.Extensions.Logging;

namespace SierraStack.Mediator.Behaviors.Logging;

/// <summary>
/// Logs when a request starts and finishes handling.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Logger for logging before and after the request is handled.
    /// </summary>
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    
    /// <summary>
    /// Parameterized constructor for the LoggingBehavior class.
    /// </summary>
    /// <param name="logger">The logger instance used for logging.</param>
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation("Handling request: {RequestName}", requestName);
        
        var response = await next();
        
        _logger.LogInformation("Handled request: {RequestName}", requestName);
        
        return response;
    }
}