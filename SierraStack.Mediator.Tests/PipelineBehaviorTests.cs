using Microsoft.Extensions.DependencyInjection;
using SierraStack.Mediator.Abstractions;
using SierraStack.Mediator.Core;
using SierraStack.Mediator.Extensions.Microsoft.DependencyInjection;
using SierraStack.Mediator.Pipeline;
using SierraStack.Mediator.Tests.Behaviors;
using SierraStack.Mediator.Tests.RequestHandlers;
using SierraStack.Mediator.Tests.Requests;

namespace SierraStack.Mediator.Tests;

public class PipelineBehaviorTests
{
    [Fact]
    public async Task Behaviors_Are_Executed_In_Order()
    {
        // Arrange
        var calls = new List<string>();
        
        var services = new ServiceCollection();
        services.AddSierraStackMediator(typeof(TestRequestHandler).Assembly);
        
        services.AddTransient<IPipelineBehavior<TestRequest, string>>(_ => new FirstBehavior(calls));
        services.AddTransient<IPipelineBehavior<TestRequest, string>>(_ => new SecondBehavior(calls));
        services.AddTransient<IRequestHandler<TestRequest, string>>(_ => new TestRequestHandler(calls));
        
        var provider = services.BuildServiceProvider();
        var mediator = provider.GetRequiredService<IMediator>();
        
        // Act
        var result = await mediator.SendAsync(new TestRequest());
        
        // Assert
        Assert.Equal("Handled", result);
        Assert.Equal(["FirstBefore", "SecondBefore", "Handler", "SecondAfter", "FirstAfter"], calls);
    }
}