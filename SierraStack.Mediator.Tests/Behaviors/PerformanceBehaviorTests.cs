using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SierraStack.Mediator.Behaviors.Performance;

namespace SierraStack.Mediator.Tests.Behaviors;

public class PerformanceBehaviorTests
{
    [Fact]
    public async Task Should_Execute_And_Log_Performance()
    {
        var logger = new LoggerFactory().CreateLogger<PerformanceBehavior<object, string>>();
        var options = Options.Create(new PerformanceBehaviorOptions
        {
            WarningThresholdMilliseconds = 1,
            LogEveryRequest = true
        });

        var behavior = new PerformanceBehavior<object, string>(logger, options);

        var result = await behavior.HandleAsync(
            new object(),
            CancellationToken.None, 
            async () =>
            {
                await Task.Delay(10);
                return "OK";
            });

        Assert.Equal("OK", result);
    }
}