using Microsoft.Extensions.Logging;
using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Sample.Notifications;

namespace SierraStack.Mediator.Sample.NotificationHandlers;

public class LogPingedHandler : INotificationHandler<Pinged>
{
    private readonly ILogger<LogPingedHandler> _logger;
    
    public LogPingedHandler(ILogger<LogPingedHandler> logger)
    {
        _logger = logger;
    }
    
    public Task HandleAsync(Pinged notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Pinged received from {Source} at {Time}", notification.Source, notification.Timestamp);
        return Task.CompletedTask;
    }
}