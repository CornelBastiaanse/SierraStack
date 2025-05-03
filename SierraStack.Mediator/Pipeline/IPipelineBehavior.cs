namespace SierraStack.Mediator.Pipeline;

/// <summary>
/// Defines a behavior executed around the handling of a request.
/// Useful for cross-cutting concerns like logging, validation, etc.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// Handles the request, either continuing to the next step or short-circuiting it.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <param name="next">The next step in the pipeline.</param>
    /// <returns>The response from the request pipeline.</returns>
    Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next);
}