namespace SierraStack.Mediator.Abstractions;

/// <summary>
/// Represents a request that expects a response of type <typeparamref name="TResponse"/>.
/// Typically used for command or query operations in a CQRS-based architecture.
/// </summary>
/// <typeparam name="TResponse">The type of response expected from the handler.</typeparam>
public interface IRequest<TResponse>
{
    
}