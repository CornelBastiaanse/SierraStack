using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SierraStack.Mediator.Behaviors.Retry;

namespace SierraStack.Mediator.Tests.Behaviors;

public class RetryBehaviorTests
{
    [Fact]
    public async Task Should_Retry_On_Transient_Exception()
    {
        var attempts = 0;
        var options = Options.Create(new RetryBehaviorOptions
        {
            RetryCount = 3,
            ShouldRetryOnException = _ => true
        });

        var behavior = new RetryBehavior<object, string>(CreateLogger(), options);

        var result = await behavior.HandleAsync(
            new object(),
            CancellationToken.None, 
            () =>
            {
                attempts++;
                if (attempts < 3)
                    throw new TimeoutException();
                return Task.FromResult("Success");
            });

        Assert.Equal("Success", result);
        Assert.Equal(3, attempts);
    }

    private ILogger<RetryBehavior<object, string>> CreateLogger() => new LoggerFactory().CreateLogger<RetryBehavior<object, string>>();
}