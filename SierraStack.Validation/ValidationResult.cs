namespace SierraStack.Validation;

/// <summary>
/// Represents the outcome of a validation operation.
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// Indicates whether the validation was successful.
    /// </summary>
    public bool IsValid => Errors.Count == 0;
    
    /// <summary>
    /// A list of validation errors, if any.
    /// </summary>
    public List<ValidationFailure> Errors { get; init; } = [];
    
    /// <summary>
    /// Creates a valid result.
    /// </summary>
    public static ValidationResult Success() => new();
    
    /// <summary>
    /// Creates a result with validation errors.
    /// </summary>
    /// <param name="errors">The list of errors.</param>
    public static ValidationResult Failure(params ValidationFailure[] errors) => new ValidationResult { Errors = errors.ToList() };
}