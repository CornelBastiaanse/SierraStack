using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Benchmarks;

public class Ping : IRequest<string>
{
    public string Message => "Ping";
}