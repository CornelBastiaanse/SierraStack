namespace SierraStack.Mediator.Processing;

/// <summary>
/// Executes logic after a request handler has completed.
/// </summary>
public interface IRequestPostProcessor<TRequest, TResponse>
{
    /// <summary>
    /// Processes the request after it is handled.
    /// </summary>
    /// <param name="request">The request to process.</param>
    /// <param name="response">The response to process.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the processing.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ProcessAsync(TRequest request, TResponse response, CancellationToken cancellationToken = default);
}