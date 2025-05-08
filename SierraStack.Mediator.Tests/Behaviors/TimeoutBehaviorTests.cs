using Microsoft.Extensions.Options;
using SierraStack.Mediator.Behaviors.Timeout;

namespace SierraStack.Mediator.Tests.Behaviors;

public class TimeoutBehaviorTests
{
    [Fact]
    public async Task Should_Throw_TimeoutException_If_Handler_Exceeds_Timeout()
    {
        var options = Options.Create(new TimeoutBehaviorOptions { Timeout = TimeSpan.FromMilliseconds(50) });
        var behavior = new TimeoutBehavior<object, string>(options);

        async Task<string> SlowHandler()
        {
            await Task.Delay(200);
            return "done";
        }

        await Assert.ThrowsAsync<TimeoutException>(() =>
            behavior.HandleAsync(new object(), CancellationToken.None, SlowHandler));
    }

    [Fact]
    public async Task Should_Complete_If_Handler_Is_Within_Timeout()
    {
        var options = Options.Create(new TimeoutBehaviorOptions { Timeout = TimeSpan.FromMilliseconds(500) });
        var behavior = new TimeoutBehavior<object, string>(options);

        var result = await behavior.HandleAsync(new object(), CancellationToken.None, async () =>
        {
            await Task.Delay(100);
            return "success";
        });

        Assert.Equal("success", result);
    }

    [Fact]
    public async Task Should_Respect_CancellationToken()
    {
        var options = Options.Create(new TimeoutBehaviorOptions { Timeout = TimeSpan.FromSeconds(1) });
        var behavior = new TimeoutBehavior<object, string>(options);

        using var cts = new CancellationTokenSource(50);

        await Assert.ThrowsAsync<TaskCanceledException>(() =>
            behavior.HandleAsync(new object(), cts.Token, async () =>
            {
                await Task.Delay(500, cts.Token);
                return "cancelled";
            }));
    }
}