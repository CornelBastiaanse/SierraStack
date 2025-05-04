using SierraStack.Mediator.Pipeline;
using SierraStack.Mediator.Tests.Requests;

namespace SierraStack.Mediator.Tests.Behaviors;

public class FirstBehavior : IPipelineBehavior<TestRequest, string>
{
    private readonly List<string> _calls;
    
    public FirstBehavior(List<string> calls)
    {
        _calls = calls;
    }
    
    public async Task<string> HandleAsync(TestRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<string> next)
    {
        _calls.Add("FirstBefore");
        var response = await next();
        _calls.Add("FirstAfter");
        return response;
    }
}