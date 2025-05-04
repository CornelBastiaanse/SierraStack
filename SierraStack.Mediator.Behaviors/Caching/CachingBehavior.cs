using Microsoft.Extensions.Caching.Memory;
using SierraStack.Mediator.Pipeline;

namespace SierraStack.Mediator.Behaviors.Caching;

/// <summary>
/// Returns cached response for cacheable requests.
/// </summary>
/// <typeparam name="TRequest">The type of the request being handled.</typeparam>
/// <typeparam name="TResponse">The type of response expected.</typeparam>
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : ICacheableRequest<TResponse>
{
    /// <summary>
    /// The cache to use for caching responses.
    /// </summary>
    private readonly IMemoryCache _cache;
    
    /// <summary>
    /// Parameterized constructor to inject the cache.
    /// </summary>
    /// <param name="cache">The cache to use for caching responses.</param>
    public CachingBehavior(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var key = request.CacheKey;

        if (_cache.TryGetValue<TResponse>(key, out var cached))
        {
            return cached!;
        }
        
        var response = await next();
        
        _cache.Set(key, response, request.AbsoluteExpirationRelativeToNow ?? TimeSpan.FromMinutes(5));
        
        return response;
    }
}