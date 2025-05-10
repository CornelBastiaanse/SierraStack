namespace SierraStack.Validation;

/// <summary>
/// Represents a single validation failure.
/// </summary>
/// <param name="PropertyName">The name of the property that failed validation.</param>
/// <param name="ErrorMessage">The error message describing the validation failure.</param>
public record ValidationFailure(string PropertyName, string ErrorMessage);