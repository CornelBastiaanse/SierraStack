namespace SierraStack.Mediator.Processing;

/// <summary>
/// Defines a processor that executes logic after a request handler has completed.
/// </summary>
public interface IRequestPostProcessor<TRequest, TResponse>
{
    /// <summary>
    /// Executes processing logic after the request has been handled and a response has been generated.
    /// </summary>
    /// <param name="request">The request that was processed.</param>
    /// <param name="response">The response produced by the handler.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    Task ProcessAsync(TRequest request, TResponse response, CancellationToken cancellationToken = default);
}