using Microsoft.Extensions.Logging;
using SierraStack.Mediator.Behaviors.ExceptionHandling;

namespace SierraStack.Mediator.Tests.Behaviors;

public class ExceptionHandlingBehaviorTests
{
    [Fact]
    public async Task Should_Log_And_Rethrow_Exception()
    {
        var logger = new LoggerFactory().CreateLogger<ExceptionHandlingBehavior<object, string>>();
        var behavior = new ExceptionHandlingBehavior<object, string>(logger);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            behavior.HandleAsync(
                new object(),
                CancellationToken.None, 
                () => throw new InvalidOperationException("Oops")));

        Assert.Equal("Oops", ex.Message);
    }
}