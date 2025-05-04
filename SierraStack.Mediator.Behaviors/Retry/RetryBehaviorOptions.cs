namespace SierraStack.Mediator.Behaviors.Retry;

/// <summary>
/// Configuration options for RetryBehavior.
/// </summary>
public class RetryBehaviorOptions
{
    /// <summary>
    /// Number of retry attempts. Default is 3.
    /// </summary>
    public int RetryCount { get; set; } = 3;

    /// <summary>
    /// Function to determine if an exception is transient and should be retried.
    /// </summary>
    public Func<Exception, bool> ShouldRetryOnException { get; set; } = ex => ex is TimeoutException || ex is HttpRequestException;

    /// <summary>
    /// Optional callback to run on each retry attempt.
    /// </summary>
    public Action<Exception, TimeSpan, int>? OnRetry { get; set; }

    /// <summary>
    /// Sleep duration function per attempt.
    /// </summary>
    public Func<int, TimeSpan> SleepDurationProvider { get; set; } = attempt => TimeSpan.FromMilliseconds(200 * attempt);
}