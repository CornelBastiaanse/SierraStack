namespace SierraStack.Mediator.Processing;

/// <summary>
/// Executes logic before a request handler is invoked.
/// </summary>
public interface IRequestPreProcessor<TRequest>
{
    /// <summary>
    /// Processes the request before it is handled.
    /// </summary>
    /// <param name="request">The request to process.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the processing.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task ProcessAsync(TRequest request, CancellationToken cancellationToken = default);
}