using SierraStack.Mediator.Abstractions;

namespace SierraStack.Mediator.Tests;

public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> HandleAsync(Ping request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Pong: {request.Message}");
    }
}