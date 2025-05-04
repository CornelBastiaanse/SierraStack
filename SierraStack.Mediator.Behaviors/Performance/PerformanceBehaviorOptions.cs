namespace SierraStack.Mediator.Behaviors.Performance;

/// <summary>
/// Options for PerformanceBehavior.
/// </summary>
public class PerformanceBehaviorOptions
{
    /// <summary>
    /// Log a warning if the handler takes longer than this threshold (in ms).
    /// </summary>
    public int WarningThresholdMilliseconds { get; set; } = 500;

    /// <summary>
    /// Enable or disable logging of all durations.
    /// </summary>
    public bool LogEveryRequest { get; set; } = true;
}