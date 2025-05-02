namespace SierraStack.Mediator.Abstractions;

/// <summary>
/// Defines a handler for a notification of type <typeparamref name="TNotification"/>.
/// </summary>
/// <typeparam name="TNotification">The type of notification being handled.</typeparam>
public interface INotificationHandler<TNotification>
where TNotification : INotification
{
    /// <summary>
    /// Handles the specified notification.
    /// </summary>
    /// <param name="notification">The notification instance to handle.</param>
    /// <param name="cancellationToken">A token for cancelling the operation.</param>
    Task HandleAsync(TNotification notification, CancellationToken cancellationToken);
}