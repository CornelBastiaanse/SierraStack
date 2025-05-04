using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Tests.Requests;

namespace SierraStack.Mediator.Tests.RequestHandlers;

public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> HandleAsync(Ping request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Pong: {request.Message}");
    }
}