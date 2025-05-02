using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Sample.Requests;

public class Ping : IRequest<string>
{
    public string Message { get; set; } = "Ping";
}