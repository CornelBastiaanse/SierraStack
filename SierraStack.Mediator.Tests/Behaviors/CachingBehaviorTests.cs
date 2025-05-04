using Microsoft.Extensions.Caching.Memory;
using SierraStack.Mediator.Behaviors.Caching;

namespace SierraStack.Mediator.Tests.Behaviors;

public class CachingBehaviorTests
{
    [Fact]
    public async Task Should_Cache_Response()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        var request = new SampleCacheableRequest();

        var behavior = new CachingBehavior<SampleCacheableRequest, string>(cache);

        var first = await behavior.HandleAsync(request, CancellationToken.None, () => Task.FromResult("result-1"));
        var second = await behavior.HandleAsync(request, CancellationToken.None, () => Task.FromResult("result-2"));

        Assert.Equal("result-1", first);
        Assert.Equal("result-1", second); // Should use cached
    }

    private class SampleCacheableRequest : ICacheableRequest<string>
    {
        public string CacheKey => "test-key";
        public TimeSpan? AbsoluteExpirationRelativeToNow => TimeSpan.FromMinutes(5);
    }
}