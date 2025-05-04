using Microsoft.Extensions.Logging;
using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Sample.Notifications;

namespace SierraStack.Mediator.Sample.NotificationHandlers;

public class MetricsPingedHandler : INotificationHandler<Pinged>
{
    private readonly ILogger<MetricsPingedHandler> _logger;
    
    public MetricsPingedHandler(ILogger<MetricsPingedHandler> logger)
    {
        _logger = logger;
    }
    
    public Task HandleAsync(Pinged notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Tracking metric for Pinged from {Source}", notification.Source);
        return Task.CompletedTask;
    }
}