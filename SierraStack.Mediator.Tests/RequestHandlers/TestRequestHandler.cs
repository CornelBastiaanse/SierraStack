using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Tests.Requests;

namespace SierraStack.Mediator.Tests.RequestHandlers;

public class TestRequestHandler : IRequestHandler<TestRequest, string>
{
    private readonly List<string> _calls;
    
    public TestRequestHandler(List<string> calls)
    {
        _calls = calls;
    }
    
    public Task<string> HandleAsync(TestRequest request, CancellationToken cancellationToken)
    {
        _calls?.Add("Handler");
        return Task.FromResult("Handled");
    }
}