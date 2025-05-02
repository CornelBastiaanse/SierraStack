namespace SierraStack.Mediator.Abstractions;

/// <summary>
/// Defines a handler for a request of type <typeparamref name="TRequest"/> that returns a response of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TRequest">The type of request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response returned by the handler.</typeparam>
public interface IRequestHandler<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handles the request and returns a response.
    /// </summary>
    /// <param name="request">The request instance to handle.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>The response for the given request.</returns>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}