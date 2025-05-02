using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Sample.Requests;

namespace SierraStack.Mediator.Sample.RequestHandlers;

public class PingHandler : IRequestHandler<Ping, string>
{
    public Task<string> HandleAsync(Ping request, CancellationToken cancellationToken)
    {
        return Task.FromResult($"Pong: {request.Message}");
    }
}