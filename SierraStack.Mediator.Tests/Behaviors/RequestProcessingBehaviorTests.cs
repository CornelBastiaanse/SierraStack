using SierraStack.Mediator.Behaviors.Processing;
using SierraStack.Mediator.Processing;

namespace SierraStack.Mediator.Tests.Behaviors;

public class RequestProcessingBehaviorTests
{
    [Fact]
    public async Task Should_Invoke_PreProcessors_Before_Handler()
    {
        var log = new List<string>();
        var pre = new TestPreProcessor(log, "pre");
        var behavior = new RequestProcessingBehavior<object, string>([pre], []);

        var result = await behavior.HandleAsync(new object(), CancellationToken.None, () =>
        {
            log.Add("handler");
            return Task.FromResult("result");
        });

        Assert.Equal(new[] { "pre", "handler" }, log);
        Assert.Equal("result", result);
    }
    
    [Fact]
    public async Task Should_Invoke_PostProcessors_After_Handler()
    {
        var log = new List<string>();
        var post = new TestPostProcessor(log, "post");
        var behavior = new RequestProcessingBehavior<object, string>([], [post]);

        var result = await behavior.HandleAsync(new object(), CancellationToken.None, () =>
        {
            log.Add("handler");
            return Task.FromResult("result");
        });

        Assert.Equal(new[] { "handler", "post" }, log);
        Assert.Equal("result", result);
    }
    
    [Fact]
    public async Task Should_Invoke_All_Pre_And_Post_Processors()
    {
        var log = new List<string>();
        var behavior = new RequestProcessingBehavior<object, string>(
            [
                new TestPreProcessor(log, "pre1"),
                new TestPreProcessor(log, "pre2")
            ],
            [
                new TestPostProcessor(log, "post1"),
                new TestPostProcessor(log, "post2")
            ]);

        await behavior.HandleAsync(new object(), CancellationToken.None, () =>
        {
            log.Add("handler");
            return Task.FromResult("result");
        });

        Assert.Equal(["pre1", "pre2", "handler", "post1", "post2"], log);
    }
    
    private class TestPreProcessor : IRequestPreProcessor<object>
    {
        private readonly List<string> _log;
        private readonly string _name;
        public TestPreProcessor(List<string> log, string name) => (_log, _name) = (log, name);
        public Task ProcessAsync(object request, CancellationToken cancellationToken)
        {
            _log.Add(_name);
            return Task.CompletedTask;
        }
    }

    private class TestPostProcessor : IRequestPostProcessor<object, string>
    {
        private readonly List<string> _log;
        private readonly string _name;
        public TestPostProcessor(List<string> log, string name) => (_log, _name) = (log, name);
        public Task ProcessAsync(object request, string response, CancellationToken cancellationToken)
        {
            _log.Add(_name);
            return Task.CompletedTask;
        }
    }
}