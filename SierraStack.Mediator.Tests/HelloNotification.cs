using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Tests;

public class HelloNotification : INotification
{
    public string Name { get; set; } = "World";
}