namespace SierraStack.Mediator.Behaviors.Caching;

/// <summary>
/// Marker interface for cacheable requests.
/// </summary>
/// <typeparam name="TResponse">The type of response expected.</typeparam>
public interface ICacheableRequest<TResponse>
{
    /// <summary>
    /// The cache key used to store the response.
    /// </summary>
    string CacheKey { get; }
    
    /// <summary>
    /// An optional absolute expiration time for the cache entry.
    /// </summary>
    TimeSpan? AbsoluteExpirationRelativeToNow { get; }
}