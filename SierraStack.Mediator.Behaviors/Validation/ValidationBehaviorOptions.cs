using SierraStack.Validation;

namespace SierraStack.Mediator.Behaviors.Validation;

/// <summary>
/// Configuration options for how validation behavior handles failures.
/// </summary>
public class ValidationBehaviorOptions
{
    /// <summary>
    /// Whether to throw a <see cref="ValidationException"/> when validation fails.
    /// </summary>
    public bool ThrowOnFailure { get; set; } = true;

    /// <summary>
    /// Optional callback invoked when validation fails, before throwing.
    /// </summary>
    public Action<IReadOnlyList<ValidationFailure>>? OnFailure { get; set; }
}