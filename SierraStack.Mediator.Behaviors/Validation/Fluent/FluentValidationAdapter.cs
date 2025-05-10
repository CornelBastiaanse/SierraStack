using SierraStack.Validation;

namespace SierraStack.Mediator.Behaviors.Validation.Fluent;

/// <summary>
/// Adapts FluentValidation's <see cref="IValidator{T}"/> to SierraStack's <see cref="IValidator{T}"/>.
/// </summary>
/// <typeparam name="T">The type being validated.</typeparam>
public class FluentValidatorAdapter<T> : IValidator<T>
{
    /// <summary>
    /// The inner FluentValidation validator.
    /// </summary>
    private readonly FluentValidation.IValidator<T> _inner;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="FluentValidatorAdapter{T}"/> class.
    /// </summary>
    /// <param name="inner">The inner FluentValidation validator.</param>
    public FluentValidatorAdapter(FluentValidation.IValidator<T> inner)
    {
        _inner = inner;
    }
    
    /// <inheritdoc/>
    public async Task<ValidationResult> ValidateAsync(T instance, CancellationToken cancellationToken = default)
    {
        var result = await _inner.ValidateAsync(instance, cancellationToken);

        if (result.IsValid)
            return ValidationResult.Success();

        var failures = result.Errors
            .Select(f => new ValidationFailure(f.PropertyName, f.ErrorMessage))
            .ToList();

        return ValidationResult.Failure(failures.ToArray());
    }
}