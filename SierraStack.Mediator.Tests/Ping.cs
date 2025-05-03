using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Tests;

public class Ping : IRequest<string>
{
    public string Message { get; set; } = "Hello";
}