using Microsoft.Extensions.Options;
using SierraStack.Mediator.Pipeline;
using SierraStack.Validation;
using ValidationException = SierraStack.Validation.ValidationException;

namespace SierraStack.Mediator.Behaviors.Validation;

/// <summary>
/// Runs FluentValidation validators before executing the handler.
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    /// <summary>
    /// The collection of validators to run against the request.
    /// </summary>
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    /// <summary>
    /// The options for configuring the validation behavior.
    /// </summary>
    private readonly ValidationBehaviorOptions _options;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validators">The collection of validators to run against the request.</param>
    /// <param name="options">The options for configuring the validation behavior. </param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IOptions<ValidationBehaviorOptions> options)
    {
        _validators = validators;
        _options = options.Value;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var failures = new List<ValidationFailure>();

        foreach (var validator in _validators)
        {
            var result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
                failures.AddRange(result.Errors);
        }

        if (failures.Count <= 0) return await next();
        
        _options.OnFailure?.Invoke(failures);

        if (_options.ThrowOnFailure)
            throw new ValidationException(failures);

        return await next();
    }
}