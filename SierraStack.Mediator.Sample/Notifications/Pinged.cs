using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Sample.Notifications;

public class Pinged : INotification
{
    public string Source { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}