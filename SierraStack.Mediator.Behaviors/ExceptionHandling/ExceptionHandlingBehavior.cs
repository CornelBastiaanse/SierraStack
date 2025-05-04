using Microsoft.Extensions.Logging;
using SierraStack.Mediator.Pipeline;

namespace SierraStack.Mediator.Behaviors.ExceptionHandling;

/// <summary>
/// Catches and logs unhandled exceptions during request handling.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the request handler.</typeparam>
public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Logger for logging exceptions.
    /// </summary>
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;
    
    /// <summary>
    /// Parameterized constructor for the ExceptionHandlingBehavior class.
    /// </summary>
    /// <param name="logger">Logger instance for logging exceptions.</param>
    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception while handling {RequestName}", typeof(TRequest).Name);
            throw;
        }
    }
}