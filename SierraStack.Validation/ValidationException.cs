namespace SierraStack.Validation;

/// <summary>
/// Thrown when validation fails on a request.
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// The list of validation failures that caused the exception.
    /// </summary>
    public IReadOnlyList<ValidationFailure> Errors { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationException"/> class with the specified validation errors.
    /// </summary>
    /// <param name="errors">The list of validation failures that caused the exception.</param>
    public ValidationException(IEnumerable<ValidationFailure> errors) : base("Validation failed for one or more properties.")
    {
        Errors = errors.ToList();
    }
}