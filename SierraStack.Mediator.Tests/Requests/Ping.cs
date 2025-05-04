using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Tests.Requests;

public class Ping : IRequest<string>
{
    public string Message { get; set; } = "Hello";
}