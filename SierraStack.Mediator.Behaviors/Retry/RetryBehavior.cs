using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using SierraStack.Mediator.Pipeline;

namespace SierraStack.Mediator.Behaviors.Retry;

/// <summary>
/// Retries transient failures when handling a request.
/// </summary>
/// <typeparam name="TRequest">The type of the request to handle.</typeparam>
/// <typeparam name="TResponse">The type of the response to expect.</typeparam>
public class RetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Logger for logging retry attempts.
    /// </summary>
    private readonly ILogger<RetryBehavior<TRequest, TResponse>> _logger;
    
    /// <summary>
    /// The retry policy to use for transient failures.
    /// </summary>
    private readonly AsyncRetryPolicy _retryPolicy;
    
    /// <summary>
    /// Parameterized constructor for the retry behavior.
    /// </summary>
    /// <param name="logger">The logger to use for logging retry attempts.</param>
    /// <param name="options">The options to configure the retry behavior.</param>
    public RetryBehavior(
        ILogger<RetryBehavior<TRequest, TResponse>> logger,
        IOptions<RetryBehaviorOptions> options)
    {
        _logger = logger;
        var config = options.Value;
        
        // Define a default retry policy (can be replaced later via DI)
        _retryPolicy = Policy
            .Handle<Exception>(ex => config.ShouldRetryOnException(ex))
            .WaitAndRetryAsync(
                retryCount: config.RetryCount,
                sleepDurationProvider: config.SleepDurationProvider,
                onRetry: (ex, delay, attempt, _) =>
                {
                    config.OnRetry?.Invoke(ex, delay, attempt);
                    
                    _logger.LogWarning(
                        ex,
                        "Retry {Attempt} after {Delay}ms for {Request}", 
                        attempt,
                        delay.TotalMilliseconds, 
                        typeof(TRequest).Name);
                });
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        return await _retryPolicy.ExecuteAsync(() => next());
    }
}