using FluentValidation;
using SierraStack.Mediator.Pipeline;

namespace SierraStack.Mediator.Behaviors.Validation;

/// <summary>
/// Runs FluentValidation validators before executing the handler.
/// </summary>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    /// <inheritdoc/>
    public async Task<TResponse> HandleAsync(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
            return await next();
        
        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        
        var failures = results
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();
        
        if (failures.Count > 0)
            throw new ValidationException(failures);
        
        return await next();
    }
}