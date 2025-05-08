namespace SierraStack.Mediator.Behaviors.Timeout;

/// <summary>
/// Configuration options for <see cref="TimeoutBehavior{TRequest, TResponse}"/>.
/// </summary>
public class TimeoutBehaviorOptions
{
    /// <summary>
    /// The maximum amount of time allowed for a request to complete.
    /// If the handler exceeds this duration, a <see cref="TimeoutException"/> is thrown.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);
}