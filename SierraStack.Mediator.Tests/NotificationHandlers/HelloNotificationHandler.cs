using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Tests.Notifications;

namespace SierraStack.Mediator.Tests.NotificationHandlers;

public class HelloNotificationHandler : INotificationHandler<HelloNotification>
{
    public static bool WasCalled { get; private set; }
    
    public static void Reset() => WasCalled = false;
    
    public Task HandleAsync(HelloNotification notification, CancellationToken cancellationToken)
    {
        WasCalled = true;
        return Task.CompletedTask;
    }
}