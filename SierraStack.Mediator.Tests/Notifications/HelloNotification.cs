using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Tests.Notifications;

public class HelloNotification : INotification
{
    public string Name { get; set; } = "World";
}