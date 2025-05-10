namespace SierraStack.Validation;

/// <summary>
/// Validates a request and returns a <see cref="ValidationResult"/>.
/// </summary>
/// <typeparam name="TRequest">The type of request to validate.</typeparam>
public interface IValidator<in TRequest>
{
    /// <summary>
    /// Validates the specified request asynchronously.
    /// </summary>
    /// <param name="request">The request instance to validate.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The result of the validation.</returns>
    Task<ValidationResult> ValidateAsync(TRequest request, CancellationToken cancellationToken = default);
}