using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Core;

/// <summary>
/// Defines the contract for sending requests and publishing notifications in-process.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Sends a request to its corresponding handler and returns a response.
    /// </summary>
    /// <typeparam name="TResponse">The type of response expected.</typeparam>
    /// <param name="request">The request instance to send.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    /// <returns>The response returned by the handler.</returns>
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Publishes a notification to all registered handlers.
    /// </summary>
    /// <typeparam name="TNotification">The type of notification.</typeparam>
    /// <param name="notification">The notification instance to publish.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
}