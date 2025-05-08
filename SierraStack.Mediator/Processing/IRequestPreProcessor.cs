namespace SierraStack.Mediator.Processing;

/// <summary>
/// Defines a processor that executes logic before a request handler is invoked.
/// </summary>
public interface IRequestPreProcessor<TRequest>
{
    /// <summary>
    /// Executes processing logic before the request is handled.
    /// </summary>
    /// <param name="request">The request to process.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    Task ProcessAsync(TRequest request, CancellationToken cancellationToken = default);
}