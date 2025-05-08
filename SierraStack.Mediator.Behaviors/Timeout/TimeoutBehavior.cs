using Microsoft.Extensions.Options;
using SierraStack.Mediator.Pipeline;

namespace SierraStack.Mediator.Behaviors.Timeout;

/// <summary>
/// A pipeline behavior that enforces a timeout for request execution.
/// If the request handler does not complete within the configured duration,
/// a <see cref="TimeoutException"/> is thrown.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the handler.</typeparam>
public class TimeoutBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// The options for configuring the timeout behavior.
    /// </summary>
    private readonly TimeoutBehaviorOptions _options;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TimeoutBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="options">The timeout configuration options.</param>
    public TimeoutBehavior(IOptions<TimeoutBehaviorOptions> options)
    {
        _options = options.Value;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(_options.Timeout);

        try
        {
            return await next().WaitAsync(cts.Token);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException($"Request of type {typeof(TRequest).Name} exceeded the configured timeout of {_options.Timeout.TotalMilliseconds} ms.");
        }
    }
}